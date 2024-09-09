using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4.Classes
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityStock { get; set; }

        public static void ListProducts(List<Product> products)
        {
            products.ForEach(p =>
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Product Id         : {p.ProductId}");
                Console.WriteLine($"Product Name       : {p.ProductName.Trim()}");
                Console.WriteLine($"Description        : {p.Description.Trim()}");
                Console.WriteLine($"Price              : {p.Price:C}");
                Console.WriteLine($"Quantity in Stock  : {p.QuantityStock}");
                Console.WriteLine("--------------------------------------------------");
            });
        }

        public static List<Product> FilterProductsById(List<Product> filterProducts,
            int filterId)
        {
            if (filterId >= 0)
            {
                return filterProducts.Where(p => p.ProductId == filterId).ToList();
            }
            else
            {
                return null;
            }
        }

        public static List<Product> FilterByText(List<Product> filterProducts, string filterString)
        {
            if (!string.IsNullOrEmpty(filterString))
            {
                return filterProducts.Where(p =>
            (p.ProductName.ToUpper()).Contains(filterString.ToUpper())).ToList();
            }
            else
            {
                return null;
            }

        }
    }
}
