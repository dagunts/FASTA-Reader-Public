using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace parser
{
    public partial class frmSearchOptions : Form
    {
        public List<string> allTypes = new List<string>();
        public List<string> selectedTypes = new List<string>();

        public frmSearchOptions()
        {
            InitializeComponent();
        }

        public void Init(List<string> types, List<string> selectedTypes)
        {
            allTypes = types;
            foreach (string type in types)
            {
                lstFeatures.Items.Add(type);
                if (selectedTypes.Contains(type))
                    lstFeatures.SetItemChecked(lstFeatures.Items.Count - 1, true);
            }
            checkBox1.Checked = (types.Count==selectedTypes.Count);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i<lstFeatures.Items.Count; i++)
                lstFeatures.SetItemChecked(i, checkBox1.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedTypes.Clear();
            for (int i = 0; i < lstFeatures.Items.Count; i++)
            {
                if (lstFeatures.GetItemChecked(i))
                    selectedTypes.Add(lstFeatures.Items[i].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
