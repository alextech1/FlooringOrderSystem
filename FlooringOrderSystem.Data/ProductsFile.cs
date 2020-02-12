using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Interfaces;
using System.IO;

namespace FlooringOrderSystem.Data
{
    public class ProductsFile// : IProductsFile
    {
        public List<Order> productsFile = new List<Order>();
        FilePath fp = new FilePath();

        public void ReadFile()
        {
            string productsFileName = $"{fp.filePathLoc}\\Products.txt";
            List<string> productsLines = File.ReadAllLines(productsFileName).ToList();

            foreach (var productLine in productsLines.Skip(1))
            {
                List<string> entry = productLine.Split(',').ToList();

                Order productsOrder = new Order();
                productsOrder.ProductType = entry[0];
                decimal.TryParse(entry[1], out decimal _costPerSquareFoot);
                productsOrder.CostPerSquareFoot = _costPerSquareFoot;
                decimal.TryParse(entry[2], out decimal _laborCostPerSquareFoot);
                productsOrder.LaborCostPerSquareFoot = _laborCostPerSquareFoot;
                
                productsFile.Add(productsOrder);
            }
        }

        public string ProductType(string productType)
        {
            if(productsFile.Any(x => x.ProductType == productType))
            {
                return productType;
            }
            else
            {
                return null;
            }
        }

        public decimal CostPerSquareFoot(string productType)
        {
            if(productsFile.Any(x => x.ProductType == productType))
            {
                var getCostPerSquareFoot = productsFile.Where(x => x.ProductType == productType);
                return getCostPerSquareFoot.Select(x => x.CostPerSquareFoot).FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }

        public decimal LaborCostPerSquareFoot(string labCostPerSquareFoot)
        {
            if(productsFile.Any(x => x.ProductType == labCostPerSquareFoot))
            {
                var getLaborCostPerSquareFoot = productsFile.Where(x => x.ProductType == labCostPerSquareFoot);
                return getLaborCostPerSquareFoot.Select(x => x.LaborCostPerSquareFoot).FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }
    }
}
