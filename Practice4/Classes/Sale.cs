using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4.Classes
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string Quantity_Sold { get; set; }
        public DateTime Sale_Date { get; set; }
        public decimal Sale_Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
    }
}
