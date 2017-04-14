using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeverCurrency.Models
{
   public class IncomingRequest
    {
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Mode { get; set; }
    }
}
