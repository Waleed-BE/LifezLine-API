using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCQRS.Core.Options
{
    //#OPTIONSPATTERN 1
    public class ConnectionStringsOptions
    {
        public const string SectionName = "ConnectionStrings";
        public string DefaultConnection { get; set; }
    }
}
