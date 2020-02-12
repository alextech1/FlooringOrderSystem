using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrder(int orderNumber);
        Order SaveOrder(int orderNumber, string date, string customerName, string state,
            string productType, decimal area);
        bool FindDate(string date);
        Order AddOrder(string date, string customerName, string state,
            string productType, decimal area, bool confirmOrder);
        Order DeleteOrder(int orderNumber, string date);
    }
}
