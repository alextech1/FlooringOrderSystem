using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models.Responses
{
    public class OrderDateExist
    {
        public Order Order { get; set; }
        public bool DoesOrderDateExist { get; set; }
        public bool IsDateFormatOk { get; set; }
        public bool IsFutureDate { get; set; }
        public bool IsAreaCorrect { get; set; }
        public bool IsNameCorrect { get; set; }
    }
}
