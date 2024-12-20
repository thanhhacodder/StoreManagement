using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
        public class ProductWarehouseReceipt
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }

            public ProductWarehouseReceipt(int productId, string productName, int quantity)
            {
                this.ProductID = productId;
                this.ProductName = productName;
                this.Quantity = quantity;
            }
        }
}
