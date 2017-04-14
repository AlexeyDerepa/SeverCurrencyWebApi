using SeverCurrency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeverCurrency.Controllers
{
    public class ValuesController : ApiController
    {
        ProcessingOfData p;

        // GET api/values
        public string Get()
        {
            p = new ProcessingOfData(new IncomingRequest { Mode = "average_value_12", DateStart = "20161201", DateEnd = "20170103" });
            return p.GetResult();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        public string Put(int id, [FromBody]IncomingRequest data)
        {
            p = new ProcessingOfData(data);
            //p = new ProcessingOfData(new IncomingRequest { Mode = "average_value_" + id, DateStart = "20161201", DateEnd = "20170103" });
            return p.GetResult();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}