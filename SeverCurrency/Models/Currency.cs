using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeverCurrency.Models
{
    class Currency
    {
        readonly string _code;
        public string Code { get { return this._code; } }
        public Rate Rate { get; private set; }
        public Currency(string code, string date, string rate)
        {
            this.Rate = new Rate(date, rate);
            this._code = code;
        }
        public override string ToString()
        {
            return string.Format("Date: {0}, Code: {1}, Rate: {2}", this.Rate.date, this._code, this.Rate.rate);
        }
    }
}
