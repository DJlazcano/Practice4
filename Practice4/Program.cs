using Practice4.Classes;
using System.Data;
using System.Net.Security;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Channels;

namespace Practice4
{
    public class Practice4
    {
        static void Main(string[] args)
        {
            int option = -1;
            string connectionString = @"Server=localhost;Database=Practice4_JM;Integrated Security=True;";
            do
            {
                Console.WriteLine("Please type in the number of the activity to be performed: ");
                Console.WriteLine("Press the 1 key to get the data from the products table." +
                    "\n Press the 2 key to request the creation of a new product" +
                    "\n Press the 3 key to execute the stored procedure related to the product table" +
                    "\n Press the 4 key to execute a visual execution of a view" +
                    "\n Press the 5 key to execute a sql command from linq" +
                    "\n Press the 6 key to perform a query to the products and purchases table data in linq" +
                    "\n Press the 7 key to perform the sum of the products sold in the month." +
                    "\n Press the 0 key to exit...");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:

                        List<Product> products = new List<Product>();
                        string query = "SELECT * FROM Products";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(query, connection);
                            connection.Open();

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
                        }

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

                        //var expensiveProducts = products.Where(p => p.Price > 50).ToList();
                        //foreach (Product product in expensiveProducts)
                        //{
                        //    Console.WriteLine(product.Price + " " + product.ProductName);
                        //}

                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    default:
                        break;
                }


            } while (option != 0);
        }
    }
}
