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
        public int Quantity_Sold { get; set; }
        public DateTime Sale_Date { get; set; }
        public decimal Sale_Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }

        public static void ListSales(List<Sale> sales)
        {
            sales.ForEach(s =>
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Sale Id         : {s.SaleId}");
                Console.WriteLine($"Quantity Sold   : {s.Quantity_Sold}");
                Console.WriteLine($"Sale_Date       : {s.Sale_Date}");
                Console.WriteLine($"Sale_Price      : {s.Sale_Price:C}");
                Console.WriteLine($"Total Amount    : {s.TotalAmount}");
                Console.WriteLine($"Customer Name   : {s.CustomerName}");
                Console.WriteLine("--------------------------------------------------");
            });
        }

        public static void OrderSale(List<Sale> sales)
        {
            var orderSales = sales.GroupBy(s => s.Sale_Date)
                .Select(s => new
                {
                    SaleDate = s.Key,
                    TotalSales = s.Sum(s => s.TotalAmount),
                    SalesCount = s.Count()
                })
                .OrderByDescending(s => s.TotalSales).ToList();

            foreach (var order in orderSales)
            {
                Console.WriteLine($"Sale Date: {order.SaleDate} | Total Sale: {order.TotalSales} |" +
                    $" Sales Count: {order.SalesCount}");
            }
        }
    }
}
