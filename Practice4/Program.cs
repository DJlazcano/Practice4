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
                Console.WriteLine("\nPlease type in the number of the activity to be performed: ");
                Console.WriteLine(" Press the 1 key to Get the data from the products table." +
                    "\n Press the 2 key to Request the creation of a new product" +
                    "\n Press the 3 key to Filter the information by text or Id from product table." +
                    "\n Press the 4 key to Group sales by day and sort them by sales number from highest to lowest." +
                    "\n Press the 5 key to Get the product and sales information for the month using line joins" +
                    "\n Press the 6 key to Get the products that don't have Sales." +
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
                        Console.WriteLine("Filter Product Data, Filter by Id press (1)" +
                            "\n Filter by text press (2):");

                        var filter = Convert.ToInt32(Console.ReadLine());


                        var filterVal = Console.ReadLine();

                        List<Product> filterProducts = sqlManager.GetProducts();

                        switch (filter)
                        {
                            case 1:
                                Console.WriteLine($"Enter the filter ID:");
                                int filterId = Convert.ToInt32(Console.ReadLine());

                                List<Product> filteredProductsById = Product.FilterProducts(filterProducts,
                                    filterId, "");

                                Product.ListProducts(filteredProductsById);

                                break;

                            case 2:
                                Console.WriteLine($"Enter the filter Text:");
                                var filterText = Console.ReadLine();

                                List<Product> filteredProductsByText = Product.FilterByText(filterProducts, filterText);

                                Product.ListProducts(filteredProductsByText);

                                break;
                            default:
                                break;
                        }

                        Product.ListProducts(filterProducts);

                        break;

                    case 4:

                        sqlManager.GetOrderedSales();

                        break;

                    case 5:
                        sqlManager.GetMonthlyProductSales();

                        break;

                    case 6:

                        List<Product> noSalesOrPurchases = sqlManager.GetProductsWithNoSaleOrPurchase();
                        Console.WriteLine("Products Not Sold nor Purchased: ");
                        Product.ListProducts(noSalesOrPurchases);

                        break;

                    case 7:

                        sqlManager.GetSumOfProductSales();

                        break;

                    default:
                        break;
                }


            } while (option != 0);
        }
    }
}
