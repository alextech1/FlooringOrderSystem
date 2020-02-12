using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models.Interfaces
{
    public interface ITaxesFile
    {
        void ReadFile();
        string StateAbbreviation(string input);
        string StateName(string abbr);
        decimal TaxRate(decimal taxRate);
    }
}
