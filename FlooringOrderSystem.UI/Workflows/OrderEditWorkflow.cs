using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.UI.Workflows
{
    public class OrderEditWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine("Edit an Order");
            Console.WriteLine("--------------------");
            Console.WriteLine("Enter Order Number: ");
            string orderNumberInput = Console.ReadLine();
            int.TryParse(orderNumberInput, out int orderNumber);

            string date;
            while (true)
            {
                Console.WriteLine("Enter existing date as (MM/dd/yyyy): ");

                string dateInput = Console.ReadLine();
                if (dateInput == "") { Console.WriteLine("null not allowed"); continue; }
                else date = dateInput;
                break;
            }


            OrderEditResponse response = manager.CheckOrderExist(orderNumber, date);

            if (response.Success)
            {
                Console.WriteLine($"Enter Customer Name({response.Order.CustomerName}): ");
                string customerName = Console.ReadLine(); 
                if (customerName == "") { customerName = response.Order.CustomerName; }
                
                Console.WriteLine($"Enter State({response.Order.State}): ");                
                string state = Console.ReadLine();
                if (state == "") { state = response.Order.State; }
                
                Console.WriteLine($"Enter Product Type({response.Order.ProductType}): ");
                string productType = Console.ReadLine();
                if (productType == "") { productType = response.Order.ProductType; }
                
                Console.WriteLine($"Enter Area({response.Order.Area}): ");
                decimal area;
                string areaInput = Console.ReadLine();
                if (areaInput == "") { area = response.Order.Area; }
                else decimal.TryParse(areaInput, out area);

                Console.WriteLine($"Order Number: {orderNumber}");
                Console.WriteLine($"Date: {date}");
                Console.WriteLine($"Name: {customerName}");
                Console.WriteLine($"State: {state}");
                Console.WriteLine($"Product: {productType}");
                Console.WriteLine($"Area: {area}");

                Console.WriteLine("Are you sure you want to save these values (y/n)?");
                string inputSave = Console.ReadLine();
                if (inputSave == "Y" || inputSave == "y")
                {
                    OrderEditResponse newResponse = manager.EditOrder(orderNumber, date, customerName, state,
                    productType, area);
                } else
                {
                    response.Message = "Save Cancelled";
                    response.Success = false;
                }

                

                if (response.Success)
                {
                    Console.WriteLine("Order edited from list!");
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
                else if (!response.Success)
                {
                    Console.WriteLine($"{response.Message}");
                }
            }
            else
            {
                Console.WriteLine($"An error has occured: {response.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            

        }

    }
}
