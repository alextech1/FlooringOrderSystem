using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Interfaces;
using FlooringOrderSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static List<Order> orderList = new List<Order>()
        {
            new Order(1,"06012013","Wise","OH","Ohio",6.25m,"Wood",100.00m,5.15m,4.75m,515.00m,475.00m,61.88m,1051.88m),
            new Order(2,"04042020","Alex","OH","Ohio",6.25m,"Wood",100.00m,5.15m,4.75m,515.00m,475.00m,61.88m,1051.88m),
            new Order(3,"06012020","Omar","OH","Ohio",6.25m,"Wood",100.00m,5.15m,4.75m,515.00m,475.00m,61.88m,1051.88m)
        };

        TaxesTestsFiles taxesFile = new TaxesTestsFiles();
        ProductsTestsFiles productsFile = new ProductsTestsFiles();

        public OrderTestRepository()
        {

        }

        public Order LoadOrder(int orderNumber)
        {
            return orderList.Where(x => x.OrderNumber == orderNumber).FirstOrDefault(); //response
        }

        public bool FindDate(string _date)
        {
            string givenDate = _date.Replace("/", "");

            foreach (var file in orderList)
            {
                if (file.Date == givenDate)
                {
                    return true;
                }
            }
            return false;
        }
     

        public Order AddOrder(string date, string customerName, string state,
            string productType, decimal area, bool confirmOrder)
        {
            var givenDate = date.Replace("/", "");

            if (!orderList.Any(x => x.Date == givenDate))
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
                                        areaInput, costPerSqInput, laborCostPerSqInput, materialCost, laborCost, tax, total, out order))
                {
                    return addToList.Order = null;
                }
                else if (confirmOrder)
                {
                    orderList.Add(order);
                }

                return order;
            }
            else if (orderList.Any(x => x.Date == givenDate)) //Orders file exist, must add new OrderNumber
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
                                        areaInput, costPerSqInput, laborCostPerSqInput, materialCost, laborCost, tax, total, out order))
                {
                    return addToList.Order = null;
                }
                else if (confirmOrder)
                {
                    orderList.Add(order);
                }
                else if (!confirmOrder)
                {
                    return order;
                }
            }

            return orderList.Where(x => x.Date == givenDate).FirstOrDefault();
        }

        //Update existing order
        public Order SaveOrder(int orderNumber, string date, string customerName, string state,
            string productType, decimal area)
        {
            var givenDate = date.Replace("/", "");

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

            return existingOrder;
        }

        public Order DeleteOrder(int orderNumber, string date)
        {
            var givenDate = date.Replace("/", "");
            var getDate = orderList.Where(y => y.Date == givenDate);
            var selectedOrder = getDate.FirstOrDefault(x => x.OrderNumber == orderNumber);

            orderList.Remove(selectedOrder);

            return selectedOrder;
        }
    }
}
