using Practice4.Classes;
using System.Data;
using System.Net.Security;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Channels;
using System.Diagnostics;

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

                        break;

                    case 2:
                        Console.WriteLine("Create a new Product");
                        Console.WriteLine("Fill the following data:");

                        Console.WriteLine("Please enter Product Name:");
                        string productName = Console.ReadLine();

                        Console.WriteLine("Please enter Product Description:");
                        string productDescription = Console.ReadLine();

                        Console.WriteLine("Please enter Product Price:");
                        decimal productPrice = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("Please enter Product Quantity for Stock:");
                        int productStock = Convert.ToInt32(Console.ReadLine());

                        //EXEC AddProduct @productName = 'Grapes',
                        //@description = 'Small but sweet',
                        //@price = 29.1,
                        //@quantityInStock = 72;

                        string query2 = "AddProduct";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(query2, connection);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@productName", productName);
                            command.Parameters.AddWithValue("@description", productDescription);
                            command.Parameters.AddWithValue("@price", productPrice);
                            command.Parameters.AddWithValue("@quantityInStock", productStock);

                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            Console.WriteLine($"Product {productName} Created!\n");
                        }

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
