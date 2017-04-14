using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SeverCurrency.Models
{
    class ProcessingOfData
    {
        DateTime _dateStart;
        DateTime _dateEnd;
        string _mode;

        List<Currency> _listCurrencyFromXml;
        List<Currency> _listCurrencyFromScv;

        string _pathToXml;
        string _pathToScv;

        public ProcessingOfData(IncomingRequest request)
        {
            this._dateStart = DateTime.ParseExact(request.DateStart, "yyyyMMdd", CultureInfo.InvariantCulture);
            this._dateEnd = DateTime.ParseExact(request.DateEnd, "yyyyMMdd", CultureInfo.InvariantCulture);
            this._mode = request.Mode;

            this._listCurrencyFromScv = new List<Currency>();
            this._listCurrencyFromXml = new List<Currency>();

            string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "App_Data\\";
            this._pathToScv = path+"rates.csv";
            this._pathToXml = path+"rates.xml";
        }
        public string GetResult()
        {

            ReadXmlFormat();
            ReadScvFormat();
            if (this._mode == "average_value_1")
            {
                return AverageValue_1();
            }
            else if (this._mode == "average_value_2")
            {
                return AverageValue_2();

            }
            return "error";
        }

        private string AverageValue_1()
        {
            List<JustCurrency> listJustCurrency = new List<JustCurrency>();
            List<JustCurrency> listJustCurrency2 = new List<JustCurrency>();

            ProcessingData(this._listCurrencyFromXml, listJustCurrency);

            ProcessingData(this._listCurrencyFromScv, listJustCurrency2);

            listJustCurrency.AddRange(listJustCurrency2.ToList<JustCurrency>());
            OrderByNameCurrency(listJustCurrency);

            return ProcessingDataFromJustCurrency(listJustCurrency);

        }
        private string AverageValue_2()
        {
            List<JustCurrency> listJustCurrency = new List<JustCurrency>();

            this._listCurrencyFromXml.AddRange(this._listCurrencyFromScv.ToList<Currency>());

            ProcessingData(this._listCurrencyFromXml, listJustCurrency);
            OrderByNameCurrency(listJustCurrency);
            return ProcessingDataFromJustCurrency(listJustCurrency);
        }

        private void OrderByNameCurrency(List<JustCurrency> listJustCurrency)
        {
            listJustCurrency.Sort((x,y) => x.Code.CompareTo(y.Code));//.ToList<JustCurrency>();
        }

        private string ProcessingDataFromJustCurrency(List<JustCurrency> listJustCurrency)
        {

            StringBuilder strBilder = new StringBuilder();
            strBilder.Append("{rates:[");

            foreach (string item in listJustCurrency.Select(x => x.Code).Distinct().ToList<string>())
            {
                decimal rate = listJustCurrency.Where(x => x.Code == item).Average(x => x.Rate);
                strBilder.Append("" + item.ToLower() + ":{rate:" + rate + "},");
            }
            strBilder.Append("]}");
            return strBilder.ToString().Replace("},]}", "}]}");
        }

        private void ProcessingData(List<Currency> curr, List<JustCurrency> listJustCurrency)
        {

            foreach (string code in curr.Select(x => x.Code).Distinct().ToList<string>())
            {
                List<Currency> temp = curr.Where(x => x.Code == code && x.Rate.date >= this._dateStart && x.Rate.date <= this._dateEnd).ToList<Currency>();
                if (temp.Count != 0)
                {
                    decimal rate = temp .Average(x => x.Rate.rate);
                listJustCurrency.Add(new JustCurrency { Rate = rate, Code = code });

                }
            }
        }


        private void ReadXmlFormat()
        {
            if (File.Exists(this._pathToXml)==false) return;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(this._pathToXml);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                string currency_code = xnode.Attributes.GetNamedItem("code").Value.Trim();

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    Currency currency = new Currency(
                        code: currency_code.Trim(),
                        date: childnode.Attributes.GetNamedItem("date").Value.Trim(),
                        rate: childnode.Attributes.GetNamedItem("rate").Value.Trim());

                    _listCurrencyFromXml.Add(currency);
                }
            }
        }
        private void ReadScvFormat()
        {
            if (File.Exists(this._pathToScv) == false) return;

            using (var fs = File.OpenRead(this._pathToScv))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Currency currency = new Currency(
                        code: values[0].ToString().Trim(),
                        date: values[1].ToString().Trim(),
                        rate: values[2].ToString().Trim());

                    _listCurrencyFromScv.Add(currency);
                }
            }
        }
    }
}
