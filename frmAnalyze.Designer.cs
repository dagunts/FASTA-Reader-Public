namespace parser
{
    partial class frmAnalyze
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstDistincts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnUnselect = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lstAll = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnExportHTML = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblExplanation);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.cmbOperator);
            this.panel1.Controls.Add(this.lblCaption);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1265, 90);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Click on grid columns below to sort the items";
            // 
            // lblExplanation
            // 
            this.lblExplanation.AutoSize = true;
            this.lblExplanation.Location = new System.Drawing.Point(292, 37);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(21, 19);
            this.lblExplanation.TabIndex = 4;
            this.lblExplanation.Text = "...";
            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmbOperator.Location = new System.Drawing.Point(182, 34);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(104, 27);
            this.cmbOperator.TabIndex = 3;
            this.cmbOperator.SelectedIndexChanged += new System.EventHandler(this.cmbOperator_SelectedIndexChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.Location = new System.Drawing.Point(12, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(779, 19);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "...";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Combine Results Using ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportHTML);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1005, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 90);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(136, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 90);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lstDistincts);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.lstAll);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 90);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1265, 224);
            this.panel4.TabIndex = 3;
            // 
            // lstDistincts
            // 
            this.lstDistincts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4});
            this.lstDistincts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDistincts.FullRowSelect = true;
            this.lstDistincts.GridLines = true;
            this.lstDistincts.HideSelection = false;
            this.lstDistincts.Location = new System.Drawing.Point(692, 0);
            this.lstDistincts.Margin = new System.Windows.Forms.Padding(4);
            this.lstDistincts.Name = "lstDistincts";
            this.lstDistincts.ShowItemToolTips = true;
            this.lstDistincts.Size = new System.Drawing.Size(573, 224);
            this.lstDistincts.TabIndex = 8;
            this.lstDistincts.UseCompatibleStateImageBehavior = false;
            this.lstDistincts.View = System.Windows.Forms.View.Details;
            this.lstDistincts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstDistincts_ColumnClick);
            this.lstDistincts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstDistincts_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Selected Pattern";
            this.columnHeader1.Width = 400;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Count";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnUnselect);
            this.panel3.Controls.Add(this.btnSelect);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(570, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(122, 224);
            this.panel3.TabIndex = 7;
            // 
            // btnUnselect
            // 
            this.btnUnselect.Location = new System.Drawing.Point(8, 124);
            this.btnUnselect.Margin = new System.Windows.Forms.Padding(4);
            this.btnUnselect.Name = "btnUnselect";
            this.btnUnselect.Size = new System.Drawing.Size(104, 42);
            this.btnUnselect.TabIndex = 1;
            this.btnUnselect.Text = "<< Unselect";
            this.btnUnselect.UseVisualStyleBackColor = true;
            this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(8, 75);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(104, 41);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select >>";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lstAll
            // 
            this.lstAll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lstAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstAll.FullRowSelect = true;
            this.lstAll.GridLines = true;
            this.lstAll.HideSelection = false;
            this.lstAll.Location = new System.Drawing.Point(0, 0);
            this.lstAll.Margin = new System.Windows.Forms.Padding(4);
            this.lstAll.Name = "lstAll";
            this.lstAll.ShowItemToolTips = true;
            this.lstAll.Size = new System.Drawing.Size(570, 224);
            this.lstAll.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstAll.TabIndex = 6;
            this.lstAll.UseCompatibleStateImageBehavior = false;
            this.lstAll.View = System.Windows.Forms.View.Details;
            this.lstAll.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstAll_ColumnClick);
            this.lstAll.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAll_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Identified Pattern";
            this.columnHeader2.Width = 400;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Count";
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Controls.Add(this.statusStrip1);
            this.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowser.Location = new System.Drawing.Point(0, 314);
            this.pnlBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Size = new System.Drawing.Size(1265, 438);
            this.pnlBrowser.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 416);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1265, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRun.Image = global::parser.Properties.Resources.search;
            this.btnRun.Location = new System.Drawing.Point(881, 0);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(124, 90);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "Go!";
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnExportHTML
            // 
            this.btnExportHTML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportHTML.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExportHTML.Image = global::parser.Properties.Resources.html;
            this.btnExportHTML.Location = new System.Drawing.Point(12, 0);
            this.btnExportHTML.Name = "btnExportHTML";
            this.btnExportHTML.Size = new System.Drawing.Size(124, 90);
            this.btnExportHTML.TabIndex = 1;
            this.btnExportHTML.Text = "Export to HTML";
            this.btnExportHTML.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnExportHTML.UseVisualStyleBackColor = true;
            this.btnExportHTML.Click += new System.EventHandler(this.btnExportHTML_Click);
            // 
            // frmAnalyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 752);
            this.Controls.Add(this.pnlBrowser);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "frmAnalyze";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAnalize";
            this.Shown += new System.EventHandler(this.frmAnalyze_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlBrowser.ResumeLayout(false);
            this.pnlBrowser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView lstDistincts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnUnselect;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ListView lstAll;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel pnlBrowser;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportHTML;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}