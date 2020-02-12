using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models.Responses;
using FlooringOrderSystem.Models;
using FlooringOrderSystem.Data;

namespace FlooringOrderSystem.UI.Workflows
{
    public class OrderAddToListWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine("Add an Order");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Today is {DateTime.Now.ToString("MM/dd/yyyy")}");
            
            string date;
            while (true)
            {
                Console.WriteLine("Enter future date as (MM/dd/yyyy): ");

                string dateInput = Console.ReadLine();
                if (dateInput == "") { Console.WriteLine("null not allowed"); continue; }
                else date = dateInput;
                break;
            }

            Console.WriteLine("Enter Customer Name: ");
            string customerName = Console.ReadLine();

            TaxesFile taxesClassFile = new TaxesFile();
            taxesClassFile.ReadFile();
            var myListTax = taxesClassFile.taxFile.Select(x => x.TaxFile).ToList();

            Console.WriteLine();
            Console.WriteLine("State,StateName,TaxRate");
            for (int i = 0; i < myListTax.Count; i++)
            {
                Console.WriteLine(myListTax[i]);
            }

            Console.WriteLine("Enter State: ");
            string state = Console.ReadLine();


            ProductsFile productsClassFile = new ProductsFile();
            productsClassFile.ReadFile();
            var myListProducts = productsClassFile.productsFile.Select(x => x.ProductsFile).ToList();

            Console.WriteLine();
            Console.WriteLine("ProductType,CostPerSquareFoot,LaborCostPerSquareFoot");
            for (int i = 0; i < myListProducts.Count; i++)
            {
                Console.WriteLine(myListProducts[i]);
            }

            Console.WriteLine("Enter Product Type: ");
            string productType = Console.ReadLine();

            decimal area;
            while (true)
            {
                Console.WriteLine("Enter Area: ");
                
                string areaInput = Console.ReadLine();
                if (areaInput == "") { Console.WriteLine("null not allowed"); continue; }
                else if (!decimal.TryParse(areaInput, out area))
                {
                    Console.WriteLine("characters not allowed");
                    continue;
                }
                break;
            }

            OrderAddToListResponse response;
            response = manager.AddOrder(date, customerName, state, productType, area);

            if (response.Order != null)
            {
                Console.WriteLine("Please confirm this order:");
                Console.WriteLine($"Order Number: {response.Order.OrderNumber}");
                Console.WriteLine($"Future date: {response.Order.Date}");
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
                Console.WriteLine($"Error: {response.Message}");
            }
            

            Console.WriteLine("Are you sure you want to add this order (y/n)?");
            string inputVal = Console.ReadLine();

            if (inputVal == "Y" || inputVal == "y")
            {
                AddOrderConfirmResponse response2;

                response2 = manager.ConfirmAddResponse(date, customerName, state, productType, area);

                if (response2.Success && response2.Order != null)
                {
                    Console.WriteLine("Order added to list!");
                    Console.WriteLine($"Order Number: {response2.Order.OrderNumber}");
                    Console.WriteLine($"Future date: {response2.Order.Date}");
                    Console.WriteLine($"Customer Name: {response2.Order.CustomerName}");
                    Console.WriteLine($"State: {response2.Order.State}");
                    Console.WriteLine($"Tax Rate: {response2.Order.TaxRate}");
                    Console.WriteLine($"Product Type: {response2.Order.ProductType}");
                    Console.WriteLine($"Area: {response2.Order.Area}");
                    Console.WriteLine($"Cost Per Square: ${response2.Order.CostPerSquareFoot.ToString("#.##")}");
                    Console.WriteLine($"Labor Cost Per Square Foot: ${response2.Order.LaborCostPerSquareFoot.ToString("#.##")}");
                    Console.WriteLine($"Tax: ${response2.Order.Tax.ToString("#.##")}");
                    Console.WriteLine($"Total: ${response2.Order.Total.ToString("#.##")}");
                }
                else
                {
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(response2.Message);
                }
            } else
            {
                Console.WriteLine("Adding order has been cancelled.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
    }
}
