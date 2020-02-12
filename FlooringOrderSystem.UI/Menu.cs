using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.UI.Workflows;

namespace FlooringOrderSystem.UI
{
    public class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Program");
                Console.WriteLine("---------------------------");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("5. Quit");
                Console.WriteLine("\nEnter selection: ");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        OrderAddToListWorkflow addToListWorkflow = new OrderAddToListWorkflow();
                        addToListWorkflow.Execute();
                        break;
                    case "3":
                        OrderEditWorkflow editWorkflow = new OrderEditWorkflow();
                        editWorkflow.Execute();
                        break;
                    case "4":
                        OrderDeleteWorkflow deleteWorkflow = new OrderDeleteWorkflow();
                        deleteWorkflow.Execute();
                        break;
                    case "5":
                        Environment.Exit(0);
                        return;
                }

            }
        }
    }
}
