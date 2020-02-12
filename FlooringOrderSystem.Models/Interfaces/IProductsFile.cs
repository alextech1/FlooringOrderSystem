using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models.Interfaces
{
    public interface IProductsFile
    {
        void ReadFile();
        string ProductType(string product);
        decimal CostPerSquareFoot(decimal costPerSquareFoot);
        decimal LaborCostPerSquareFoot(decimal laborCostPerSquareFoot);
    }
}
