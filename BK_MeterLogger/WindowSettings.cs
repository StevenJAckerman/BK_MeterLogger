using System;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace BK_MeterLogger
{
    /* Author: Don Kirkby http://donkirkby.googlecode.com/
     * Released under the MIT licence http://www.opensource.org/licenses/mit-license.php
     * Installation:
     * - Copy this file into your project.
     * - Change the namespace above to match your project's namespace.
     * - Compile your project.
     * - Edit the project properties using the Project Properties... item in 
     * the project menu.
     * - Go to the settings tab.
     * - Add a new setting for each form whose position you want to save, and 
     * type a name for it like MainFormPosition.
     * - In the type column, select Browse... from the bottom of the list.
     * - You won't see WindowSettings in the list, but you can just type the
     * namespace and class name, and click OK. For example, if you changed this
     * class's namespace to UltimateApp, then you would type 
     * UltimateApp.WindowSettings and click OK.
     * - Add Load and FormClosing event handlers to any forms you want to save.
     * See the forms in this project for example code.
     * - Add a call to Settings.Default.Save() somewhere in your shutdown code.
     * The FormClosed event of your main form is a good spot. If you have 
     * subforms open, you may have to explicitly call their FormClosing events 
     * when shutting down the app, because they're not called by default.
     */
    /// <summary>
    /// Serializes a window's location, size, state, and any splitter 
    /// positions to a single application setting. Then you can just create a 
    /// setting of this type for each form in the application, save on close, 
    /// and restore on load.
    /// </summary>
    [Serializable()]
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class WindowSettings
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public FormWindowState WindowState { get; set; }

        public WindowSettings()
        {
            // default to an invalid location
            Location = new Point(Int32.MinValue, Int32.MinValue);
        }

        /// <summary>
        /// Record a form's position
        /// </summary>
        /// <param name="windowSettings">Where the settings should be recorded,
        /// or null.</param>
        /// <param name="form">The form to record.</param>
        /// <returns>The windowSettings parameter, or a new WindowSettings 
        /// object if that was null.</returns>
        public static WindowSettings Record(WindowSettings windowSettings, Form form)
        {
            if (windowSettings == null)
            {
                windowSettings = new WindowSettings();
            }
            windowSettings.Record(form);
            return windowSettings;
        }

        /// <summary>
        /// Record a form's position.
        /// </summary>
        /// <param name="form">The form to record. </param>
        public void Record(Form form)
        {
            switch (form.WindowState)
            {
                case FormWindowState.Maximized:
                    RecordWindowPosition(form.RestoreBounds);
                    break;
                case FormWindowState.Normal:
            		RecordWindowPosition(form.Bounds);
                    break;
                default:
                    // Don't record anything when closing while minimized.
                    return;
            }

            WindowState = form.WindowState;
        }

        /// <summary>
        /// Restore a form's position.
        /// </summary>
        /// <param name="windowSettings">Holds the settings to restore.</param>
        /// <param name="form">The form to restore. </param>
        public static void Restore(WindowSettings windowSettings, Form form)
        {
            if (windowSettings != null)
            {
                windowSettings.Restore(form);
            }
        }

        /// <summary>
        /// Restore a form's position.
        /// </summary>
        /// <param name="form">The form to restore.</param>
        public void Restore(Form form)
        {
            if (IsOnScreen(Location, Size))
            {
                form.Location = Location;
                form.Size = Size;
                form.WindowState = WindowState;
            }
            else
            {
                form.WindowState = WindowState;
            }
        }

        private bool RecordWindowPosition(Rectangle bounds)
        {
            bool isOnScreen = IsOnScreen(bounds.Location, bounds.Size);
            if (isOnScreen)
            {
                Location = bounds.Location;
                Size = bounds.Size;
            }
            return isOnScreen;
        }

        private bool IsOnScreen(Point location, Size size)
        {
            return IsOnScreen(location) && IsOnScreen(location + size);
        }

        private bool IsOnScreen(Point location)
        {
            foreach (var screen in Screen.AllScreens)
            {
                Rectangle workingArea = new Rectangle(
                    screen.WorkingArea.Location, 
                    screen.WorkingArea.Size);
                workingArea.Inflate(1, 1);
                if (workingArea.Contains(location))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
