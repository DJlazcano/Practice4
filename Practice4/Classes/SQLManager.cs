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
    }
}
