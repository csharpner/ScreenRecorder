﻿#region File Header
/*[ Compilation unit ----------------------------------------------------------
 
   Component       : ScreenRecorderMP
 
   Name            : ScreenRecorder.cs
 
  Author           : Sunil
 
-----------------------------------------------------------------------------*/
/*] END */
#endregion
#region Using directives
using log4net;
using ScreenRecorder;
using ScreenRecorder.Codecs;
using ScreenRecorder.ContentPages;
using ScreenRecorder.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenRecorder.Hooks;
#endregion

namespace ScreenRecorderMP
{
    /// <summary>
    /// Screen recorder MP
    /// </summary>
    public partial class ScreenRecorder : Form
    {
        /// <summary>
        /// Content pages
        /// </summary>
        List<IContentPage> contentPages = new List<IContentPage>();

        /// <summary>
        /// encoder
        /// </summary>
        AnimatedGifEncoder encoder = new AnimatedGifEncoder();

        /// <summary>
        /// Global keyboard shortcuts
        /// </summary>
        GlobalKeyboardHook globalKeyHook = new GlobalKeyboardHook();

        /// <summary>
        /// Last point
        /// </summary>
        private Point lastPoint;

        /// <summary>
        /// frames to be captured per secound
        /// </summary>
        private int fps = 1;

        /// <summary>
        /// is recording in progess
        /// </summary>
        private bool recoding = false;

        /// <summary>
        /// Save loc
        /// </summary>
        string saveLoc = string.Empty;

        ///// <summary>
        ///// Index
        ///// </summary>
        private int index = 0;

        /// <summary>
        /// hooks
        /// </summary>
        List<HookData> hooks = null;

        private static ILog log = LogManager.GetLogger(typeof(ScreenRecorder).Name);

        /// <summary>
        /// Initializes a new instance of the ScreenRecorderMP class.
        /// </summary>
        public ScreenRecorder()
        {
            ReadUserSettings();
            InitializeComponent();
            InitCP();
            SetupShortcuts();
        }

        /// <summary>
        /// keyboard shortcuts
        /// </summary>
        private void SetupShortcuts()
        {
            globalKeyHook.HookedKeys.Add(Keys.LControlKey); //record/pause
            globalKeyHook.HookedKeys.Add(Keys.RControlKey); //stop

            globalKeyHook.KeyUp += globalKeyHook_KeyUp;
            globalKeyHook.Hook();
        }

        /// <summary>
        /// event handlers for keyboard shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void globalKeyHook_KeyUp(object sender, KeyEventArgs e)
        {
            log.Info("Key Up " + e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.RControlKey:
                    {
                        this.stopBtn_Click(this, EventArgs.Empty);
                    }
                    break;
                case Keys.LControlKey:
                    {
                        this.recordBtn_Click(this, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }

        }
        
        /// <summary>
        /// Read user app settings
        /// </summary>
        private void ReadUserSettings()
        {
            this.Opacity = (double)Settings.Default.Opacity;
            saveLoc = Settings.Default.BitmapTempLoc;
            if (string.IsNullOrEmpty(saveLoc))
            {
                saveLoc = Path.GetTempPath();
                Settings.Default.BitmapTempLoc = saveLoc;
            }

            //create dir if necessary
            if (!Directory.Exists(saveLoc))
            {
                try
                {
                    Directory.CreateDirectory(saveLoc);
                }
                catch (Exception e)
                {
                    log.Error("unable to create dir", e);
                    log.Info("Setting save loction to temp dir");
                    saveLoc = Path.GetTempPath();
                    Settings.Default.BitmapTempLoc = saveLoc;
                }
            }

            HookParser parser = new HookParser();
            hooks = parser.Parse();

            //retrive the user stored fps from the settings file config.
            fps = Settings.Default.FramesPerSec;
        }

        /// <summary>
        /// Init CP
        /// </summary>
        private void InitCP()
        {
            //TODO: setup CP's from reading from Xmls
            //Design: Dynamic versus static Cp's ?
            contentPages.Add(new InformationCP());
            contentPages.Add(new AppSettings());
        }

        /// <summary>
        /// Close btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        /// <summary>
        /// Save config
        /// </summary>
        private void SaveConfig()
        {
            Settings.Default.Save();
        }

        /// <summary>
        /// Maximum btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void maxBtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                this.maxBtn.Image = Resources.MaximizeMinus;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.maxBtn.Image = Resources.MaximizePlus;
            }
        }

        /// <summary>
        /// Minimum btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void minBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// On mouse down
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.WindowState != FormWindowState.Maximized)
            {
                lastPoint = new Point(e.X, e.Y);
                this.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// Wnd proc for handling form resize
        /// </summary>
        /// <param name="m">M</param>
        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;
            const UInt32 HTBOTTOMRIGHT = 17;
            const int RESIZE_HANDLE_SIZE = 10;
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);
                Rectangle hitBox = new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE);
                if (hitBox.Contains(clientPoint))
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    handled = true;
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }

        /// <summary>
        /// On mouse move
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - lastPoint.X, p2.Y - lastPoint.Y);

                this.Location = p3;
            }
        }

        /// <summary>
        /// On mouse up
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Info btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void infoBtn_Click(object sender, EventArgs e)
        {
            ActivatePage(contentPages[0]);
        }

        /// <summary>
        /// Activate page
        /// </summary>
        /// <param name="page">Page</param>
        private void ActivatePage(IContentPage page)
        {
            Control control = page as Control;
            control.Dock = DockStyle.Fill;

            if (screenViewMP.Controls.Contains(control))
            {
                screenViewMP.Controls.Remove(control);
                //this.Opacity = .9D;
                this.TransparencyKey = Color.Black;
            }
            else
            {
                screenViewMP.Controls.Clear();

                screenViewMP.Controls.Add(control);
                //this.Opacity = 1;
                this.TransparencyKey = Color.Red;
            }
            this.Invalidate();
        }

        /// <summary>
        /// Setting btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void settingBtn_Click(object sender, EventArgs e)
        {
            ActivatePage(contentPages[1]);
        }

        /// <summary>
        /// Frame capture timer tick
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void frameCaptureTimer_Tick(object sender, EventArgs e)
        {
            string fileName = String.Format("{1}\\img{0}.png", index++, saveLoc);

            //use the location of the form (x,y)
            Point leftPt = //new Point(this.Location.X, this.Location.Y);
                new Point(this.screenViewMP.Location.X, this.screenViewMP.Location.Y);

            using (Bitmap bmp = new Bitmap(this.screenViewMP.Bounds.Size.Width, this.screenViewMP.Bounds.Size.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(leftPt.X, leftPt.Y, 0, 0, this.screenViewMP.Bounds.Size, CopyPixelOperation.SourceCopy);

                    PCURSORINFO cinfo = new PCURSORINFO
                    {
                        Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(PCURSORINFO))
                    };

                    if (Win32Api.GetCursorInfo(out cinfo))
                    {
                        if (cinfo.Flags == Win32Api.CURSOR_SHOWING)
                        {
                            Win32Api.DrawIcon(g.GetHdc(), cinfo.ScreenPos.x, cinfo.ScreenPos.y, cinfo.Cursor);
                            g.ReleaseHdc();
                        }
                    }
                }

                try
                {
                    //encoder.AddFrame(bmp);
                    bmp.Save(fileName);
                }
                catch (Exception ex)
                {
                    log.Error("unable to save bitmap", ex);
                }
            }
            
        }

        /// <summary>
        /// Record btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void recordBtn_Click(object sender, EventArgs e)
        {
            if (recoding)
            {
                //pause the recording
                NotifyUser(Resources.RecordPause);
                frameCaptureTimer.Stop();

                recoding = false;
                recordBtn.Image = Resources.Record;
                this.toolTip.SetToolTip(this.recordBtn, global::ScreenRecorder.Properties.Resources.RecordToolTip);
            }
            else
            {
                screenViewMP.Controls.Clear();
                this.TransparencyKey = Color.Black;

                //encoder.Start(saveLoc + "\\rec.gif");

                NotifyUser(Resources.RecordStarted);

                //encoder.SetDelay(50);
                //encoder.SetRepeat(0);

                frameCaptureTimer.Interval = (int)(1000 / fps);
                frameCaptureTimer.Start();

                recoding = true;
                recordBtn.Image = Resources.Pause;
                this.toolTip.SetToolTip(this.recordBtn, global::ScreenRecorder.Properties.Resources.PauseToolTip);
            }
        }

        /// <summary>
        /// Notify user
        /// </summary>
        /// <param name="message">Message</param>
        private void NotifyUser(string message)
        {
            new Task(() => NotifyUserTask(message)).Start();
        }

        /// <summary>
        /// Notify user task. Don't call this method use NotifyUser(String message);
        /// </summary>
        private void NotifyUserTask(string message)
        {
            Point p = new Point(this.Location.X + this.Width / 3, this.Location.Y + this.Height / 3);

            NotificationForm notifyUser = new NotificationForm()
            {
                /*set the location of the user notification*/
                StartPosition = FormStartPosition.Manual,
                Location = p
            };

            notifyUser.Popup(message);
        }

        /// <summary>
        /// Stop btn click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void stopBtn_Click(object sender, EventArgs e)
        {
            frameCaptureTimer.Stop();

            //reset the index
            index = 0;

            recoding = false;
            recordBtn.Image = Resources.Record;
            this.toolTip.SetToolTip(this.recordBtn, global::ScreenRecorder.Properties.Resources.RecordToolTip);
            
            //encoder.Finish();
           
            NotifyUser(Resources.RecordStopped);
            
            //TODO : capture audio and embed with video file. check out NAudio from www.Codeplex.com

            //run the suitable hook command
            foreach (HookData hook in hooks)
            {
                if (string.Equals(hook.HookId, Settings.Default.VideoType))
                {
                    hook.Execute();
                    break;
                }
            }

            NotifyUser(Resources.MovieCreated);

            DeleteTempFiles();
        }

        /// <summary>
        /// Delete temporary bmp/png/etc files 
        /// </summary>
        private void DeleteTempFiles()
        {
            //delete all temp bitmaps
            foreach (string file in Directory.GetFiles(saveLoc))
            {
                try
                {
                    Regex regex = new Regex(@"img.*\.png");

                    if (regex.Match(file).Success) File.Delete(file);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Hide button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideBtn_Click(object sender, EventArgs e)
        {
            //Hide ConfigMP and resize screenViewMP
            if (!configMP.Visible)
            {
                if (configMP.Tag == null)
                    configMP.Tag = configMP.Width;
                if (titlePanel.Tag == null)
                    titlePanel.Tag = titlePanel.Height;

                screenViewMP.Location = (Point)screenViewMP.Tag;
                screenViewMP.Height -= (int)titlePanel.Tag;
                screenViewMP.Width -= (int)configMP.Tag;

                hideBtn.Image = Resources.RightSide;
                configMP.Show();
                titlePanel.Show();
            }
            else
            {
                hideBtn.Image = Resources.LeftSide;
                configMP.Hide();
                titlePanel.Hide();

                if (configMP.Tag == null)
                    configMP.Tag = configMP.Width;
                if (titlePanel.Tag == null)
                    titlePanel.Tag = titlePanel.Height;

                screenViewMP.Tag = screenViewMP.Location;
                screenViewMP.Location = titlePanel.Location;

                screenViewMP.Height += (int)titlePanel.Tag;
                screenViewMP.Width += (int)configMP.Tag;
            }
        }

        /// <summary>
        /// Reset the application settings to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetSettingBtn_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
        }
    } // class ScreenRecorderMP
} // namespace ScreenRecorderMP
