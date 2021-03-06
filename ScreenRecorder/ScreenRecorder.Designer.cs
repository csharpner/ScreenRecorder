﻿namespace ScreenRecorderMP
{
    partial class ScreenRecorder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            globalKeyHook.HookedKeys.Clear();
            globalKeyHook.KeyUp -= globalKeyHook_KeyUp;
            globalKeyHook.Unhook();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenRecorder));
            this.titleLbl = new System.Windows.Forms.Label();
            this.screenViewMP = new System.Windows.Forms.Panel();
            this.configMP = new System.Windows.Forms.Panel();
            this.resetSettingBtn = new System.Windows.Forms.Button();
            this.infoBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.recordBtn = new System.Windows.Forms.Button();
            this.settingBtn = new System.Windows.Forms.Button();
            this.frameCaptureTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.maxBtn = new System.Windows.Forms.Button();
            this.minBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.hideBtn = new System.Windows.Forms.Button();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.configMP.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.BackColor = System.Drawing.Color.Transparent;
            this.titleLbl.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.titleLbl.Location = new System.Drawing.Point(87, 8);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(126, 16);
            this.titleLbl.TabIndex = 3;
            this.titleLbl.Text = "Screen Recorder";
            // 
            // screenViewMP
            // 
            this.screenViewMP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.screenViewMP.AutoSize = true;
            this.screenViewMP.BackColor = System.Drawing.Color.Black;
            this.screenViewMP.Location = new System.Drawing.Point(3, 33);
            this.screenViewMP.Name = "screenViewMP";
            this.screenViewMP.Size = new System.Drawing.Size(748, 494);
            this.screenViewMP.TabIndex = 4;
            // 
            // configMP
            // 
            this.configMP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.configMP.BackColor = System.Drawing.Color.Transparent;
            this.configMP.Controls.Add(this.resetSettingBtn);
            this.configMP.Controls.Add(this.infoBtn);
            this.configMP.Controls.Add(this.stopBtn);
            this.configMP.Controls.Add(this.recordBtn);
            this.configMP.Controls.Add(this.settingBtn);
            this.configMP.Location = new System.Drawing.Point(753, 30);
            this.configMP.Name = "configMP";
            this.configMP.Size = new System.Drawing.Size(38, 497);
            this.configMP.TabIndex = 5;
            this.configMP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.configMP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.configMP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // resetSettingBtn
            // 
            this.resetSettingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetSettingBtn.FlatAppearance.BorderSize = 0;
            this.resetSettingBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.resetSettingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetSettingBtn.Image = global::ScreenRecorder.Properties.Resources.Reset;
            this.resetSettingBtn.Location = new System.Drawing.Point(4, 154);
            this.resetSettingBtn.Name = "resetSettingBtn";
            this.resetSettingBtn.Size = new System.Drawing.Size(30, 34);
            this.resetSettingBtn.TabIndex = 5;
            this.toolTip.SetToolTip(this.resetSettingBtn, global::ScreenRecorder.Properties.Resources.StopToolTip);
            this.resetSettingBtn.UseVisualStyleBackColor = true;
            this.resetSettingBtn.Click += new System.EventHandler(this.resetSettingBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoBtn.FlatAppearance.BorderSize = 0;
            this.infoBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.infoBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infoBtn.Image = global::ScreenRecorder.Properties.Resources.Help;
            this.infoBtn.Location = new System.Drawing.Point(4, 39);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(30, 33);
            this.infoBtn.TabIndex = 0;
            this.toolTip.SetToolTip(this.infoBtn, global::ScreenRecorder.Properties.Resources.HelpToolTip);
            this.infoBtn.UseVisualStyleBackColor = true;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopBtn.FlatAppearance.BorderSize = 0;
            this.stopBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBtn.Image = global::ScreenRecorder.Properties.Resources.Stop;
            this.stopBtn.Location = new System.Drawing.Point(4, 114);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(30, 34);
            this.stopBtn.TabIndex = 4;
            this.toolTip.SetToolTip(this.stopBtn, global::ScreenRecorder.Properties.Resources.StopToolTip);
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // recordBtn
            // 
            this.recordBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.recordBtn.FlatAppearance.BorderSize = 0;
            this.recordBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.recordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recordBtn.Image = ((System.Drawing.Image)(resources.GetObject("recordBtn.Image")));
            this.recordBtn.Location = new System.Drawing.Point(4, 76);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(30, 34);
            this.recordBtn.TabIndex = 3;
            this.toolTip.SetToolTip(this.recordBtn, global::ScreenRecorder.Properties.Resources.RecordToolTip);
            this.recordBtn.UseVisualStyleBackColor = true;
            this.recordBtn.Click += new System.EventHandler(this.recordBtn_Click);
            // 
            // settingBtn
            // 
            this.settingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingBtn.BackColor = System.Drawing.Color.Transparent;
            this.settingBtn.FlatAppearance.BorderSize = 0;
            this.settingBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.settingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingBtn.Image")));
            this.settingBtn.Location = new System.Drawing.Point(4, 3);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(30, 32);
            this.settingBtn.TabIndex = 2;
            this.toolTip.SetToolTip(this.settingBtn, global::ScreenRecorder.Properties.Resources.SettingsToolTip);
            this.settingBtn.UseVisualStyleBackColor = false;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // frameCaptureTimer
            // 
            this.frameCaptureTimer.Interval = 50;
            this.frameCaptureTimer.Tick += new System.EventHandler(this.frameCaptureTimer_Tick);
            // 
            // toolTip
            // 
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            // 
            // maxBtn
            // 
            this.maxBtn.BackColor = System.Drawing.Color.Transparent;
            this.maxBtn.FlatAppearance.BorderSize = 0;
            this.maxBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.maxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxBtn.ForeColor = System.Drawing.Color.Transparent;
            this.maxBtn.Image = ((System.Drawing.Image)(resources.GetObject("maxBtn.Image")));
            this.maxBtn.Location = new System.Drawing.Point(31, 3);
            this.maxBtn.Name = "maxBtn";
            this.maxBtn.Size = new System.Drawing.Size(22, 23);
            this.maxBtn.TabIndex = 2;
            this.toolTip.SetToolTip(this.maxBtn, global::ScreenRecorder.Properties.Resources.MaximizeToolTip);
            this.maxBtn.UseVisualStyleBackColor = false;
            this.maxBtn.Click += new System.EventHandler(this.maxBtn_Click);
            // 
            // minBtn
            // 
            this.minBtn.BackColor = System.Drawing.Color.Transparent;
            this.minBtn.FlatAppearance.BorderSize = 0;
            this.minBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.minBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minBtn.ForeColor = System.Drawing.Color.Transparent;
            this.minBtn.Image = ((System.Drawing.Image)(resources.GetObject("minBtn.Image")));
            this.minBtn.Location = new System.Drawing.Point(59, 3);
            this.minBtn.Name = "minBtn";
            this.minBtn.Size = new System.Drawing.Size(22, 23);
            this.minBtn.TabIndex = 1;
            this.toolTip.SetToolTip(this.minBtn, global::ScreenRecorder.Properties.Resources.MinimizeToolTip);
            this.minBtn.UseVisualStyleBackColor = false;
            this.minBtn.Click += new System.EventHandler(this.minBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.ForeColor = System.Drawing.Color.Transparent;
            this.closeBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeBtn.Image")));
            this.closeBtn.Location = new System.Drawing.Point(3, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(22, 23);
            this.closeBtn.TabIndex = 0;
            this.toolTip.SetToolTip(this.closeBtn, global::ScreenRecorder.Properties.Resources.CloseToolTip);
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // hideBtn
            // 
            this.hideBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.hideBtn.FlatAppearance.BorderSize = 0;
            this.hideBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.hideBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.hideBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtn.Image = global::ScreenRecorder.Properties.Resources.RightSide;
            this.hideBtn.Location = new System.Drawing.Point(791, 264);
            this.hideBtn.Name = "hideBtn";
            this.hideBtn.Size = new System.Drawing.Size(10, 23);
            this.hideBtn.TabIndex = 6;
            this.hideBtn.UseVisualStyleBackColor = true;
            this.hideBtn.Click += new System.EventHandler(this.hideBtn_Click);
            // 
            // titlePanel
            // 
            this.titlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel.Controls.Add(this.titleLbl);
            this.titlePanel.Controls.Add(this.closeBtn);
            this.titlePanel.Controls.Add(this.maxBtn);
            this.titlePanel.Controls.Add(this.minBtn);
            this.titlePanel.Location = new System.Drawing.Point(3, 4);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(798, 27);
            this.titlePanel.TabIndex = 7;
            this.titlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.titlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.titlePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // ScreenRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(803, 531);
            this.Controls.Add(this.titlePanel);
            this.Controls.Add(this.hideBtn);
            this.Controls.Add(this.configMP);
            this.Controls.Add(this.screenViewMP);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::ScreenRecorder.Properties.Resources.AVRecorder;
            this.Name = "ScreenRecorder";
            this.Opacity = 0.9D;
            this.Text = "ScreenRecorder";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.configMP.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button minBtn;
        private System.Windows.Forms.Button maxBtn;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Panel screenViewMP;
        private System.Windows.Forms.Panel configMP;
        private System.Windows.Forms.Button infoBtn;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.Timer frameCaptureTimer;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button recordBtn;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button hideBtn;
        private System.Windows.Forms.Button resetSettingBtn;
        private System.Windows.Forms.Panel titlePanel;
    }
}

