using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SeverCurrency.Models
{
    class Rate
    {
        readonly DateTime _date;
        readonly Decimal _rate;
        public DateTime date { get { return this._date; } }
        public Decimal rate { get { return this._rate; } }

        public Rate(string date, string rate)
        {
            //http://stackoverflow.com/questions/21906935/how-to-initialize-a-datetime-field
            this._date = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            //http://stackoverflow.com/questions/23131414/culture-invariant-decimal-tryparse
            this._rate = decimal.Parse(rate, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}
