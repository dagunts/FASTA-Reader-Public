using System;
using System.Collections;
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
    public partial class frmAnalyze : Form
    {

        public List<PatternItem> PatternList { get; set; }
        List<Protein> result = new List<Protein>();
        public List<string> allTypes = new List<string>();
        public List<string> selectedTypes = new List<string>();

        public SearchParams Params { get; set; } 

        private int lstAllSortCol= 0;
        private int lstDistinctsSortCol = 0;
        private string overallPattern = "";

        public frmAnalyze()
        {
            InitializeComponent();
        }

        public void InitData(ref ToolStripProgressBar progressBar)
        { 
            lblCaption.Text = "Patterns for " + Params.Pattern;
            lblCaption.Text += ". Found total: " + Params.ProteinList.Count.ToString();
            lblCaption.Text += ". Searched within "+ Params.SearchedLastChars + " last characters.";


            List<Protein> list = Params.ProteinList;
            List<PatternItem> patterns = new List<PatternItem>();

            progressBar.Maximum = list.Count;
            progressBar.Value = 0;
            foreach (Protein protein in list)
            {
                foreach (FoundMatch match in protein.FoundSequences)
                {
                    if (!PatternExists(patterns, match.Value))
                    {
                        patterns.Add(new PatternItem(match.Value, 1));                        
                    } else
                    {
                        foreach (PatternItem item in patterns)
                        {
                            if (item.Pattern == match.Value)
                            {
                                item.Count++;
                                break;
                            }
                        }
                    }
                }
                progressBar.Value++;
            }
            progressBar.Value = 0;
            progressBar.Maximum = patterns.Count;

            PatternList = patterns;
            lstAll.BeginUpdate();
            foreach (PatternItem pattern in PatternList)
            {
                ListViewItem it = lstAll.Items.Add(pattern.Pattern);
                it.ImageIndex = 0;                
                it.SubItems.Add(pattern.Count.ToString());
                progressBar.Value++;
            }
            progressBar.Value = 0;
            lstAll.EndUpdate();
        }

        public void NavigateInDefaultBrowser2(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
            if (e.Url.ToString().Contains("open_features"))
            {
                string[] parts = e.Url.ToString().Split('/');
                string proteinid = parts[1];
                string featureid = "";
                if (parts.Length > 2)
                    featureid = parts[2];

                foreach (Protein p in result)
                {
                    if (p.EntryID == proteinid)
                    {
                        frmFeatures frm = new frmFeatures();
                        frm.InitTree(p, allTypes, featureid);
                        frm.ShowDialog();
                        return;
                    }
                }
            }
            else
            {
                System.Diagnostics.Process.Start(e.Url.ToString());
            }
        }

        private void RunAnalysis(string op)
        {
            result.Clear();
            FileParser parser = new FileParser();
            overallPattern = "";
            for (int i = 0; i < lstDistincts.Items.Count; i++)
                overallPattern += lstDistincts.Items[i].Text + " ";

            if (op=="OR")
            {
                overallPattern = overallPattern.Trim().Replace(" ", " or ");
                for (int i = 0; i < lstDistincts.Items.Count; i++)
                {
                    string s = lstDistincts.Items[i].Text;
                    List<Protein> founds = parser.QuickSearch(Params.ProteinList, s, Params.SearchedLastChars);
                    parser.Overlapp(ref result, founds);
                }
            } else
            {
                overallPattern = overallPattern.Trim().Replace(" ", " and ");
                List<Protein> founds = new List<Protein>();                

                founds.AddRange(Params.ProteinList);

                for (int j = 0; j < lstDistincts.Items.Count; j++)
                {
                    string s = lstDistincts.Items[j].Text;                        
                    founds = parser.QuickSearch(founds, s, Params.SearchedLastChars);
                    if (founds.Count == 0)
                        break;
                }
                if (founds.Count>0)
                    result.AddRange(founds);
            }


            if (result.Count > 0)
            {
                while (pnlBrowser.Controls.Count > 0)
                {
                    pnlBrowser.Controls[0].Dispose();
                }
                pnlBrowser.Controls.Clear();                

                Application.DoEvents();
                WebBrowser browser = new WebBrowser();
                browser.Parent = pnlBrowser;
                browser.Dock = DockStyle.Fill;
                browser.DocumentText = "<html><body></body></html>";
                browser.Document.OpenNew(true);
                browser.Document.Write("<html><body>" + parser.GetHTML(result, Params.SearchedLastChars, overallPattern, selectedTypes) + "</body></html>");

                browser.Navigating += new WebBrowserNavigatingEventHandler(NavigateInDefaultBrowser2);
                btnExportHTML.Tag = browser;
            }
            else
            {
                MessageBox.Show("No result found.");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            lstAll.BeginUpdate();
            lstDistincts.BeginUpdate();
            lstAll.Sorting = SortOrder.None;
            lstDistincts.Sorting = SortOrder.None;

            for (int i = 0; i < lstAll.SelectedItems.Count; i++)
            {
                (lstDistincts.Items.Add(lstAll.SelectedItems[i].Text)).SubItems.Add(lstAll.SelectedItems[i].SubItems[1]);
            }

            for (int i = lstAll.SelectedItems.Count-1; i >= 0; i--)
            {
                lstAll.Items.Remove(lstAll.SelectedItems[i]);
            }
            lstAll.Sorting = SortOrder.Ascending;
            lstAll.ListViewItemSorter = new ListViewItemComparer(lstAllSortCol);
            lstAll.Sort();
            lstDistincts.Sorting = SortOrder.Ascending;
            lstDistincts.ListViewItemSorter = new ListViewItemComparer(lstDistinctsSortCol);
            lstDistincts.Sort();
            lstAll.EndUpdate();
            lstDistincts.EndUpdate();
        }

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            lstAll.BeginUpdate();
            lstDistincts.BeginUpdate();
            lstAll.Sorting = SortOrder.None;
            lstDistincts.Sorting = SortOrder.None;

            for (int i = 0; i < lstDistincts.SelectedItems.Count; i++)
            {
                (lstAll.Items.Add(lstDistincts.SelectedItems[i].Text)).SubItems.Add(lstDistincts.SelectedItems[i].SubItems[1]);
            }

            for (int i = lstDistincts.SelectedItems.Count - 1; i >= 0; i--)
            {
                lstDistincts.Items.Remove(lstDistincts.SelectedItems[i]);
            }

            lstAll.Sorting = SortOrder.Ascending;
            lstAll.ListViewItemSorter = new ListViewItemComparer(lstAllSortCol);
            lstAll.Sort();
            lstDistincts.Sorting = SortOrder.Ascending;
            lstDistincts.ListViewItemSorter = new ListViewItemComparer(lstDistinctsSortCol);
            lstDistincts.Sort();
            lstAll.EndUpdate();
            lstDistincts.EndUpdate();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (cmbOperator.Text=="")
            {
                MessageBox.Show("Please select 'Combine Results Using' operator");
                return;
            }
            RunAnalysis(cmbOperator.Text);
        }

        private bool PatternExists(List<PatternItem> list, string pattern)
        {
            foreach (PatternItem item in list)
            {
                if (item.Pattern == pattern)
                    return true;
            }
            return false;
        }

        private void lstAll_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lstAllSortCol = e.Column;
            lstAll.ListViewItemSorter = new ListViewItemComparer(e.Column);
            lstAll.Sort();
        }

        private void lstDistincts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lstDistinctsSortCol = e.Column;
            lstDistincts.ListViewItemSorter = new ListViewItemComparer(e.Column);
            lstDistincts.Sort();
        }

        private void lstAll_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAll.SelectedItems.Count > 0)
                btnSelect_Click(sender, e);
        }

        private void lstDistincts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstDistincts.SelectedItems.Count > 0)
                btnUnselect_Click(sender, e);
        }

        private void cmbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOperator.Text=="OR")
            {
                lblExplanation.Text = "Each sequence in result should contain at least one of the selected patterns.";
            }
            else if (cmbOperator.Text == "AND")
            {
                lblExplanation.Text = "Each sequence in result should contain all the selected patterns.";
            }
            else if (cmbOperator.Text == "")
            {
                lblExplanation.Text = "Please select 'Combine Results Using' operator.";
            }
        }

        private void btnExportHTML_Click(object sender, EventArgs e)
        {
            WebBrowser browser = (WebBrowser)((System.Windows.Forms.Button)sender).Tag;
            //Export browser to HTML
            saveFileDialog1.Filter = "HTML Files (*.html)|*.html";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "Results_" + FileParser.GenerateFileName(Params.Pattern, overallPattern) + ".html";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileParser parser = new FileParser();
                parser.ExportToHTML(browser, saveFileDialog1.FileName);
            }

        }

        private void frmAnalyze_Shown(object sender, EventArgs e)
        {

        }
    }

    public class ListViewItemComparer : IComparer
    {
        private int col;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int PrepareInt(object o)
        {
            if (o == null)
                return 0;
            else
            {
                try
                {
                    return int.Parse(o.ToString());
                }                
                catch {
                    return 0;
                }

            }
        }

        public int Compare(object x, object y)
        {
            if (col == 0)
                return String.Compare(((ListViewItem)x).Text, ((ListViewItem)y).Text);
            else
            {                
                if (PrepareInt(((ListViewItem)x).SubItems[col].Text) > PrepareInt(((ListViewItem)y).SubItems[col].Text))
                    return -1;
                else if (PrepareInt(((ListViewItem)x).SubItems[col].Text) < PrepareInt(((ListViewItem)y).SubItems[col].Text))
                    return 1;
                else
                    return 0;                
            }
                
        }
    }

    public class SearchParams
    {
        public string Pattern { get; set; } = "";
        public int SearchedLastChars { get; set; }
        public List<Protein> ProteinList { get; set; }
        public WebBrowser Browser { get; set; }

        public SearchParams(string pattern, int searchedLastChars, List<Protein> proteinList, WebBrowser b = null)
        {
            Pattern = pattern;
            SearchedLastChars = searchedLastChars;
            ProteinList = proteinList;
            Browser = b;
        }
    }

    public class PatternItem
    {
        public string Pattern { get; set; }
        public int Count { get; set; }

        public PatternItem(string pattern, int count)
        {
            Pattern = pattern;
            Count = count;
        }
    }
}
