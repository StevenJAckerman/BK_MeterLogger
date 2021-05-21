using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace BK_MeterLogger
{
    public enum Level : int
	{
		Critical = 0,         
		Error = 1,         
		Warning = 2,         
		Info = 3,         
		Verbose = 4,         
		Debug = 5
	};     
	
	public sealed class ListBoxLog : IDisposable     
	{         
		private const string DEFAULT_MESSAGE_FORMAT = "{0} [{5}] : {8}";
		private const int DEFAULT_MAX_LINES_IN_LISTBOX = 750;        
		private bool _disposed;         
		//private ListBox _listBox;
		private DoubleBufferedListBox _listBox;
		private string _messageFormat;         
		private int _maxEntriesInListBox;         
		private bool _canAdd;         
		private bool _paused;
        private Font _font;
          
		private void OnHandleCreated(object sender, EventArgs e)
		{
			_canAdd = true;
		}

		private void OnHandleDestroyed(object sender, EventArgs e)
		{
			_canAdd = false;
		}

        private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        {
            LogEvent logEvent = ((ListBox)sender).Items[e.Index] as LogEvent;
            e.ItemHeight = (int) e.Graphics.MeasureString(FormatALogEventMessage(logEvent, _messageFormat),
                _font, ((ListBox)sender).Width).Height;
        }
		
		private void DrawItemHandler(object sender, DrawItemEventArgs e)         
		{
			if (e.Index >= 0)             
			{                 
				e.DrawBackground();                 
				
				LogEvent logEvent = ((ListBox)sender).Items[e.Index] as LogEvent;
                  
				// SafeGuard against wrong configuration of list box                 
				if (logEvent == null)
				{
					logEvent = new LogEvent(Level.Critical, ((ListBox)sender).Items[e.Index].ToString());
				}

				Color color;                 
				switch (logEvent.Level)
				{
					case Level.Critical:                         
						color = Color.White;
						break;
					case Level.Error:                         
						color = Color.Red;                         
						break;
					case Level.Warning:                         
						color = Color.Goldenrod;                         
						break;
					case Level.Info:                         
						color = Color.Green;                         
						break;
					case Level.Verbose:                         
						color = Color.Blue;                         
						break;
					default:
						color = Color.Black;
						break;
				} 
                 
				if (logEvent.Level == Level.Critical)
				{
					e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
				}

				e.Graphics.DrawString(FormatALogEventMessage(logEvent, _messageFormat), 
					_font, new SolidBrush(color), e.Bounds);
			
				e.DrawFocusRectangle();
			}
		}

		private void KeyDownHandler(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control)
			{
                if (e.KeyCode == Keys.A)
                {
                    SelectAllItems();
                }
                else if (e.KeyCode == Keys.C)
                {
                    CopyToClipboard();
                }
                else if (e.KeyCode == Keys.L)
                {
                    Clear();
                }
            }
		}

        private void SelectAllMenuOnClickHandler(object sender, EventArgs e)
        {
			SelectAllItems();
        }

		private void CopyMenuOnClickHandler(object sender, EventArgs e)
		{
			CopyToClipboard();
		}
         
		private void MenuPopupHandler(object sender, EventArgs e)
		{
			ContextMenu menu = sender as ContextMenu;             
			if (menu != null)
			{
				menu.MenuItems[1].Enabled = (_listBox.SelectedItems.Count > 0);
			}
		}

        private void ClearMenuOnClickHandler(object sender, EventArgs e)
        {
			Clear();
        }

		private class LogEvent
		{
			public LogEvent(Level level, string message)
			{
				EventTime = DateTime.Now;                 
				Level = level;                 
				Message = message;
			}

			public readonly DateTime EventTime;              
			public readonly Level Level;             
			public readonly string Message;
		}

		private void WriteEvent(LogEvent logEvent)
		{
			if ((logEvent != null) && (_canAdd))
			{
                if (_listBox.InvokeRequired)
                {
                    _listBox.BeginInvoke(new AddALogEntryDelegate(AddALogEntry), logEvent);
                }
                else
                {
                    AddALogEntry(logEvent);
                }
            }
		}

		private delegate void AddALogEntryDelegate(object item);
		
		private void AddALogEntry(object item)
		{
			_listBox.BeginUpdate();
			_listBox.Items.Add(item);
			if (_listBox.Items.Count > _maxEntriesInListBox)
			{
				_listBox.Items.RemoveAt(0);
			}

			if (!_paused) 
				_listBox.TopIndex = _listBox.Items.Count - 1;
			_listBox.EndUpdate();
		}

		private string LevelName(Level level)         
		{             
			switch (level)             
			{                 
				case Level.Critical: 
					return "Critical";                 
				case Level.Error: 
					return "Error";                 
				case Level.Warning: 
					return "Warning";                 
				case Level.Info: 
					return "Info";                 
				case Level.Verbose: 
					return "Verbose";                 
				case Level.Debug: 
					return "Debug";                 
				default: 
					return string.Format("<value={0}>", (int)level);             
			}
		}

		private string FormatALogEventMessage(LogEvent logEvent, string messageFormat)
		{
			string message = logEvent.Message;             
			
			if (message == null) 
			{ 
				message = "<NULL>"; 
			}

			return string.Format(messageFormat,                 
				/* {0} */ logEvent.EventTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),                 
				/* {1} */ logEvent.EventTime.ToString("yyyy-MM-dd HH:mm:ss"),                 
				/* {2} */ logEvent.EventTime.ToString("yyyy-MM-dd"),                 
				/* {3} */ logEvent.EventTime.ToString("HH:mm:ss.fff"),                 
				/* {4} */ logEvent.EventTime.ToString("HH:mm:ss"),                  
				/* {5} */ LevelName(logEvent.Level)[0],                 
				/* {6} */ LevelName(logEvent.Level),                 
				/* {7} */ (int)logEvent.Level,                  
				/* {8} */ message);
		}

        private void SelectAllItems()
        {
            _listBox.BeginUpdate();

            for (int i = 0; i < _listBox.Items.Count; i++)
            {
                _listBox.SetSelected(i, true);
            }

            _listBox.EndUpdate();

		}

		private void CopyToClipboard()
		{
			if (_listBox.SelectedItems.Count > 0)
			{
				StringBuilder selectedItemsAsRTFText = new StringBuilder();
				selectedItemsAsRTFText.AppendLine(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fcharset0 Courier;}}");
				selectedItemsAsRTFText.AppendLine(@"{\colortbl;\red255\green255\blue255;\red255\green0\blue0;\red218\green165\blue32;\red0\green128\blue0;\red0\green0\blue255;\red0\green0\blue0}");
				
				foreach (LogEvent logEvent in _listBox.SelectedItems)
				{
					selectedItemsAsRTFText.AppendFormat(@"{{\f0\fs16\chshdng0\chcbpat{0}\cb{0}\cf{1} ", (logEvent.Level == Level.Critical) ? 2 : 1, (logEvent.Level == Level.Critical) ? 1 : ((int)logEvent.Level > 5) ? 6 : ((int)logEvent.Level) + 1);
					selectedItemsAsRTFText.Append(FormatALogEventMessage(logEvent, _messageFormat));
					selectedItemsAsRTFText.AppendLine(@"\par}");
				}
				selectedItemsAsRTFText.AppendLine(@"}");
				System.Diagnostics.Debug.WriteLine(selectedItemsAsRTFText.ToString());
				Clipboard.SetData(DataFormats.Rtf, selectedItemsAsRTFText.ToString());
			}
		}

		public ListBoxLog(DoubleBufferedListBox listBox)
			: this(listBox, DEFAULT_MESSAGE_FORMAT, DEFAULT_MAX_LINES_IN_LISTBOX)
		{
		}

		public ListBoxLog(DoubleBufferedListBox listBox, string messageFormat)
			: this(listBox, messageFormat, DEFAULT_MAX_LINES_IN_LISTBOX)
		{
		}

		public ListBoxLog(DoubleBufferedListBox listBox, string messageFormat, int maxLinesInListbox)
		{
			_disposed = false;              
			_listBox = listBox;             
			_messageFormat = messageFormat;             
			_maxEntriesInListBox = maxLinesInListbox;              
			_paused = false;              
			_canAdd = listBox.IsHandleCreated;
            _font = new Font("Lucida Console", 8.25f, FontStyle.Regular);

			_listBox.SelectionMode = SelectionMode.MultiExtended;              
			
			_listBox.HandleCreated += OnHandleCreated;             
			_listBox.HandleDestroyed += OnHandleDestroyed;
            _listBox.MeasureItem += MeasureItemHandler;
			_listBox.DrawItem += DrawItemHandler;             
			_listBox.KeyDown += KeyDownHandler; 
             
			MenuItem[] menuItems = new MenuItem[]
			                       	{
										new MenuItem("Select &All", new EventHandler(SelectAllMenuOnClickHandler), Shortcut.CtrlA), 
			                       		new MenuItem("&Copy", new EventHandler(CopyMenuOnClickHandler), Shortcut.CtrlC),
										new MenuItem("C&lear", new EventHandler(ClearMenuOnClickHandler), Shortcut.CtrlL), 
			                       	};             
			_listBox.ContextMenu = new ContextMenu(menuItems);             
			_listBox.ContextMenu.Popup += new EventHandler(MenuPopupHandler);              
			_listBox.DrawMode = DrawMode.OwnerDrawVariable;
			
		}

		public void Log(string message)
		{
			Log(Level.Debug, message);
		}

		public void Log(string format, params object[] args)
		{
			Log(Level.Debug, (format == null) ? null : string.Format(format, args));
		}

		public void Log(Level level, string format, params object[] args)
		{
			Log(level, (format == null) ? null : string.Format(format, args));
		}

		public void Log(Level level, string message)
		{
			WriteEvent(new LogEvent(level, message));
		}

		public bool Paused
		{
			get { return _paused; }
			set
			{
				_paused = value;
				if (_paused)
				{
					_listBox.BeginUpdate();
				}
				else
				{
					_listBox.EndUpdate();
				}
			}
		}

		public void Refresh()
		{
			_listBox.Invalidate();
		}

		public void Clear()
		{
			_canAdd = false;
			_listBox.Items.Clear();
			_canAdd = true;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in _listBox.Items)
			{
				LogEvent logEvent = item as LogEvent;
				sb.AppendFormat("{0}\n", FormatALogEventMessage(logEvent, "{3} {8}"));
			}

			return sb.ToString();
		}

		~ListBoxLog()
		{
			if (!_disposed)
			{
				Dispose(false);                 
				_disposed = true;
			}
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				Dispose(true);                 
				GC.SuppressFinalize(this);                 
				_disposed = true;
			}
		}

		private void Dispose(bool disposing)
		{
			if (_listBox != null)
			{
				_canAdd = false;                  
				_listBox.HandleCreated -= OnHandleCreated;                 
				_listBox.HandleCreated -= OnHandleDestroyed;                 
				_listBox.DrawItem -= DrawItemHandler;                 
				_listBox.KeyDown -= KeyDownHandler;                  
				_listBox.ContextMenu.MenuItems.Clear();                 
				_listBox.ContextMenu.Popup -= MenuPopupHandler;                 
				_listBox.ContextMenu = null;                  
				_listBox.Items.Clear();                
				_listBox.DrawMode = DrawMode.Normal;                 
				_listBox = null;
			}
		}
	}
}

