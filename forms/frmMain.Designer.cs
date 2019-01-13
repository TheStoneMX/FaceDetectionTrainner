namespace RedBallTracker
{
    partial class frmMain
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
            this.tblOuter = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Eigne_threshold_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.tlpInner = new System.Windows.Forms.TableLayoutPanel();
            this.txtXYRadius = new System.Windows.Forms.TextBox();
            this.btnTraing = new System.Windows.Forms.Button();
            this.btnPauseOrResume = new System.Windows.Forms.Button();
            this.message_bar = new System.Windows.Forms.Label();
            this.Faces_Found_Panel = new System.Windows.Forms.Panel();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.workCorruptedImages = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxSplitChar = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.eqHisChecked = new System.Windows.Forms.CheckBox();
            this.captureButton = new System.Windows.Forms.Button();
            this.faceRecog = new System.Windows.Forms.CheckBox();
            this.comboBoxCapture = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tblOuter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            this.tlpInner.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblOuter
            // 
            this.tblOuter.ColumnCount = 3;
            this.tblOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1242F));
            this.tblOuter.Controls.Add(this.panel1, 2, 1);
            this.tblOuter.Controls.Add(this.ibOriginal, 0, 0);
            this.tblOuter.Controls.Add(this.tlpInner, 0, 1);
            this.tblOuter.Controls.Add(this.Faces_Found_Panel, 2, 0);
            this.tblOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOuter.Location = new System.Drawing.Point(0, 0);
            this.tblOuter.Name = "tblOuter";
            this.tblOuter.RowCount = 2;
            this.tblOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.93693F));
            this.tblOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.06307F));
            this.tblOuter.Size = new System.Drawing.Size(2602, 1871);
            this.tblOuter.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.Eigne_threshold_txtbx);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1366, 1089);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 776);
            this.panel1.TabIndex = 5;
            // 
            // Eigne_threshold_txtbx
            // 
            this.Eigne_threshold_txtbx.Location = new System.Drawing.Point(232, 12);
            this.Eigne_threshold_txtbx.Margin = new System.Windows.Forms.Padding(6);
            this.Eigne_threshold_txtbx.Name = "Eigne_threshold_txtbx";
            this.Eigne_threshold_txtbx.Size = new System.Drawing.Size(146, 31);
            this.Eigne_threshold_txtbx.TabIndex = 1;
            this.Eigne_threshold_txtbx.Text = "2000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unknown Theshold:";
            // 
            // ibOriginal
            // 
            this.ibOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblOuter.SetColumnSpan(this.ibOriginal, 2);
            this.ibOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibOriginal.Enabled = false;
            this.ibOriginal.Location = new System.Drawing.Point(3, 3);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(1354, 1077);
            this.ibOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // tlpInner
            // 
            this.tlpInner.ColumnCount = 2;
            this.tblOuter.SetColumnSpan(this.tlpInner, 2);
            this.tlpInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.79292F));
            this.tlpInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.20708F));
            this.tlpInner.Controls.Add(this.txtXYRadius, 1, 0);
            this.tlpInner.Controls.Add(this.message_bar, 1, 1);
            this.tlpInner.Controls.Add(this.btnPauseOrResume, 0, 0);
            this.tlpInner.Controls.Add(this.btnTraing, 0, 1);
            this.tlpInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInner.Location = new System.Drawing.Point(3, 1086);
            this.tlpInner.Name = "tlpInner";
            this.tlpInner.RowCount = 2;
            this.tlpInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tlpInner.Size = new System.Drawing.Size(1354, 782);
            this.tlpInner.TabIndex = 3;
            // 
            // txtXYRadius
            // 
            this.txtXYRadius.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXYRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXYRadius.Location = new System.Drawing.Point(298, 3);
            this.txtXYRadius.Multiline = true;
            this.txtXYRadius.Name = "txtXYRadius";
            this.txtXYRadius.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXYRadius.Size = new System.Drawing.Size(1053, 670);
            this.txtXYRadius.TabIndex = 1;
            // 
            // btnTraing
            // 
            this.btnTraing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTraing.AutoSize = true;
            this.btnTraing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTraing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTraing.Location = new System.Drawing.Point(3, 705);
            this.btnTraing.Name = "btnTraing";
            this.btnTraing.Size = new System.Drawing.Size(289, 47);
            this.btnTraing.TabIndex = 2;
            this.btnTraing.Text = "Train";
            this.btnTraing.UseVisualStyleBackColor = true;
            this.btnTraing.Click += new System.EventHandler(this.btnTraing_Click);
            // 
            // btnPauseOrResume
            // 
            this.btnPauseOrResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseOrResume.AutoSize = true;
            this.btnPauseOrResume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPauseOrResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPauseOrResume.Location = new System.Drawing.Point(3, 314);
            this.btnPauseOrResume.Name = "btnPauseOrResume";
            this.btnPauseOrResume.Size = new System.Drawing.Size(289, 47);
            this.btnPauseOrResume.TabIndex = 0;
            this.btnPauseOrResume.Text = "Pause ";
            this.btnPauseOrResume.UseVisualStyleBackColor = true;
            this.btnPauseOrResume.Click += new System.EventHandler(this.btnPauseOrResume_Click);
            // 
            // message_bar
            // 
            this.message_bar.AutoSize = true;
            this.message_bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.message_bar.Location = new System.Drawing.Point(301, 676);
            this.message_bar.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.message_bar.Name = "message_bar";
            this.message_bar.Size = new System.Drawing.Size(1047, 25);
            this.message_bar.TabIndex = 3;
            this.message_bar.Text = "Message:";
            // 
            // Faces_Found_Panel
            // 
            this.Faces_Found_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Faces_Found_Panel.AutoScroll = true;
            this.Faces_Found_Panel.Location = new System.Drawing.Point(1366, 6);
            this.Faces_Found_Panel.Margin = new System.Windows.Forms.Padding(6);
            this.Faces_Found_Panel.Name = "Faces_Found_Panel";
            this.Faces_Found_Panel.Size = new System.Drawing.Size(1230, 1071);
            this.Faces_Found_Panel.TabIndex = 4;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.workCorruptedImages);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBoxSplitChar);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBoxAlgorithm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.eqHisChecked);
            this.groupBox1.Controls.Add(this.captureButton);
            this.groupBox1.Controls.Add(this.faceRecog);
            this.groupBox1.Controls.Add(this.comboBoxCapture);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(19, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1108, 316);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // workCorruptedImages
            // 
            this.workCorruptedImages.AutoSize = true;
            this.workCorruptedImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.workCorruptedImages.Location = new System.Drawing.Point(686, 105);
            this.workCorruptedImages.Margin = new System.Windows.Forms.Padding(6);
            this.workCorruptedImages.Name = "workCorruptedImages";
            this.workCorruptedImages.Size = new System.Drawing.Size(456, 41);
            this.workCorruptedImages.TabIndex = 22;
            this.workCorruptedImages.Text = "Work with Corrupted Images";
            this.workCorruptedImages.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(294, 248);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 37);
            this.label8.TabIndex = 21;
            this.label8.Text = "Splith with";
            // 
            // comboBoxSplitChar
            // 
            this.comboBoxSplitChar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSplitChar.FormattingEnabled = true;
            this.comboBoxSplitChar.Items.AddRange(new object[] {
            "_",
            "0",
            "."});
            this.comboBoxSplitChar.Location = new System.Drawing.Point(456, 244);
            this.comboBoxSplitChar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxSplitChar.Name = "comboBoxSplitChar";
            this.comboBoxSplitChar.Size = new System.Drawing.Size(164, 45);
            this.comboBoxSplitChar.TabIndex = 20;
            this.comboBoxSplitChar.Text = "_";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(483, 161);
            this.button2.Margin = new System.Windows.Forms.Padding(6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 55);
            this.button2.TabIndex = 19;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(304, 161);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 55);
            this.button1.TabIndex = 18;
            this.button1.Text = "Train";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAlgorithm.FormattingEnabled = true;
            this.comboBoxAlgorithm.Items.AddRange(new object[] {
            "SURF Feature Extractor",
            "EigenFaces",
            "FisherFaces",
            "LBPHFaces"});
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(304, 98);
            this.comboBoxAlgorithm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(316, 45);
            this.comboBoxAlgorithm.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 37);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select Capture";
            // 
            // eqHisChecked
            // 
            this.eqHisChecked.AutoSize = true;
            this.eqHisChecked.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.eqHisChecked.Location = new System.Drawing.Point(686, 66);
            this.eqHisChecked.Margin = new System.Windows.Forms.Padding(6);
            this.eqHisChecked.Name = "eqHisChecked";
            this.eqHisChecked.Size = new System.Drawing.Size(327, 41);
            this.eqHisChecked.TabIndex = 12;
            this.eqHisChecked.Text = "Equalize Histogram";
            this.eqHisChecked.UseVisualStyleBackColor = true;
            // 
            // captureButton
            // 
            this.captureButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.captureButton.Location = new System.Drawing.Point(686, 161);
            this.captureButton.Margin = new System.Windows.Forms.Padding(6);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(250, 55);
            this.captureButton.TabIndex = 11;
            this.captureButton.Text = "Start Capture";
            this.captureButton.UseVisualStyleBackColor = true;
            // 
            // faceRecog
            // 
            this.faceRecog.AutoSize = true;
            this.faceRecog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.faceRecog.Location = new System.Drawing.Point(686, 25);
            this.faceRecog.Margin = new System.Windows.Forms.Padding(6);
            this.faceRecog.Name = "faceRecog";
            this.faceRecog.Size = new System.Drawing.Size(374, 41);
            this.faceRecog.TabIndex = 13;
            this.faceRecog.Text = "Start Face Recognition\r\n";
            this.faceRecog.UseVisualStyleBackColor = true;
            // 
            // comboBoxCapture
            // 
            this.comboBoxCapture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCapture.FormattingEnabled = true;
            this.comboBoxCapture.Items.AddRange(new object[] {
            "Camera",
            "Video",
            "Single Image",
            "Multi Image"});
            this.comboBoxCapture.Location = new System.Drawing.Point(16, 98);
            this.comboBoxCapture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxCapture.Name = "comboBoxCapture";
            this.comboBoxCapture.Size = new System.Drawing.Size(248, 45);
            this.comboBoxCapture.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(297, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 37);
            this.label3.TabIndex = 16;
            this.label3.Text = "Select Algorithm";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2602, 1871);
            this.Controls.Add(this.tblOuter);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PeopleCounter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tblOuter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            this.tlpInner.ResumeLayout(false);
            this.tlpInner.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblOuter;
        private Emgu.CV.UI.ImageBox ibOriginal;
        private System.Windows.Forms.TableLayoutPanel tlpInner;
        private System.Windows.Forms.Button btnPauseOrResume;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.TextBox txtXYRadius;
        private System.Windows.Forms.Button btnTraing;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Eigne_threshold_txtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Faces_Found_Panel;
        private System.Windows.Forms.Label message_bar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox workCorruptedImages;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxSplitChar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox eqHisChecked;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.CheckBox faceRecog;
        private System.Windows.Forms.ComboBox comboBoxCapture;
        private System.Windows.Forms.Label label3;
    }
}

