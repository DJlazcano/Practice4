namespace Practice4
{
    public class Practice4
    {
        static void Main(string[] args)
        {
            int option = -1;
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

                Console.WriteLine(option);

                switch (option)
                {
                    case 1:
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
