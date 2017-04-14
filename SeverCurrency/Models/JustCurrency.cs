using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeverCurrency.Models
{
    class JustCurrency
    {
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public override string ToString()
        {
            return string.Format("Code: {0}, Rate: {1}", Code, Rate);
        }
    }
}
