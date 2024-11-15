using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace parser
{
    public partial class frmFeatures : Form
    {
        public Protein Protein { get; set; }    
        public frmFeatures()
        {
            InitializeComponent();
        }

        public void InitTree(Protein p, List<string> types, string featureid = "")
        {
            Protein = p;
            this.Text = "Features of " + Protein.EntryID;

            lblCaption.Text = Protein.EntryID + "\n";
            lblCaption.Text += Protein.Entry + "\n";

            lstTypes.Items.Clear();
            lstTypes.Items.Add("All..." + GetCount(""));
            foreach (string type in types)
                lstTypes.Items.Add(type + GetCount(type));
            

            PresentWebSequence();

            if (Protein.Features == null)
                return;

            lstTypes.SelectedIndex = 0;

            if (featureid != "")
            {
                foreach (TreeNode n in tvFeatures.Nodes)
                {
                    if (n.Tag != null)
                    {
                        Feature f = (Feature)n.Tag;
                        if (f.InternalID == featureid)
                        {
                            tvFeatures.SelectedNode = n;
                            tvFeatures.SelectedNode.EnsureVisible();
                            timer1.Start();
                            break;
                        }
                    }
                }
            }


        }

        public string GetCount(string type)
        {
            if (Protein.Features == null)
                return "";

            if (type=="" && Protein.Features.Count>0)
                return " - [" + Protein.Features.Count + "]";

            int count = 0;
            if (Protein.Features == null)
                return "";

            foreach (Feature f in Protein.Features)
            {
                if (f.Type == type)
                    count++;
            }
            if (count == 0)
                return "";
            else
                return " - [" + count + "]";            
        }

        public void PopulateTree()
        {
            tvFeatures.SuspendLayout();
            tvFeatures.Visible = false;
            tvFeatures.Nodes.Clear();

            string filter = "All...";
            if (lstTypes.SelectedItems.Count!=0)
                filter = lstTypes.SelectedItems[0].ToString();

            if (Protein.Features == null)
                return;

            foreach (Feature f in Protein.Features)
            {
                if (filter.Contains("["))
                {
                    if (filter.Substring(0, filter.IndexOf(" - [")) != "All..." && f.Type != filter.Substring(0, filter.IndexOf(" - [")))
                        continue;
                }
                else
                    continue;

                TreeNode node = new TreeNode(f.Type);
                node.Tag = f;
                node.Nodes.Add(new TreeNode(f.Description));
                node.Nodes.Add(new TreeNode(f.Start.Value + " (" + f.Start.Modifier + ") - " + f.End.Value + " (" + f.End.Modifier + ")"));
                TreeNode evidences = new TreeNode("Evidences");
                if (f.Evidences != null)
                {
                    foreach (Evidence e in f.Evidences)
                    {
                        TreeNode ev = new TreeNode("ID:" + e.ID + " - Code:" + e.EvidenceCode + " - Source:" + e.Source);
                        evidences.Nodes.Add(ev);
                    }
                    node.Nodes.Add(evidences);
                }
                tvFeatures.Nodes.Add(node);
            }

            tvFeatures.ExpandAll();
            PresentWebSequence();

            if (tvFeatures.Nodes.Count > 0)
                tvFeatures.SelectedNode = tvFeatures.Nodes[0];
            
            tvFeatures.Visible = true;
            tvFeatures.ResumeLayout();
        }

        public void PresentWebSequence(Feature f = null)
        {
            string sequence = "";
            if (Protein.Sequence != null)
            {
                sequence = Protein.Sequence;
                sequence = sequence.Replace("\n", "<br>");
                sequence = sequence.Replace("\r", "<br>");
                sequence = sequence.Replace(" ", "&nbsp;");
            }


            wb.Text = "";

            if (f != null)
            {
                int start = (int)f.Start.Value;
                int end = (int)f.End.Value;
                wb.SelectionBackColor = Color.Transparent;
                wb.AppendText(sequence.Substring(0, start - 1));
                wb.SelectionBackColor = Color.FromArgb(174, 229, 0);
                wb.AppendText(sequence.Substring(start-1, end - start + 1));
                wb.SelectionBackColor = Color.Transparent;
                wb.AppendText(sequence.Substring(end));
            }
            else
            {
                wb.SelectionBackColor = Color.Transparent;
                wb.AppendText(sequence);
            }          
            
            Application.DoEvents();

        }

        private void tvFeatures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode root = e.Node;
            while (root.Parent != null)
                root = root.Parent;

            if (root.Tag != null)
            {
                Feature f = (Feature)root.Tag;
                PresentWebSequence(f);
            }            
        }

        private void btnOpenUrl_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.uniprot.org/uniprotkb/" + Protein.EntryID + "/entry'");
        }

        private void lstTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateTree();
        }

        private void CalculatePosition()
        {
            int n = wb.GetCharIndexFromPosition(wb.PointToClient(Cursor.Position));
            lblPos.Text = (n+1).ToString();            
        }

        private void wb_Click(object sender, EventArgs e)
        {
            CalculatePosition();
        }

        private void wb_KeyPress(object sender, KeyPressEventArgs e)
        {
            CalculatePosition();
        }

        private void wb_KeyUp(object sender, KeyEventArgs e)
        {
            CalculatePosition();
        }

        private void frmFeatures_Shown(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false; 
            tvFeatures.Focus();
        }
    }
}
