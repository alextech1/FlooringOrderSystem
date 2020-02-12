using FlooringOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlooringOrderSystem.Models.Order;

namespace FlooringOrderSystem.Data
{
    public class TaxesTestsFiles
    {
        private static List<Taxes> taxesTestsFile = new List<Taxes>()
        {
            new Taxes("OH","Ohio",6.25m),
            new Taxes("PA","Pennsylvania",6.75m),
            new Taxes("MI","Michigan",5.75m),
            new Taxes("IN","Indiana",6.00m)
        };

        public string StateAbbreviation(string input)
        {
            if (taxesTestsFile.Any(x => x.State == input))
            {
                return input;
            }
            else
            {
                return null;
            }
        }

        public string StateName(string abbr)
        {
            if (taxesTestsFile.Any(s => s.State == abbr))
            {
                var getStateName = taxesTestsFile.Where(x => x.State == abbr);

                return getStateName.Select(x => x.StateName).FirstOrDefault();
            }
            else
            {
                return "";
            }
        }

        public decimal TaxRate(string state)
        {
            if (taxesTestsFile.Any(y => y.State == state))
            {
                var getTaxRate = taxesTestsFile.Where(x => x.State == state);

                return getTaxRate.Select(x => x.TaxRate).FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }
    }
}
