using StoreManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManagement.Forms
{
    
    public partial class EditProductForm : Form
    {
        private int productId;
        DatabaseConnection dbConnection = new DatabaseConnection();
        public EditProductForm(int productId)
        {
            InitializeComponent();
            this.productId = productId;
            PopulateCategoriesComboBox();
        }

        private void PopulateCategoriesComboBox()
        {
            {
                string getCategoriesQuery = "SELECT CategoryID, CategoryName FROM Categories";
                DataTable dataTableCategories = dbConnection.getData(getCategoriesQuery);
                if (dataTableCategories.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "CategoryName";
                    comboBox1.ValueMember = "CategoryID";
                    comboBox1.DataSource = dataTableCategories;
                    comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                string getProviderQuery = "SELECT ProviderID, ProviderName FROM Providers";
                DataTable dataTableProviders = dbConnection.getData(getProviderQuery);
                if (dataTableProviders.Rows.Count > 0)
                {
                    comboBox2.DisplayMember = "ProviderName";
                    comboBox2.ValueMember = "ProviderID";
                    comboBox2.DataSource = dataTableProviders;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            
        }

        private void EditProductForm_Load(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM Products WHERE ProductID = {productId}";
            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                textBox1.Text = dataTable.Rows[0]["ProductName"].ToString();
                textBox9.Text = dataTable.Rows[0]["Description"].ToString();
                comboBox1.SelectedValue = dataTable.Rows[0]["CategoryID"];
                comboBox2.SelectedValue = dataTable.Rows[0]["ProviderID"];
                textBox7.Text = dataTable.Rows[0]["UnitPrice"].ToString();
                textBox6.Text = dataTable.Rows[0]["StockQuantity"].ToString();
            }
            else
            {
                MessageBox.Show("An error occurred!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, EventArgs e)
        {
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

            string query = $@"UPDATE Products 
                      SET ProductName = '{productName}', 
                          Description = '{description}', 
                          CategoryID = {category}, 
                          ProviderID = {provider}, 
                          UnitPrice = {price}, 
                          StockQuantity = {quantity} 
                      WHERE ProductID = {productId}";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
