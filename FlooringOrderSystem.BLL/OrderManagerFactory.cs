using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Data;

namespace FlooringOrderSystem.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();


            switch (mode)
            {
                case "Test":
                    return new OrderManager(new OrderTestRepository());
                case "Prod":
                    return new OrderManager(new OrderProdRepository());
                default:
                    throw new Exception("Mode value not configured correctly");
            }
        }
    }
}
