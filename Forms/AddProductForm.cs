using StoreManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagement.Forms
{
    public partial class AddProductForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public AddProductForm()
        {
            InitializeComponent();
            PopulateCategoriesComboBox();
        }
        private void PopulateCategoriesComboBox()
        {
            {
                string getCategoriesQuery = "SELECT CategoryID, CategoryName FROM Categories WHERE Deleted = 0";
                DataTable dataTableCategories = dbConnection.getData(getCategoriesQuery);
                if (dataTableCategories.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "CategoryName";
                    comboBox1.ValueMember = "CategoryID";
                    comboBox1.DataSource = dataTableCategories;
                }

                string getProviderQuery = "SELECT ProviderID, ProviderName FROM Providers";
                DataTable dataTableProviders = dbConnection.getData(getProviderQuery);
                if (dataTableProviders.Rows.Count > 0)
                {
                    comboBox2.DisplayMember = "ProviderName";
                    comboBox2.ValueMember = "ProviderID";
                    comboBox2.DataSource = dataTableProviders;
                }
            }
        }

        private void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int productId = generateId.Generate("Products");

            string productName = textBox1.Text;
            string description = textBox9.Text;

            if (string.IsNullOrEmpty(productName) ||
                string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(comboBox1.ValueMember) ||
                string.IsNullOrEmpty(comboBox2.ValueMember) ||
                string.IsNullOrEmpty(textBox7.Text) ||
                string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int category = Convert.ToInt32(comboBox1.SelectedValue);
            int provider = Convert.ToInt32(comboBox2.SelectedValue);
            float price = float.Parse(textBox7.Text);
            int quantity = Convert.ToInt32(textBox6.Text);

            string query = $@"INSERT INTO Products (ProductID, ProductName, Description, CategoryID, ProviderID, UnitPrice, StockQuantity) 
                    VALUES ({productId}, '{productName}', '{description}', {category}, {provider}, {price}, {quantity})";
            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
