using FlooringOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlooringOrderSystem.Models.Order;

namespace FlooringOrderSystem.Data
{
    public class ProductsTestsFiles
    {
        private static List<Product> productsTestsFile = new List<Product>()
        {            
            new Product("Carpet",2.25m,2.10m),
            new Product("Laminate",1.75m,2.10m),
            new Product("Tile",3.50m,4.15m),
            new Product("Wood",5.15m,4.75m)
        };

        public string ProductType(string productType)
        {
            if (productsTestsFile.Any(x => x.ProductType == productType))
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
            if (productsTestsFile.Any(x => x.ProductType == productType))
            {
                var getCostPerSquareFoot = productsTestsFile.Where(x => x.ProductType == productType);
                return getCostPerSquareFoot.Select(x => x.CostPerSquareFoot).FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }

        public decimal LaborCostPerSquareFoot(string labCostPerSquareFoot)
        {
            if (productsTestsFile.Any(x => x.ProductType == labCostPerSquareFoot))
            {
                var getLaborCostPerSquareFoot = productsTestsFile.Where(x => x.ProductType == labCostPerSquareFoot);
                return getLaborCostPerSquareFoot.Select(x => x.LaborCostPerSquareFoot).FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }

    }
}
