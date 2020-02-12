using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Interfaces;
using FlooringOrderSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Data
{
    public class OrderProdRepository : IOrderRepository
    {
        public List<Order> orderList = new List<Order>();
        FilePath fp = new FilePath();
        TaxesFile taxesFile = new TaxesFile();
        ProductsFile productsFile = new ProductsFile();

        public OrderProdRepository()
        {            
            Directory.CreateDirectory(fp.filePathLoc); //Auto checks if directory exists
        }

        public Order LoadOrder(int orderNumber)
        {
            return orderList.Where(x => x.OrderNumber == orderNumber).FirstOrDefault(); //response
        }

        public bool FindDate(string _date)
        {            
            taxesFile.ReadFile();
            productsFile.ReadFile();
            string orderFilePath = $"{fp.filePathLoc}"; //Orders_06012013.txt
            string[] files = Directory.GetFiles(orderFilePath, $"Orders_*.txt", SearchOption.AllDirectories);
            string givenDate = _date.Replace("/", "");

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string[] getDate = fileName.Split("_.".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string date = getDate[1]; //captures date
                if (date == givenDate)
                {
                    if (!ReadFromFile(date))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ReadFromFile(string _date)
        {
            string orderFileName = $"{fp.filePathLoc}\\Orders_{_date}.txt";

            List<string> lines = File.ReadAllLines(orderFileName).ToList();

            foreach (var line in lines.Skip(1)) //?? new List<string>(0)
            {
                List<string> entry = line.Split(',').ToList();

                Order newOrder = new Order();
                int.TryParse(entry[0], out int orderNumber);
                newOrder.OrderNumber = orderNumber;
                newOrder.Date = _date;
                newOrder.CustomerName = entry[1]; //[a-z][0-9,.]
                newOrder.State = taxesFile.StateAbbreviation(entry[2]); //State: IL
                newOrder.StateName = taxesFile.StateName(newOrder.State); //State IL -> Illinois must check list
                newOrder.TaxRate = taxesFile.TaxRate(newOrder.State);
                newOrder.ProductType = productsFile.ProductType(entry[4]);
                decimal.TryParse(entry[5], out decimal area);
                newOrder.Area = area; //positive decimal and min size is 100.00
                decimal.TryParse(entry[6], out decimal costPerSquareFoot);
                newOrder.CostPerSquareFoot = costPerSquareFoot; //productsFile.CostPerSquareFoot(newOrder.ProductType);
                decimal.TryParse(entry[7], out decimal laborCostPerSquareFoot);
                newOrder.LaborCostPerSquareFoot = laborCostPerSquareFoot; //productsFile.LaborCostPerSquareFoot(newOrder.ProductType);
                decimal.TryParse(entry[8], out decimal materialCost);
                newOrder.MaterialCost = materialCost;
                decimal.TryParse(entry[9], out decimal laborCost);
                newOrder.LaborCost = laborCost;
                decimal.TryParse(entry[10], out decimal tax);
                newOrder.Tax = tax;
                decimal.TryParse(entry[11], out decimal total);
                newOrder.Total = total;

                newOrder.ArePropertiesNotNull();

                orderList.Add(newOrder);
            }
            return true;
        }

        
        public Order AddOrder(string date, string customerName, string state,
            string productType, decimal area, bool confirmOrder)
        {
            var givenDate = date.Replace("/", "");
            string orderFileName = $"{fp.filePathLoc}\\Orders_{givenDate}.txt";
            string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot," +
                "LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
            List<Order> newFileList = new List<Order>();

            if (!orderList.Any(x => x.Date == givenDate)) //Orders file not exist, create new OrderNumber
            {
                OrderAddToListResponse addToList = new OrderAddToListResponse();

                Order order;
                var newOrderNumber = 1;
                var customerNameInput = customerName;
                var stateInput = taxesFile.StateAbbreviation(state);
                var stateName = taxesFile.StateName(stateInput); //returns full state name
                var taxRateInput = taxesFile.TaxRate(stateInput);
                var productTypeInput = productsFile.ProductType(productType);
                var areaInput = area;
                var costPerSqInput = productsFile.CostPerSquareFoot(productTypeInput);
                var laborCostPerSqInput = productsFile.LaborCostPerSquareFoot(productTypeInput);
                var materialCost = areaInput * costPerSqInput;
                var laborCost = areaInput * laborCostPerSqInput;
                var tax = (materialCost + laborCost) * (taxRateInput / 100);
                var total = materialCost + laborCost + tax;


                if (!Order.TryParse(newOrderNumber, givenDate, customerNameInput, stateInput, stateName, taxRateInput, productTypeInput,
                                        areaInput, costPerSqInput, laborCostPerSqInput, materialCost,
                                        laborCost, tax, total, out order))
                {
                    return addToList.Order = null;
                }
                else if (confirmOrder)
                {
                    newFileList.Add(order);

                    List<string> ordersToSave = new List<string>() { header };
                    ordersToSave.AddRange(newFileList.Select(x => x.ToString()).ToList());

                    File.AppendAllLines(orderFileName, ordersToSave);
                }

                return order;
            }
            else if (orderList.Any(x => x.Date == givenDate)) //Orders file exist, must add max OrderNumber + 1
            {
                OrderAddToListResponse addToList = new OrderAddToListResponse();

                Order order;
                var maxIdPlusOne = orderList.Max(x => x.OrderNumber) + 1;
                var customerNameInput = customerName;
                var stateInput = taxesFile.StateAbbreviation(state);
                var stateName = taxesFile.StateName(stateInput);
                var taxRateInput = taxesFile.TaxRate(stateInput);
                var productTypeInput = productsFile.ProductType(productType);
                var areaInput = area;
                var costPerSqInput = productsFile.CostPerSquareFoot(productTypeInput);
                var laborCostPerSqInput = productsFile.LaborCostPerSquareFoot(productTypeInput);
                var materialCost = areaInput * costPerSqInput;
                var laborCost = areaInput * laborCostPerSqInput;
                var tax = (materialCost + laborCost) * (taxRateInput / 100);
                var total = materialCost + laborCost + tax;


                if (!Order.TryParse(maxIdPlusOne, givenDate, customerNameInput, stateInput, stateName, taxRateInput, productTypeInput,
                                        areaInput, costPerSqInput, laborCostPerSqInput, materialCost, 
                                        laborCost, tax, total, out order))
                {
                    return addToList.Order = null;
                }
                else if(confirmOrder)
                {
                    newFileList.Add(order);

                    List<string> ordersToSave = new List<string>() { header };
                    ordersToSave.AddRange(newFileList.Select(x => x.ToString()).ToList());

                    File.AppendAllLines(orderFileName, ordersToSave.Skip(1));
                }
                else if (!confirmOrder)
                {
                    return order;
                }
            }

            return newFileList.Where(x => x.Date == givenDate).FirstOrDefault();
        }

        //Update existing order
        public Order SaveOrder(int orderNumber, string date, string customerName, string state,
            string productType, decimal area)
        {
            var givenDate = date.Replace("/", "");
            string orderFileName = $"{fp.filePathLoc}\\Orders_{givenDate}.txt";
            string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot," +
                "LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";

            Order order;
            OrderEditResponse editList = new OrderEditResponse();

            var existingOrder = orderList.FirstOrDefault(x => x.OrderNumber == orderNumber);
            var getOrderNumber = existingOrder.OrderNumber;
            existingOrder.Date = givenDate;
            existingOrder.CustomerName = customerName;
            existingOrder.State = taxesFile.StateAbbreviation(state);
            existingOrder.StateName = taxesFile.StateName(existingOrder.State);
            existingOrder.TaxRate = taxesFile.TaxRate(existingOrder.State);
            existingOrder.ProductType = productsFile.ProductType(productType);
            existingOrder.Area = area;
            existingOrder.CostPerSquareFoot = productsFile.CostPerSquareFoot(existingOrder.ProductType);
            existingOrder.LaborCostPerSquareFoot = productsFile.LaborCostPerSquareFoot(existingOrder.ProductType);
            existingOrder.MaterialCost = existingOrder.Area * existingOrder.CostPerSquareFoot;
            existingOrder.LaborCost = existingOrder.Area * existingOrder.LaborCostPerSquareFoot;
            existingOrder.Tax = (existingOrder.MaterialCost + existingOrder.LaborCost) * (existingOrder.TaxRate / 100);
            existingOrder.Total = existingOrder.MaterialCost + existingOrder.LaborCost + existingOrder.Tax;

            if (!Order.TryParse(getOrderNumber, existingOrder.Date, existingOrder.CustomerName, existingOrder.State, existingOrder.StateName, existingOrder.TaxRate, existingOrder.ProductType,
                                         existingOrder.Area, existingOrder.CostPerSquareFoot, existingOrder.LaborCostPerSquareFoot,
                                         existingOrder.MaterialCost, existingOrder.LaborCost, existingOrder.Tax, existingOrder.Total, out order))
            {
                return editList.Order = null;
            }

            List<string> ordersToSave = new List<string>() { header };
            ordersToSave.AddRange(orderList.Select(x => x.ToString()).ToList());

            File.WriteAllLines(orderFileName, ordersToSave);

            return existingOrder;
        }

        public Order DeleteOrder(int orderNumber, string date)
        {
            var givenDate = date.Replace("/", "");

            var getDate = orderList.Where(y => y.Date == givenDate);
            var selectedOrder = getDate.FirstOrDefault(x => x.OrderNumber == orderNumber);

            orderList.Remove(selectedOrder);

            string orderFileName = $"{fp.filePathLoc}\\Orders_{givenDate}.txt";
            string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot," +
                "LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";

            List<string> ordersToSave = new List<string>() { header };
            ordersToSave.AddRange(orderList.Select(x => x.ToString()).ToList());

            File.WriteAllLines(orderFileName, ordersToSave);

            return selectedOrder;
        }

    }
}
