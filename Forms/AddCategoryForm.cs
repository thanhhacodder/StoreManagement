using StoreManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StoreManagement.Forms
{
    public partial class AddCategoryForm : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public AddCategoryForm()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int categoryId = generateId.Generate("Categories");

            string categoryName = textBox5.Text;
            string categoryDescription = textBox4.Text;

            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(categoryDescription))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $@"INSERT INTO Categories (CategoryID, CategoryName, Description) 
                    VALUES ({categoryId}, '{categoryName}', '{categoryDescription}')";

            bool insertSuccess = dbConnection.isExecuteSuccess(query);
            if (insertSuccess)
            {
                MessageBox.Show("Thêm danh mục thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
