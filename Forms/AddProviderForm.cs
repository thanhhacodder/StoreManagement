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
    public partial class AddProviderForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public AddProviderForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int providerId = generateId.Generate("Providers");

            string providerName = textBox8.Text;
            string contactPerson = textBox5.Text;
            string email = textBox4.Text;
            string phone = textBox3.Text;
            string address = textBox1.Text;

            if (string.IsNullOrEmpty(providerName) ||
            string.IsNullOrEmpty(contactPerson) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $@"INSERT INTO Providers (ProviderID, ProviderName, ContactPerson, Email, Phone, Address) 
                    VALUES ({providerId}, '{providerName}', '{contactPerson}', '{email}', '{phone}', '{address}')";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
