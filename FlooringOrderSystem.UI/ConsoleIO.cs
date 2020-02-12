using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Models;

namespace FlooringOrderSystem.UI
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(Order order)
        {
            Console.WriteLine($"{order.OrderNumber} | [{order.Date}]");
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"Area: {order.Area}");
            Console.WriteLine($"State Name: {order.StateName}");
            Console.WriteLine($"Product: [{order.ProductType}]");
            Console.WriteLine($"Materials: [${order.MaterialCost.ToString("#.##")}]");
            Console.WriteLine($"Labor: [${order.LaborCost.ToString("#.##")}]");
            Console.WriteLine($"Cost Per Square: ${order.CostPerSquareFoot.ToString("#.##")}");
            Console.WriteLine($"Tax: ${order.Tax.ToString("#.##")}");
            Console.WriteLine($"Total: ${order.Total.ToString("#.##")}");
        }
    }
}
