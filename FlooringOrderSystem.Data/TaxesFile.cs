using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Interfaces;

namespace FlooringOrderSystem.Data
{
    public class TaxesFile //: ITaxesFile
    {
        public List<Order> taxFile = new List<Order>();
        FilePath fp = new FilePath();

        public void ReadFile()
        {
            string taxFileName = $"{fp.filePathLoc}\\Taxes.txt";
            List<string> taxLines = File.ReadAllLines(taxFileName).ToList();

            foreach (var taxLine in taxLines.Skip(1))
            {
                List<string> entry = taxLine.Split(',').ToList();

                Order taxOrder = new Order();
                taxOrder.State = entry[0];
                taxOrder.StateName = entry[1];
                decimal.TryParse(entry[2], out decimal _taxRate);
                taxOrder.TaxRate = _taxRate;

                taxFile.Add(taxOrder);
            }
        }

        public string StateAbbreviation(string input)
        {
            if (taxFile.Any(x => x.State == input))
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
            if (taxFile.Any(s => s.State == abbr))
            {
                var getStateName = taxFile.Where(x => x.State == abbr);

                return getStateName.Select(x => x.StateName).FirstOrDefault();
            }
            else
            {
                return "";
            }
        }

        public decimal TaxRate(string state)
        {
            if (taxFile.Any(y => y.State == state))
            {
                var getTaxRate = taxFile.Where(x => x.State == state);

                return getTaxRate.Select(x => x.TaxRate).FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }
    }
}
