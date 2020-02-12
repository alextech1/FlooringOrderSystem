using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models.Responses;

namespace FlooringOrderSystem.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Console.Clear();
            Console.WriteLine("Lookup an order");
            Console.WriteLine("------------------------");
            int orderNumber;
            while (true)
            {
                Console.WriteLine("Enter Order Number: ");

                string orderInput = Console.ReadLine();
                if (orderInput == "") { Console.WriteLine("null not allowed"); continue; }
                else if (!int.TryParse(orderInput, out orderNumber))
                {
                    Console.WriteLine("characters not allowed");
                    continue;
                }
                break;
            }

            Console.WriteLine("Enter date as (MM/dd/yyyy): ");
            string date = Console.ReadLine();

            OrderLookupResponse response = manager.LookupOrder(orderNumber, date);

            if (response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Order);
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
    }
}
