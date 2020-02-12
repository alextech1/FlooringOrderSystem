using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models
{    

    public class Order
    {
        public int OrderNumber { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public Order(int orderNumber, string date, string customerName, string state, string stateName,
            decimal taxRate, string productType, decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot,
            decimal materialCost, decimal laborCost, decimal tax, decimal total) : this()
        {
            OrderNumber = orderNumber;
            Date = date;
            CustomerName = customerName;
            State = state;
            StateName = stateName;
            TaxRate = taxRate;
            ProductType = productType;
            Area = area;
            CostPerSquareFoot = costPerSquareFoot;
            LaborCostPerSquareFoot = laborCostPerSquareFoot;
            MaterialCost = materialCost;
            LaborCost = laborCost;
            Tax = tax;
            Total = total;
        }

        public string TaxFile => $"{State},{StateName},{TaxRate}";
        public string ProductsFile => $"{ProductType},{CostPerSquareFoot},{LaborCostPerSquareFoot}";

        public Order() { }

        public Order(string order)
        {
            List<string> orderInfo = order.Split(',').ToList();

            int.TryParse(orderInfo.FirstOrDefault(), out int orderNumber);
            OrderNumber = orderNumber;
            CustomerName = orderInfo.Skip(1).FirstOrDefault();
            State = orderInfo.Skip(2).FirstOrDefault();
            ProductType = orderInfo.Skip(4).FirstOrDefault();
            decimal.TryParse(orderInfo.Skip(5).FirstOrDefault(), out decimal area);
            Area = area;
        }

        public class Product : Order
        {
            public Product(string productType, decimal costPerSqFoot, decimal labCostPerSqFoot)
            {
                ProductType = productType;
                CostPerSquareFoot = costPerSqFoot;
                LaborCostPerSquareFoot = labCostPerSqFoot;
            }

        }

        public class Taxes : Order
        {
            public Taxes(string state, string stateName , decimal taxRate)
            {
                State = state;
                StateName = stateName;
                TaxRate = taxRate;
            }
        }

        public override string ToString()
        {
            return $"{OrderNumber},{CustomerName},{State},{TaxRate},{ProductType}," +
                $"{Area},{CostPerSquareFoot},{LaborCostPerSquareFoot},{MaterialCost},{LaborCost},{Tax},{Total}";
        }

        public static bool TryParse(int orderNumber, string date, string customerName, string state, string stateName, decimal taxRate, 
            string productType, decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot, decimal materialCost, 
            decimal laborCost, decimal tax, decimal total, out Order order)
        {
            order = null;

            if (orderNumber > 0 && !string.IsNullOrEmpty(date) &&
                !string.IsNullOrEmpty(customerName) && !string.IsNullOrEmpty(state) &&
                taxRate > 0 &&
                !string.IsNullOrEmpty(productType) && area > 0 &&
                costPerSquareFoot > 0 && laborCostPerSquareFoot > 0)
            {
                order = new Order(orderNumber, date, customerName, state, stateName, taxRate, productType,
                area, costPerSquareFoot, laborCostPerSquareFoot, materialCost, laborCost, tax, total);
            }

            return order == null ? false : true;
        }

        
    }
}
