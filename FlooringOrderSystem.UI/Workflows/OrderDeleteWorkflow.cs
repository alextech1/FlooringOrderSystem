using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models.Responses;

namespace FlooringOrderSystem.UI
{
    public class OrderDeleteWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine("Delete an Order");
            Console.WriteLine("-----------------------");            
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

            Console.WriteLine("Enter existing date as (MM/dd/yyyy): ");
            string date = Console.ReadLine();

            OrderLookupResponse response = manager.DeleteOrder(orderNumber, date);

            if (response.Success)
            {
                Console.WriteLine("Order deleted from list!");
                Console.WriteLine($"Order Number: {response.Order.OrderNumber}");
                Console.WriteLine($"Date: {response.Order.Date}");
                Console.WriteLine($"Customer Name: {response.Order.CustomerName}");
                Console.WriteLine($"State: {response.Order.State}");
                Console.WriteLine($"Tax Rate: {response.Order.TaxRate}");
                Console.WriteLine($"Product Type: {response.Order.ProductType}");
                Console.WriteLine($"Area: {response.Order.Area}");
                Console.WriteLine($"Cost Per Square: ${response.Order.CostPerSquareFoot.ToString("#.##")}");
                Console.WriteLine($"Labor Cost Per Square Foot: ${response.Order.LaborCostPerSquareFoot.ToString("#.##")}");
                Console.WriteLine($"Tax: ${response.Order.Tax.ToString("#.##")}");
                Console.WriteLine($"Total: ${response.Order.Total.ToString("#.##")}");
            }
            else
            {
                Console.WriteLine($"An error occured: {response.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


        }
    }
}
