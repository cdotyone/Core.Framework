namespace PO.Core.WindowsService
{
    partial class ServicesController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer _components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesController));
            this._btnExit = new System.Windows.Forms.Button();
            this._lvServices = new System.Windows.Forms.ListView();
            this._chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btnPlayPause = new System.Windows.Forms.ToolStripButton();
            this._btnStop = new System.Windows.Forms.ToolStripButton();
            this._btnRestart = new System.Windows.Forms.ToolStripButton();
            this._rememberPosition = new PO.Core.WindowsService.RememberPosition(this.components);
            this._btnSettings = new System.Windows.Forms.ToolStripButton();
            this._toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnExit
            // 
            this._btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._btnExit.Location = new System.Drawing.Point(164, 161);
            this._btnExit.Name = "_btnExit";
            this._btnExit.Size = new System.Drawing.Size(75, 23);
            this._btnExit.TabIndex = 2;
            this._btnExit.Text = "Exit";
            this._btnExit.UseVisualStyleBackColor = true;
            this._btnExit.Click += new System.EventHandler(this.BtnExitClick);
            // 
            // _lvServices
            // 
            this._lvServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lvServices.CheckBoxes = true;
            this._lvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._chName,
            this._chStatus});
            this._lvServices.Location = new System.Drawing.Point(12, 34);
            this._lvServices.MultiSelect = false;
            this._lvServices.Name = "_lvServices";
            this._lvServices.Size = new System.Drawing.Size(380, 121);
            this._lvServices.TabIndex = 3;
            this._lvServices.UseCompatibleStateImageBehavior = false;
            this._lvServices.View = System.Windows.Forms.View.Details;
            this._lvServices.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvServicesItemChecked);
            this._lvServices.SelectedIndexChanged += new System.EventHandler(this.LvServicesSelectedIndexChanged);
            // 
            // _chName
            // 
            this._chName.Text = "Name";
            this._chName.Width = 282;
            // 
            // _chStatus
            // 
            this._chStatus.Text = "Status";
            this._chStatus.Width = 90;
            // 
            // _toolStrip1
            // 
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnPlayPause,
            this._btnStop,
            this._btnRestart,
            this._btnSettings});
            this._toolStrip1.Location = new System.Drawing.Point(0, 0);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(404, 25);
            this._toolStrip1.TabIndex = 4;
            this._toolStrip1.Text = "_toolStrip1";
            // 
            // _btnPlayPause
            // 
            this._btnPlayPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnPlayPause.Enabled = false;
            this._btnPlayPause.Image = ((System.Drawing.Image)(resources.GetObject("_btnPlayPause.Image")));
            this._btnPlayPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnPlayPause.Name = "_btnPlayPause";
            this._btnPlayPause.Size = new System.Drawing.Size(23, 22);
            this._btnPlayPause.Text = "toolStripButton1";
            this._btnPlayPause.Click += new System.EventHandler(this.BtnPlayPauseClick);
            // 
            // _btnStop
            // 
            this._btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnStop.Enabled = false;
            this._btnStop.Image = ((System.Drawing.Image)(resources.GetObject("_btnStop.Image")));
            this._btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnStop.Name = "_btnStop";
            this._btnStop.Size = new System.Drawing.Size(23, 22);
            this._btnStop.Text = "toolStripButton2";
            this._btnStop.Click += new System.EventHandler(this.BtnStopClick);
            // 
            // _btnRestart
            // 
            this._btnRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnRestart.Enabled = false;
            this._btnRestart.Image = ((System.Drawing.Image)(resources.GetObject("_btnRestart.Image")));
            this._btnRestart.ImageTransparentColor = System.Drawing.Color.Black;
            this._btnRestart.Name = "_btnRestart";
            this._btnRestart.Size = new System.Drawing.Size(23, 22);
            this._btnRestart.Text = "toolStripButton3";
            this._btnRestart.Click += new System.EventHandler(this.BtnRestartClick);
            // 
            // _btnSettings
            // 
            this._btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("_btnSettings.Image")));
            this._btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnSettings.Name = "btnSettings";
            this._btnSettings.Size = new System.Drawing.Size(23, 22);
            this._btnSettings.Text = "toolStripButton1";
            this._btnSettings.Visible = false;
            this._btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // ServicesController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 194);
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._lvServices);
            this.Controls.Add(this._btnExit);
            this.Name = "ServicesController";
            this._rememberPosition.SetRegistryKey(this, "Software\\PO\\POKE.Test.TestHost");
            this.Text = "Services Controller";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServicesControllerFormClosed);
            this.Load += new System.EventHandler(this.ServicesControllerLoad);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnExit;
        private System.Windows.Forms.ListView _lvServices;
        private System.Windows.Forms.ColumnHeader _chName;
        private System.Windows.Forms.ColumnHeader _chStatus;
        private System.Windows.Forms.ToolStrip _toolStrip1;
        private System.Windows.Forms.ToolStripButton _btnPlayPause;
        private System.Windows.Forms.ToolStripButton _btnStop;
        private System.Windows.Forms.ToolStripButton _btnRestart;
        private RememberPosition _rememberPosition;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripButton _btnSettings;

    }
}