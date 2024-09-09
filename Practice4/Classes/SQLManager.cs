using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4.Classes
{
    public class SQLManager
    {
        public static string ConnectionString = @"Server=localhost;Database=Practice4_JM;Integrated Security=True;";
        private SqlConnection Connection;

        public SQLManager()
        {
            this.Connection = new SqlConnection(ConnectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM Products";

            SqlCommand command = new SqlCommand(query, this.Connection);
            this.Connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        QuantityStock = reader.GetInt32(4),
                    };
                    products.Add(product);
                }
            }
            this.Connection.Close();
            return products;
        }

        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            string query = "SELECT * FROM Sales";

            SqlCommand command = new SqlCommand(query, this.Connection);
            this.Connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Sale sale = new Sale
                    {

                        SaleId = reader.GetInt32(0),
                        Quantity_Sold = reader.GetInt32(1),
                        Sale_Date = reader.GetDateTime(2),
                        Sale_Price = reader.GetDecimal(3),
                        TotalAmount = reader.GetDecimal(4),
                        CustomerName = reader.GetString(5),
                        ProductId = reader.GetInt32(6)
                    };
                    sales.Add(sale);
                }
            }
            return sales;
        }

        public void PostProduct(string productName, string productDescription,
            decimal productPrice, int productStock)
        {
            string query = "AddProduct";

            SqlCommand command = new SqlCommand(query, this.Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@productName", productName);
            command.Parameters.AddWithValue("@description", productDescription);
            command.Parameters.AddWithValue("@price", productPrice);
            command.Parameters.AddWithValue("@quantityInStock", productStock);

            this.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Product {productName} Created!\n");

            this.Connection.Close();
        }

        public void GetOrderedSales()
        {
            List<Sale> sales = new List<Sale>();
            string query = "SELECT * FROM Sales";

            SqlCommand command = new SqlCommand(query, this.Connection);
            this.Connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Sale sale = new Sale
                    {

                        SaleId = reader.GetInt32(0),
                        Quantity_Sold = reader.GetInt32(1),
                        Sale_Date = reader.GetDateTime(2),
                        Sale_Price = reader.GetDecimal(3),
                        TotalAmount = reader.GetDecimal(4),
                        CustomerName = reader.GetString(5),
                        ProductId = reader.GetInt32(6)
                    };
                    sales.Add(sale);
                }
            }
            Sale.OrderSale(sales);
        }

        public void GetMonthlyProductSales()
        {
            List<Product> products = GetProducts();
            List<Sale> sales = GetSales();

            var monthlyProductSales = products
                .Join(sales, p => p.ProductId, s => s.ProductId,
                (p, s) => new
                {
                    p,
                    s
                }).Where(ps => ps.s.Sale_Date.Month == DateTime.Now.Month &&
                ps.s.Sale_Date.Year == DateTime.Now.Year)
                .GroupBy(ps => new
                {
                    ps.p.ProductId,
                    ps.p.ProductName,
                    ps.p.Price
                })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    Price = g.Key.Price,
                    TotalSold = g.Sum(ps => ps.s.Quantity_Sold * ps.p.Price)
                })
                .OrderByDescending(x => x.TotalSold)
                .ToList();

            foreach (var productSales in monthlyProductSales)
            {
                Console.WriteLine($"Product: {productSales.ProductName} | " +
                    $"Total Sold: {productSales.TotalSold:F2} | " +
                    $"Product Price: {productSales.Price:F2} | ");
            }

        }

        public List<Product> GetProductsWithNoSaleOrPurchase()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT * FROM Products P LEFT JOIN Sales S ON " +
                "P.ProductId = S.ProductId " +
                "LEFT JOIN Purchases PR ON P.ProductId = PR.ProductId " +
                "WHERE S.ProductId IS NULL " +
                "AND PR.ProductId IS NULL";

            SqlCommand command = new SqlCommand(query, this.Connection);
            this.Connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        QuantityStock = reader.GetInt32(4),
                    };
                    products.Add(product);
                }
            }
            this.Connection.Close();
            return products;
        }

        public void GetSumOfProductSales()
        {
            List<Product> products = GetProducts();
            List<Sale> sales = GetSales();

            var monthlyProductSales = products
                .Join(sales, p => p.ProductId, s => s.ProductId,
                (p, s) => new
                {
                    p,
                    s
                })
                .GroupBy(ps => new
                {
                    ps.p.ProductName,
                    ps.s.Sale_Date,
                })
                .Select(g => new
                {
                    ProductName = g.Key.ProductName,
                    SaleDate = g.Key.Sale_Date.ToString("MMMM"),
                    TotalQuantitySold = g.Sum(ps => ps.s.Quantity_Sold)
                }).ToList();


            foreach (var productSales in monthlyProductSales)
            {
                Console.WriteLine($"Product: {productSales.ProductName} | " +
                    $"Sale Date: {productSales.SaleDate} | " +
                    $"Product Total Sold: {productSales.TotalQuantitySold:F2} | ");
            }

        }

    }
}
