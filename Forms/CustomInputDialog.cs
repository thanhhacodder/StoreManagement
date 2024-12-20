using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement.Forms
{
    public partial class CustomInputDialog : Form
    {
        public string UserInput { get; private set; }

        public CustomInputDialog()
        {
            InitializeComponent();
            textBoxInput.Text = "1";
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            UserInput = textBoxInput.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CustomInputDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
