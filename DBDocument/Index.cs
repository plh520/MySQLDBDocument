using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBDocument
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void btn_MySql_Click(object sender, EventArgs e)
        {
            SaveDocument saveDocument = new SaveDocument();
            saveDocument.ShowDialog();
        }

        private void btn_SQLServer_Click(object sender, EventArgs e)
        {

        }
    }
}
