namespace iPlaylistToM3U
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.tbLibraryXmlPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpenLibraryXml = new System.Windows.Forms.Button();
			this.btnReadLibraryXml = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tvPlaylist = new System.Windows.Forms.TreeView();
			this.ilPlaylist = new System.Windows.Forms.ImageList(this.components);
			this.tbTracks = new System.Windows.Forms.TextBox();
			this.ofdLibraryImport = new System.Windows.Forms.OpenFileDialog();
			this.bgwCopy = new System.ComponentModel.BackgroundWorker();
			this.btnCopy = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbCopyTarget = new System.Windows.Forms.TextBox();
			this.btnOpenCopyTarget = new System.Windows.Forms.Button();
			this.ofdTargetFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.btnCancelCopy = new System.Windows.Forms.Button();
			this.cmbExportFileType = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbLibraryXmlPath
			// 
			this.tbLibraryXmlPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLibraryXmlPath.Location = new System.Drawing.Point(113, 14);
			this.tbLibraryXmlPath.Name = "tbLibraryXmlPath";
			this.tbLibraryXmlPath.Size = new System.Drawing.Size(389, 19);
			this.tbLibraryXmlPath.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "ライブラリ.xmlのパス";
			// 
			// btnOpenLibraryXml
			// 
			this.btnOpenLibraryXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenLibraryXml.Location = new System.Drawing.Point(508, 12);
			this.btnOpenLibraryXml.Name = "btnOpenLibraryXml";
			this.btnOpenLibraryXml.Size = new System.Drawing.Size(23, 23);
			this.btnOpenLibraryXml.TabIndex = 2;
			this.btnOpenLibraryXml.Text = "...";
			this.btnOpenLibraryXml.UseVisualStyleBackColor = true;
			this.btnOpenLibraryXml.Click += new System.EventHandler(this.btnOpenLibraryXml_Click);
			// 
			// btnReadLibraryXml
			// 
			this.btnReadLibraryXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReadLibraryXml.Location = new System.Drawing.Point(537, 12);
			this.btnReadLibraryXml.Name = "btnReadLibraryXml";
			this.btnReadLibraryXml.Size = new System.Drawing.Size(75, 23);
			this.btnReadLibraryXml.TabIndex = 3;
			this.btnReadLibraryXml.Text = "読み込み";
			this.btnReadLibraryXml.UseVisualStyleBackColor = true;
			this.btnReadLibraryXml.Click += new System.EventHandler(this.btnReadLibraryXml_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.splitContainer1);
			this.panel1.Location = new System.Drawing.Point(12, 39);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(600, 365);
			this.panel1.TabIndex = 4;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tvPlaylist);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tbTracks);
			this.splitContainer1.Size = new System.Drawing.Size(600, 365);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.TabIndex = 1;
			// 
			// tvPlaylist
			// 
			this.tvPlaylist.CheckBoxes = true;
			this.tvPlaylist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvPlaylist.ImageIndex = 0;
			this.tvPlaylist.ImageList = this.ilPlaylist;
			this.tvPlaylist.Location = new System.Drawing.Point(0, 0);
			this.tvPlaylist.Name = "tvPlaylist";
			this.tvPlaylist.SelectedImageIndex = 0;
			this.tvPlaylist.Size = new System.Drawing.Size(200, 365);
			this.tvPlaylist.TabIndex = 0;
			this.tvPlaylist.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvPlaylist_AfterCheck);
			this.tvPlaylist.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvPlaylist_AfterCollapse);
			this.tvPlaylist.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvPlaylist_AfterExpand);
			this.tvPlaylist.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPlaylist_AfterSelect);
			// 
			// ilPlaylist
			// 
			this.ilPlaylist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilPlaylist.ImageStream")));
			this.ilPlaylist.TransparentColor = System.Drawing.Color.Transparent;
			this.ilPlaylist.Images.SetKeyName(0, "Special.png");
			this.ilPlaylist.Images.SetKeyName(1, "Playlist.png");
			this.ilPlaylist.Images.SetKeyName(2, "Folder.png");
			this.ilPlaylist.Images.SetKeyName(3, "SmartPlaylist.png");
			this.ilPlaylist.Images.SetKeyName(4, "Special.png");
			// 
			// tbTracks
			// 
			this.tbTracks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbTracks.Location = new System.Drawing.Point(0, 0);
			this.tbTracks.Multiline = true;
			this.tbTracks.Name = "tbTracks";
			this.tbTracks.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbTracks.Size = new System.Drawing.Size(396, 365);
			this.tbTracks.TabIndex = 0;
			// 
			// ofdLibraryImport
			// 
			this.ofdLibraryImport.DefaultExt = "xml";
			this.ofdLibraryImport.FileName = "ライブラリ.xml";
			this.ofdLibraryImport.Filter = "XMLファイル|*.xml|すべてのファイル(*.*)|*.*";
			// 
			// bgwCopy
			// 
			this.bgwCopy.WorkerReportsProgress = true;
			this.bgwCopy.WorkerSupportsCancellation = true;
			this.bgwCopy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCopy_DoWork);
			this.bgwCopy.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCopy_ProgressChanged);
			this.bgwCopy.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCopy_RunWorkerCompleted);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(537, 410);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 8;
			this.btnCopy.Text = "コピー開始";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblStatus.Location = new System.Drawing.Point(12, 439);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(519, 23);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.Text = "ステータス";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 415);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "コピー先";
			// 
			// tbCopyTarget
			// 
			this.tbCopyTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbCopyTarget.Location = new System.Drawing.Point(62, 412);
			this.tbCopyTarget.Name = "tbCopyTarget";
			this.tbCopyTarget.Size = new System.Drawing.Size(337, 19);
			this.tbCopyTarget.TabIndex = 6;
			// 
			// btnOpenCopyTarget
			// 
			this.btnOpenCopyTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenCopyTarget.Location = new System.Drawing.Point(405, 410);
			this.btnOpenCopyTarget.Name = "btnOpenCopyTarget";
			this.btnOpenCopyTarget.Size = new System.Drawing.Size(23, 23);
			this.btnOpenCopyTarget.TabIndex = 7;
			this.btnOpenCopyTarget.Text = "...";
			this.btnOpenCopyTarget.UseVisualStyleBackColor = true;
			this.btnOpenCopyTarget.Click += new System.EventHandler(this.btnOpenCopyTarget_Click);
			// 
			// ofdTargetFolder
			// 
			this.ofdTargetFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// btnCancelCopy
			// 
			this.btnCancelCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelCopy.Enabled = false;
			this.btnCancelCopy.Location = new System.Drawing.Point(537, 439);
			this.btnCancelCopy.Name = "btnCancelCopy";
			this.btnCancelCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCancelCopy.TabIndex = 9;
			this.btnCancelCopy.Text = "コピー中止";
			this.btnCancelCopy.UseVisualStyleBackColor = true;
			this.btnCancelCopy.Click += new System.EventHandler(this.btnCancelCopy_Click);
			// 
			// cmbExportFileType
			// 
			this.cmbExportFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbExportFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbExportFileType.FormattingEnabled = true;
			this.cmbExportFileType.Items.AddRange(new object[] {
            "VLC(*.xspf)",
            "汎用(*.m3u)"});
			this.cmbExportFileType.Location = new System.Drawing.Point(434, 412);
			this.cmbExportFileType.Name = "cmbExportFileType";
			this.cmbExportFileType.Size = new System.Drawing.Size(97, 20);
			this.cmbExportFileType.TabIndex = 10;
			this.cmbExportFileType.SelectedIndexChanged += new System.EventHandler(this.cmbExportFileType_SelectedIndexChanged);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 471);
			this.Controls.Add(this.cmbExportFileType);
			this.Controls.Add(this.btnCancelCopy);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnReadLibraryXml);
			this.Controls.Add(this.btnOpenCopyTarget);
			this.Controls.Add(this.btnOpenLibraryXml);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbCopyTarget);
			this.Controls.Add(this.tbLibraryXmlPath);
			this.Name = "FormMain";
			this.Text = "iPlaylistToM3U";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLibraryXmlPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenLibraryXml;
        private System.Windows.Forms.Button btnReadLibraryXml;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvPlaylist;
        private System.Windows.Forms.ImageList ilPlaylist;
		private System.Windows.Forms.OpenFileDialog ofdLibraryImport;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox tbTracks;
		private System.ComponentModel.BackgroundWorker bgwCopy;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbCopyTarget;
		private System.Windows.Forms.Button btnOpenCopyTarget;
		private System.Windows.Forms.FolderBrowserDialog ofdTargetFolder;
		private System.Windows.Forms.Button btnCancelCopy;
		private System.Windows.Forms.ComboBox cmbExportFileType;
	}
}

