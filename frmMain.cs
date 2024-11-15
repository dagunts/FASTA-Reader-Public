using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace parser
{
    public partial class frmMain : Form
    {
        public int max_size = 50000000;
        public int max_size_x = 60000000;
        public List<string> allTypes = new List<string>();
        public List<string> selectedTypes = new List<string>();

        public frmMain()
        {
            InitializeComponent();
        }

        string currentFile = string.Empty;
        StringBuilder sbFile = new StringBuilder();
        List<Protein> proteins = new List<Protein>();

        private void LoadFile(string file)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(file))
            {
                long size = sr.BaseStream.Length;
                if (size > max_size_x)
                {
                    decimal n = size/max_size;
                    if (MessageBox.Show("The file is too large. Do you want to split the file to ann acceptable sizes? This creates about "+ Math.Ceiling(n).ToString()+" file(s).", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        return;                                       

                    SplitFile(file);
                    return;
                }
                currentFile = sr.ReadToEnd();
                statusBar.Text = file;
                ParseFile();
            }
        }


        private void SplitFile(string file) 
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(file))
            {

                long size = sr.BaseStream.Length;
                if (size < max_size_x)
                {
                    MessageBox.Show("The file is not too large. You don't need to split it.", "Warning", MessageBoxButtons.OK);
                    return;
                }

                string newFolder = System.IO.Path.Combine(Application.StartupPath + "\\Data", System.IO.Path.GetFileName(file + "_dir"));
                if (System.IO.Directory.Exists(newFolder))
                {
                    MessageBox.Show("The folder already exists. Please delete or rename it first.");
                    return;
                }
                System.IO.Directory.CreateDirectory(newFolder);

                char[] buffer = new char[max_size_x];
                int read = 0;
                int x = 1;
                decimal n = size / max_size;
                progressBar.Maximum = (int)Math.Ceiling(n)+5;
                progressBar.Value = 0;
                while ((read = sr.ReadBlock(buffer, 0, max_size)) > 0)
                {
                    int a;
                    read++;
                    while ((a = sr.Read()) >- 1)
                    {
                        buffer[read++] = (char)a;
                        if ((char)a == '>')
                        {
                            break;
                        }
                    }
                    string newFile = System.IO.Path.Combine(newFolder, System.IO.Path.GetFileNameWithoutExtension(file) + "_part" + x.ToString() + System.IO.Path.GetExtension(file));
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(newFile);
                    if (buffer[0] != '>')
                        sw.Write(">");

                    sw.Write(buffer, 0, read);
                    sw.Close();
                    x++;
                    if (progressBar.Value < progressBar.Maximum)
                        progressBar.Value++;
                }
                sr.Close();

                MessageBox.Show("The file has been split into " + x.ToString() + " files. The new folder is " + newFolder);
                progressBar.Value = 0;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabMain.TabPages.Count > 1)
            {
                if (MessageBox.Show("Are you sure you want to reset all the tabs?", "CONFIRM!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    while (tabMain.TabPages.Count > 1)
                        tabMain.TabPages.RemoveAt(tabMain.TabPages.Count - 1);
                    currentFile = string.Empty;
                    proteins = new List<Protein>();
                    pnlSearch.Visible = false;
                }
                else
                    return;
            }

            Application.DoEvents();
            lwTypes.SuspendLayout();
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, "Data");
            openFileDialog1.Filter = "(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Application.UseWaitCursor = true;
                LoadFile(openFileDialog1.FileName);
                Application.UseWaitCursor = false;
            }

            LoadOptions();
            lwTypes.ResumeLayout();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ParseFile()
        {
            pnlSearch.Visible = true;
            List<FeatureCollection> allFeatures = new List<FeatureCollection>();
            if (System.IO.Directory.Exists(statusBar.Text + "_features"))
            {
                string fName = statusBar.Text + "_features\\features.json";
                string json = System.IO.File.ReadAllText(fName);
                FeatureCollection fc = new FeatureCollection();
                allFeatures = FeatureCollection.LoadFeatures(json, ref allTypes, ref progressBar);
                selectedTypes.AddRange(allTypes);
                lwTypes.Items.Clear();  
                foreach (string s in selectedTypes)
                    lwTypes.Items.Add(s);
            }
            FileParser parser = new FileParser();
            proteins = parser.Parse(currentFile, allFeatures);            

            try
            {
                this.Cursor = Cursors.WaitCursor;
                progressBar.Value = 0;
                progressBar.Maximum = proteins.Count;
                Application.DoEvents(); 

                webMain.DocumentText = "<html><body></body></html>";
                webMain.Document.OpenNew(true);
                webMain.Document.Write("<html><body>" + parser.GetHTML(proteins, ref progressBar) + "</body></html>");
                webMain.Navigating += new WebBrowserNavigatingEventHandler(NavigateInDefaultBrowser);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
                progressBar.Value = 0;
            }
            this.Cursor = Cursors.Default;
            progressBar.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (proteins.Count == 0)
            {
                MessageBox.Show("Please parse the file first.");
                return;
            }

            if (string.IsNullOrEmpty(txtPattern.Text))
            {
                MessageBox.Show("Please provide a pattern to search.");
                return;
            }

            FileParser parser = new FileParser();

            try
            {
                List<Protein> founds = parser.Search(proteins, txtPattern.Text, nmbLast.Value, selectedTypes);

                if (founds.Count() > 0)
                {
                    tabMain.TabPages.Add(txtPattern.Text.Trim());
                    tabMain.SelectedTab = tabMain.TabPages[tabMain.TabPages.Count - 1];
                    Panel panel = new Panel();
                    panel.Height = pnlSearch.Height;
                    panel.Name = "pnlResult" + tabMain.TabPages.Count.ToString();
                    panel.Controls.Add(new Label() { Text = "Total Entries: " + founds.Count().ToString(), Dock = DockStyle.Top, AutoSize = false });
                    panel.Controls.Add(new Label() { Text = "Pattern: " + txtPattern.Text, Dock = DockStyle.Top, AutoSize = false });
                    panel.Controls.Add(new Label() { Text = "Last " + nmbLast.Value.ToString() + " characters", Dock = DockStyle.Top, AutoSize = false });

                    panel.Controls.Add(new Button { Text = "Analyze Pattern", Dock = DockStyle.Right, AutoSize = true, Name = "btnAnalize" + tabMain.TabPages.Count.ToString() });
                    Button btn3 = (Button)panel.Controls.Find("btnAnalize" + tabMain.TabPages.Count.ToString(), false).FirstOrDefault();

                    btn3.Tag = new SearchParams(txtPattern.Text, (int)nmbLast.Value, founds);
                    btn3.Click += new EventHandler(btnAnalizeClick);
                    btn3.Image = Properties.Resources.analyze;
                    btn3.TextImageRelation = TextImageRelation.TextBeforeImage;
                    btn3.Width = 124;
                    btn3.Cursor = Cursors.Hand;

                    panel.Controls.Add(new Button { Text = "Export to CSV", Dock = DockStyle.Right, AutoSize = true, Name = "btnExport" + tabMain.TabPages.Count.ToString() });
                    Button btn = (Button)panel.Controls.Find("btnExport" + tabMain.TabPages.Count.ToString(), false).FirstOrDefault();
                    btn.Tag = new SearchParams(txtPattern.Text, (int)nmbLast.Value, founds); 
                    btn.Click += new EventHandler(btnExportClick);
                    btn.Image = Properties.Resources.csv;
                    btn.TextImageRelation = TextImageRelation.TextBeforeImage;
                    btn.Width = 124;
                    btn.Cursor = Cursors.Hand;


                    panel.Controls.Add(new Button { Text = "Export to HTML", Dock = DockStyle.Right, AutoSize = true, Name = "btnExportHTML" + tabMain.TabPages.Count.ToString() });
                    Button btn2 = (Button)panel.Controls.Find("btnExportHTML" + tabMain.TabPages.Count.ToString(), false).FirstOrDefault();
                    btn2.Image = Properties.Resources.html;
                    btn2.TextImageRelation = TextImageRelation.TextBeforeImage;
                    btn2.Width = 124;
                    btn2.Cursor = Cursors.Hand;

                    Application.DoEvents();
                    WebBrowser browser = new WebBrowser();
                    browser.Parent = tabMain.SelectedTab;
                    browser.Dock = DockStyle.Fill;
                    browser.DocumentText = "<html><body></body></html>";
                    browser.Document.OpenNew(true);
                    browser.Document.Write("<html><body>" + parser.GetHTML(founds, (int)nmbLast.Value, txtPattern.Text, selectedTypes) + "</body></html>");

                    browser.Navigating += new WebBrowserNavigatingEventHandler(NavigateInDefaultBrowser);

                    btn2.Tag = new SearchParams(txtPattern.Text, (int)nmbLast.Value, founds, browser); 
                    btn2.Click += new EventHandler(btnExportToHtmlClick);
                    panel.Parent = tabMain.SelectedTab;
                    panel.Dock = DockStyle.Top;

                }
                else
                {
                    MessageBox.Show("No results found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops. Something wrong happenned :(   " + ex.Message);
                return;
            }            

        }

        private void btnExportToHtmlClick(object sender, EventArgs e)
        {
            WebBrowser browser = ((SearchParams)((Button)sender).Tag).Browser;
            
            saveFileDialog1.Filter = "HTML Files (*.html)|*.html";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "Results_" + FileParser.GenerateFileName(((SearchParams)((Button)sender).Tag).Pattern) + ".html";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileParser parser = new FileParser();
                parser.ExportToHTML(browser, saveFileDialog1.FileName);
            }
        }

        private void btnExportClick(object sender, EventArgs e)
        {
            SearchParams p = (SearchParams)((Button)sender).Tag;
            List<Protein> founds = p.ProteinList;
            if (founds.Count() == 0)
            {
                MessageBox.Show("No results found.");
                return;
            }            

            saveFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "Results_" +FileParser.GenerateFileName(p.Pattern) + ".csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileParser parser = new FileParser();
                parser.ExportToCSV(founds, saveFileDialog1.FileName, selectedTypes, p.Pattern, p.SearchedLastChars);
            }
        }

        public void NavigateInDefaultBrowser(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
            if (e.Url.ToString().Contains("open_features"))
            {
                string[] parts = e.Url.ToString().Split('/');
                string proteinid = parts[1];
                string featureid = "";
                if (parts.Length > 2)
                     featureid = parts[2];

                foreach (Protein p in proteins)
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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset all the tabs?", "CONFIRM!", MessageBoxButtons.OKCancel)== DialogResult.OK)
            {
                while (tabMain.TabPages.Count > 1)  
                    tabMain.TabPages.RemoveAt(tabMain.TabPages.Count-1);
                currentFile = string.Empty;
                proteins = new List<Protein>();
                pnlSearch.Visible = false;
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, "Data");
            openFileDialog1.Filter = "(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SplitFile(openFileDialog1.FileName);
            }
        }

        private void btnAnalizeClick(object sender, EventArgs e)
        {            
            frmAnalyze frm = new frmAnalyze();            
            frm.Params = (SearchParams)((Button)sender).Tag;
            frm.Text = "Analize Pattern " + frm.Params.Pattern;
            frm.allTypes.AddRange(allTypes);
            frm.selectedTypes.AddRange(selectedTypes);
            frm.InitData(ref progressBar);
            frm.ShowDialog();
        }

        private void featuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (proteins.Count==0)
            {
                MessageBox.Show("Please open a file first.");
                return;
            }
            string fName = statusBar.Text + "_features\\features.json";
            if (System.IO.File.Exists(fName))
            {
                MessageBox.Show("File "+fName+" exists. Please delete the folder and the file first.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to retrieve the features for all the proteins?\n\nThis can take signinficant time. Make sure the computer is connected to the Internet.\nPlease do not touch the application until the process is finished.", "CONFIRM!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fName));
            FileStream fs = File.Create(fName);

            string oldFile = statusBar.Text;

            try
            {
                string s = "{\n\"entries\": [\n";
                byte[] info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, 0, info.Length);

                int i = 0;
                progressBar.Maximum = proteins.Count;
                progressBar.Value = 0;
                foreach (Protein p in proteins)
                {
                    progressBar.Value++;
                    statusBar.Text = "Retreiving " + p.EntryID;
                    Application.DoEvents();
                    s = "";
                    if (i>0)
                        s = ",\n";
                    s += FeatureAPI.GetFeatures(p.EntryID).TrimEnd();
                    
                    info = new UTF8Encoding(true).GetBytes(s);
                    fs.Write(info, 0, info.Length);
                    i++;
                }
                s = s.TrimEnd().TrimEnd(',');
                s = "\n] }";
                info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, 0, info.Length);
                fs.Close();

                progressBar.Value = 0;  
                statusBar.Text = oldFile;
                MessageBox.Show("The features have been saved to " + fName + ".\n\nPlease re-load the "+fName+" file.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops. Something wrong happenned :(   " + ex.Message);
                fs.Close();
                statusBar.Text = oldFile;
                return;
            }
        }

        private void selectFeatureTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSearchOptions frm = new frmSearchOptions();
            frm.Init(allTypes, selectedTypes);
            if (frm.ShowDialog() == DialogResult.Cancel)
                return;

            if (frm.selectedTypes.Count == 0)
            {
                MessageBox.Show("Please select at least one feature type.");
                return;
            }

            selectedTypes = frm.selectedTypes;
            lwTypes.Items.Clear();
            foreach (string s in selectedTypes)
                lwTypes.Items.Add(s);
            frm.Close();

            SaveOptions();

        }

        private void SaveOptions()
        {
            string fName = System.IO.Path.Combine(Application.StartupPath, "options.xml");
            XDocument doc = new XDocument();
            XElement root = new XElement("options");
            doc.Add(root);

            XElement types = new XElement("types");
            foreach (string s in selectedTypes)
            {
                XElement type = new XElement("type");
                type.Value = s;
                types.Add(type);
            }
            root.Add(types);

            doc.Save(fName);
        }

        private void LoadOptions()
        {
            string fName = System.IO.Path.Combine(Application.StartupPath, "options.xml");
            if (!System.IO.File.Exists(fName))
                return;

            XDocument doc = XDocument.Load(fName);
            XElement root = doc.Element("options");
            if (root == null)
                return;

            XElement types = root.Element("types");
            if (types == null)
                return;

            selectedTypes.Clear();
            lwTypes.Items.Clear();

            foreach (XElement type in types.Elements())
            {
                selectedTypes.Add(type.Value);
                lwTypes.Items.Add(type.Value);
            }
        }

        private void txtPattern_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
                button1_Click(sender, e);
        }
    }
}
