using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    public class OrderItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public OrderItem(int productId, string productName, int quantity, float unitPrice)
        {
            this.ProductID = productId;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
    }
}
