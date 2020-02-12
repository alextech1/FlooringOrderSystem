using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlooringOrderSystem.Models;

namespace FlooringOrderSystem.Data
{
    public class Validation
    {
        public bool CharactersValidation(string input)
        {
            if(Regex.IsMatch(input, @"^[a-zA-Z0-9,.]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Area(decimal area)
        {
            if (Regex.IsMatch(area.ToString(), @"^([1-9][0-9]{3,}|12[0-9]|1[3-9][0-9]|[0-9][0-9][0-9])\d*(.[0-9][0-9])*")) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }        

        public bool ValidFormat(string date)
        {
            if (Regex.IsMatch(date, @"^(0[1-9]|1[012])[ / ](0[1-9]|[12][0-9]|3[01])[ / ](19|20)\d\d$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FutureDate(string date)
        {
            DateTime present = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime given = Convert.ToDateTime(date);

            if (given > present)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
