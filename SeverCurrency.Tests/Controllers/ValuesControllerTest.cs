using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeverCurrency;
using SeverCurrency.Controllers;
using SeverCurrency.Models;

namespace SeverCurrency.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {

        [TestMethod]
        public void PutNotNull()
        {

            ValuesController controller = new ValuesController();
            IncomingRequest data = new IncomingRequest { Mode = "average_value_1", DateStart = "20161201", DateEnd = "20170103" };
            string result = controller.Put(5, data);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PutError()
        {

            ValuesController controller = new ValuesController();
            IncomingRequest data = new IncomingRequest { Mode = "someMethod", DateStart = "20161201", DateEnd = "20170103" };
            string result = controller.Put(5, data);
            Assert.AreEqual("error", result);
        }
        [TestMethod]
        public void PutNotFoundData()
        {

            ValuesController controller = new ValuesController();
            IncomingRequest data = new IncomingRequest { Mode = "average_value_1", DateStart = "20261201", DateEnd = "20370103" };
            string result = controller.Put(5, data);
            Assert.AreEqual("{rates:[]}", result);
        }
        [TestMethod]
        public void PutFoundData_method_1()
        {

            ValuesController controller = new ValuesController();
            IncomingRequest data = new IncomingRequest { Mode = "average_value_1", DateStart = "19920610", DateEnd = "19920624" };
            string result = controller.Put(5, data);
            Assert.AreEqual("{rates:[dem:{rate:35,74},jpy:{rate:4,44},usd:{rate:56,373333333333333333333333333}]}", result);
        }
        [TestMethod]
        public void PutFoundData_method_2()
        {

            ValuesController controller = new ValuesController();
            IncomingRequest data = new IncomingRequest { Mode = "average_value_2", DateStart = "19920610", DateEnd = "19920624" };
            string result = controller.Put(5, data);
            Assert.AreEqual("{rates:[dem:{rate:35,74},jpy:{rate:4,44},usd:{rate:56,373333333333333333333333333}]}", result);
        }

    }
}
