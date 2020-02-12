using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Data
{
    public static class ObjectExtensions
    {
        public static bool ArePropertiesNotNull<Order>(this Order obj)
        {        
            return typeof(Order).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }        

        
    }
}
