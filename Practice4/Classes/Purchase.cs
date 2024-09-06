using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4.Classes
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int Quantity_Purchased { get; set; }
        public decimal Purchase_Price { get; set; }
        public DateTime Purchase_Date { get; set; }
        public decimal Total_Cost { get; set; }
        public int ProductId { get; set; }
    }
}
