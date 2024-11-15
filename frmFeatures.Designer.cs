namespace parser
{
    partial class frmFeatures
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenUrl = new System.Windows.Forms.Button();
            this.lblCaption = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstTypes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tvFeatures = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.wb = new System.Windows.Forms.RichTextBox();
            this.lblPos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenUrl);
            this.panel1.Controls.Add(this.lblCaption);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1341, 90);
            this.panel1.TabIndex = 0;
            // 
            // btnOpenUrl
            // 
            this.btnOpenUrl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenUrl.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenUrl.Image = global::parser.Properties.Resources.uni_prot;
            this.btnOpenUrl.Location = new System.Drawing.Point(1052, 0);
            this.btnOpenUrl.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnOpenUrl.Name = "btnOpenUrl";
            this.btnOpenUrl.Size = new System.Drawing.Size(145, 90);
            this.btnOpenUrl.TabIndex = 3;
            this.btnOpenUrl.Text = "Open in Browser";
            this.btnOpenUrl.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOpenUrl.UseVisualStyleBackColor = true;
            this.btnOpenUrl.Click += new System.EventHandler(this.btnOpenUrl_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Padding = new System.Windows.Forms.Padding(5);
            this.lblCaption.Size = new System.Drawing.Size(943, 90);
            this.lblCaption.TabIndex = 2;
            this.lblCaption.Text = "label1";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(1197, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 90);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lstTypes);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 784);
            this.panel2.TabIndex = 6;
            // 
            // lstTypes
            // 
            this.lstTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTypes.FormattingEnabled = true;
            this.lstTypes.ItemHeight = 19;
            this.lstTypes.Location = new System.Drawing.Point(0, 24);
            this.lstTypes.Name = "lstTypes";
            this.lstTypes.Size = new System.Drawing.Size(244, 760);
            this.lstTypes.TabIndex = 2;
            this.lstTypes.SelectedIndexChanged += new System.EventHandler(this.lstTypes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "All Feature Types";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.tvFeatures);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(244, 90);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(463, 784);
            this.panel3.TabIndex = 8;
            // 
            // tvFeatures
            // 
            this.tvFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFeatures.HideSelection = false;
            this.tvFeatures.Location = new System.Drawing.Point(0, 24);
            this.tvFeatures.Margin = new System.Windows.Forms.Padding(4);
            this.tvFeatures.Name = "tvFeatures";
            this.tvFeatures.Size = new System.Drawing.Size(463, 760);
            this.tvFeatures.TabIndex = 3;
            this.tvFeatures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFeatures_AfterSelect);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(463, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Protein Features (select Type to highlight)";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.wb);
            this.panel4.Controls.Add(this.lblPos);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(707, 90);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(634, 784);
            this.panel4.TabIndex = 9;
            // 
            // wb
            // 
            this.wb.BackColor = System.Drawing.Color.White;
            this.wb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wb.Location = new System.Drawing.Point(0, 24);
            this.wb.Name = "wb";
            this.wb.ReadOnly = true;
            this.wb.Size = new System.Drawing.Size(634, 739);
            this.wb.TabIndex = 12;
            this.wb.Text = "";
            this.wb.Click += new System.EventHandler(this.wb_Click);
            this.wb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.wb_KeyPress);
            this.wb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.wb_KeyUp);
            // 
            // lblPos
            // 
            this.lblPos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPos.Location = new System.Drawing.Point(0, 763);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(634, 21);
            this.lblPos.TabIndex = 11;
            this.lblPos.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(634, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Protein Sequence";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 874);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "frmFeatures";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmFeatures";
            this.Shown += new System.EventHandler(this.frmFeatures_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Button btnOpenUrl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lstTypes;
        private System.Windows.Forms.TreeView tvFeatures;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox wb;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Timer timer1;
    }
}