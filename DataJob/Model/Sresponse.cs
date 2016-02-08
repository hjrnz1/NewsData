using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataJob
{
        public class Sresponse
    {
            public string status { get; set; }
            public string usage { get; set; }
            public string url { get; set; }
            public string totalTransactions { get; set; }
            public string language { get; set; }
            public SentResult[] results { get; set; }
        }

        public class SentResult
        {
            public Sentiment sentiment { get; set; }
            public string text { get; set; }
        }

        public class Sentiment
        {
            public string mixed { get; set; }
            public float score { get; set; }
            public string type { get; set; }
            [JsonProperty("id")]
            public string docid { get; set; }
            public int timestamp { get; set; }
            [Key]
            public int sid { get; set; }
            public string url { get; set; }
            public string title { get; set; }
    }
}
