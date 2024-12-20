using StoreManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement.Forms
{
    public partial class EditProviderForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        private int providerId;
        public EditProviderForm(int providerId)
        {
            InitializeComponent();
            this.providerId = providerId;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string providerName = textBox1.Text;
            string contactPerson = textBox9.Text;
            string email = textBox7.Text;
            string phone = textBox6.Text;
            string address = textBox2.Text;

            if (string.IsNullOrEmpty(providerName) ||
            string.IsNullOrEmpty(contactPerson) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $@"UPDATE Providers 
                              SET ProviderName = '{providerName}', ContactPerson = '{contactPerson}', Email = '{email}', Phone = '{phone}', Address = '{address}' 
                              WHERE ProviderID = {providerId}";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Chỉnh sửa nhà cung cấp thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditProviderForm_Load(object sender, EventArgs e)
        {
            string query = $"SElECT * FROM Providers WHERE ProviderID = {providerId}";
            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                textBox1.Text = dataTable.Rows[0]["ProviderName"].ToString();
                textBox9.Text = dataTable.Rows[0]["ContactPerson"].ToString();
                textBox7.Text = dataTable.Rows[0]["Email"].ToString();
                textBox6.Text = dataTable.Rows[0]["Phone"].ToString();
                textBox2.Text = dataTable.Rows[0]["Address"].ToString();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
