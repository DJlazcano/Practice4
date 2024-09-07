using Practice4.Classes;
using System.Data;
using System.Net.Security;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Channels;
using System.Diagnostics;
using System.Net;

namespace Practice4
{
    public class Practice4
    {
        static void Main(string[] args)
        {
            int option = -1;
            SQLManager sqlManager = new SQLManager();

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

                        List<Product> products = sqlManager.GetProducts();

                        Product.ListProducts(products);

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

                        sqlManager.PostProduct(productName,
                            productDescription,
                            productPrice, productStock);

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
