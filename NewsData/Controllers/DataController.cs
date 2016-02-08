using DataJob;
using DataJob.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace NewsData
{
    public class DataController : ApiController
    {
        private static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return epoch.AddSeconds(unixTime);
        }

        public class data
        {
            public List<string> score = new List<string>();
            public List<string> date = new List<string>();
        }

        public data Get(string type)
        {
            data r;
            using (Context db = new Context())
            {
                List<Sentiment> l = db.Sentiment.Where(x => x.type == type).ToList();
                l.Sort((x, y) => x.timestamp.CompareTo(y.timestamp));
                r = CreateResponse(l);
            }
            Console.WriteLine("API request received - " + DateTime.Now);
            return r;
        }

        public data Get()
        {
            data r;
            using (Context db = new Context())
            {
                List<Sentiment> l = db.Sentiment.ToList();
                l.Sort((x, y) => x.timestamp.CompareTo(y.timestamp));
                r = CreateResponse(l);         
            }
            Console.WriteLine("API request received - " + DateTime.Now);
            return r;
        }

        private data CreateResponse(List<Sentiment> data)
        {
            data r = new data();
            foreach (Sentiment s in data)
            {
                r.score.Add(s.score.ToString());
                r.date.Add(FromUnixTime(s.timestamp).ToString("dd-MM-yyyy"));
            }
            return r;
        }
    }
}
