using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Flicker-Free Control Drawing:
// https://sites.google.com/site/craigandera/craigs-stuff/windows-forms/flicker-free-control-drawing

// ListBox Flicker
// http://yacsharpblog.blogspot.com/2008/07/listbox-flicker.html

namespace BK_MeterLogger
{
	/// <summary>
	/// This class is a double-buffered ListBox for owner drawing.
	/// The double-buffering is accomplished by creating a custom,
	/// off-screen buffer during painting.
	/// </summary>
	public sealed class DoubleBufferedListBox : ListBox
	{
		public DoubleBufferedListBox()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);
		}

		#region Method Overrides
		/// <summary>
		/// Override OnTemplateListDrawItem to supply an off-screen buffer to event
		/// handlers.
		/// </summary>
		[DllImport("gdi32.dll", EntryPoint = "BitBlt")]
	
		internal static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);

		public enum TernaryRasterOperations : uint
		{
			SRCCOPY = 0x00CC0020,
			SRCPAINT = 0x00EE0086,
			SRCAND = 0x008800C6,
			SRCINVERT = 0x00660046,
			SRCERASE = 0x00440328,
			NOTSRCCOPY = 0x00330008,
			NOTSRCERASE = 0x001100A6,
			MERGECOPY = 0x00C000CA,
			MERGEPAINT = 0x00BB0226,
			PATCOPY = 0x00F00021,
			PATPAINT = 0x00FB0A09,
			PATINVERT = 0x005A0049,
			DSTINVERT = 0x00550009,
			BLACKNESS = 0x00000042,
			WHITENESS = 0x00FF0062,
			CAPTUREBLT = 0x40000000 //only if WinVer >= 5.0.0 (see wingdi.h)
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;

			Rectangle newBounds = new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height);
			using (BufferedGraphics bufferedGraphics = currentContext.Allocate(e.Graphics, newBounds))
			{
				DrawItemEventArgs newArgs = new DrawItemEventArgs(
					bufferedGraphics.Graphics, e.Font, newBounds, e.Index, e.State, e.ForeColor, e.BackColor);

				// Supply the real OnTemplateListDrawItem with the off-screen graphics context
				base.OnDrawItem(newArgs);

				// Wrapper around BitBlt
				//GDI.CopyGraphics(e.Graphics, e.Bounds, bufferedGraphics.Graphics, new Point(0, 0));
				BitBlt(e.Graphics.GetHdc(), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height,
					bufferedGraphics.Graphics.GetHdc(), 0, 0, (int)TernaryRasterOperations.SRCCOPY);
				e.Graphics.ReleaseHdc();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Region iRegion = new Region(e.ClipRectangle);
			e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
			if (this.Items.Count > 0)
			{
				for (int i = 0; i < this.Items.Count; ++i)
				{
					System.Drawing.Rectangle irect = this.GetItemRectangle(i);
					if (e.ClipRectangle.IntersectsWith(irect))
					{
						if (	(this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
							||	(this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
							||	(this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
						{
							OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
								irect, i,
								DrawItemState.Selected, this.ForeColor, this.BackColor));
						}
						else
						{
							OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
								irect, i,
								DrawItemState.Default, this.ForeColor, this.BackColor));
						}
						iRegion.Complement(irect);
					}
				}
			}
			base.OnPaint(e);
		}
		#endregion
	}
}
