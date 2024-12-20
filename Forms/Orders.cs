using StoreManagement.Helpers;
using StoreManagement.Models;
using System;
using System.Collections;
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
    public partial class Orders : Form
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        private BindingList<OrderItem> cart = new BindingList<OrderItem>();
        public Orders()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void UpdateTotalPrice()
        {
            float totalPrice = cart.Sum(item => item.Quantity * item.UnitPrice);
            textBox2.Text = totalPrice.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateId generateId = new GenerateId();
            int orderId = generateId.Generate("Orders");
            int customerId = 0;
            float totalAmount = 0;
            string shippingAddress = "Store";
            string paymentMethod = comboBox1.SelectedItem.ToString();

            if(textBox2.Text != "")
            {
                totalAmount = float.Parse(textBox2.Text);
                if (totalAmount <= 0) return;
            }
            else
            {
                return;
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && int.TryParse(textBox1.Text, out int parsedCustomerId))
            {
                customerId = parsedCustomerId;
            }

            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                shippingAddress = textBox4.Text;
            }

            string insertOrderQuery = $"INSERT INTO Orders (OrderID, CustomerID, OrderDate, TotalAmount, Status) VALUES ({orderId}, {customerId}, GETDATE(), {totalAmount}, 'Pending')";

            bool isInsertOrderSuccess = dbConnection.isExecuteSuccess(insertOrderQuery);

            int orderDetailId = generateId.Generate("OrderDetails");
            string insertOrderDetailsQuery = $"INSERT INTO OrderDetails (OrderDetailID, OrderId, ShippingAddress, PaymentMethod) VALUES ({orderDetailId}, {orderId}, '{shippingAddress}', '{paymentMethod}')";
            bool isInsertOrderDetailsSuccess = dbConnection.isExecuteSuccess(insertOrderDetailsQuery);

            if (!isInsertOrderSuccess || !isInsertOrderDetailsSuccess)
            {
                MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                foreach (var item in cart)
                {
                    int orderItemId = generateId.Generate("OrderItems");
                    string insertOrderItemQuery = $"INSERT INTO OrderItems (OrderItemID, OrderID, ProductID, Quantity, UnitPrice) VALUES ({orderItemId}, {orderId}, {item.ProductID}, {item.Quantity}, {item.UnitPrice})";
                    bool isOrderItemsInsertSuccess = dbConnection.isExecuteSuccess(insertOrderItemQuery);
/*
                    string updateProductQuantityQuery = $"UPDATE Products SET StockQuantity = StockQuantity - {item.Quantity} WHERE ProductID = {item.ProductID}";
                    bool isProductQuantityUpdateSuccess = dbConnection.isExecuteSuccess(updateProductQuantityQuery);*/

                    if (!isOrderItemsInsertSuccess)
                    {
                        MessageBox.Show("Có lỗi xảy ra!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
            MessageBox.Show("Tạo đơn hàng thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cart.Clear();
            textBox2.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["SelectColumn"].Index && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
                string productName = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                float price = float.Parse(dataGridView1.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString());
                int remainingQuantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StockQuantity"].Value);
                int quantity = 1;
                using (CustomInputDialog inputDialog = new CustomInputDialog())
                {
                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        string userInput = inputDialog.UserInput;

                        if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out int parsedQuantity))
                        {
                            if (parsedQuantity > remainingQuantity)
                            {
                                MessageBox.Show("Số lượng vượt quá hàng trong kho!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            quantity = parsedQuantity;
                        }

                    }
                    else
                    {
                        return;
                    }
                }



                OrderItem existingItem = cart.FirstOrDefault(item => item.ProductID == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    OrderItem orderItem = new OrderItem(productId, productName, quantity, price);
                    cart.Add(orderItem);
                }

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = cart;

                if (dataGridView2.Columns["DeleteColumn"] == null)
                {

                    DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                    deleteColumn.Name = "DeleteColumn";
                    deleteColumn.HeaderText = "";
                    deleteColumn.Text = "Xoá";
                    deleteColumn.UseColumnTextForButtonValue = true;
                    dataGridView2.Columns.Add(deleteColumn);
                }

                dataGridView2.Columns["DeleteColumn"].DisplayIndex = dataGridView2.Columns.Count - 1;
                UpdateTotalPrice();
            }
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Products WHERE Deleted = 0";

            DataTable dataTable = dbConnection.getData(query);

            comboBox1.Items.Add("Cash");
            comboBox1.Items.Add("Transfer");
            comboBox1.Items.Add("Card");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["Deleted"].Visible = false;


                if (dataGridView1.Columns["SelectColumn"] == null)
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.Name = "SelectColumn";
                    editColumn.HeaderText = "";
                    editColumn.Text = "Thêm";
                    editColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editColumn);
                }
            }

        }


        private void search_button_Click(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM (SELECT CONCAT(ProductID, ' ', ProductName, ' ', Description) AS Infor, * FROM Products) AS Subquery WHERE Infor LIKE '%{textBox3.Text}%'";

            DataTable dataTable = dbConnection.getData(query);
            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["Infor"].Visible = false;
            }
            else
            {
                MessageBox.Show("Không có kết quả phù hợp!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            string query = $"SELECT * FROM Products";
            DataTable dataTable = dbConnection.getData(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["SelectColumn"].DisplayIndex = dataGridView1.Columns.Count - 1;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                cart.RemoveAt(e.RowIndex);
                UpdateTotalPrice(); 
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || !int.TryParse(textBox1.Text, out int parsedCustomerId))
            {
                checkBox1.Checked = false;
                MessageBox.Show("Không tìm thấy địa chỉ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $"SELECT Address FROM Customers WHERE CustomerID = {Convert.ToInt32(textBox1.Text)}";
            DataTable dataTable = dbConnection.getData(query);
            if (textBox4.Text == string.Empty)
            {
                textBox4.Text = dataTable.Rows[0]["Address"].ToString();
            }
            else
            {
                textBox4.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
