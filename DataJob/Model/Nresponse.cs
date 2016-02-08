using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataJob
{
    public class Nresponse
    {
        public string status { get; set; }
        public NewsResult result { get; set; }
    }

    public class NewsResult
    {
        public Doc[] docs { get; set; }
        public string next { get; set; }
        public string status { get; set; }
    }

    public class Doc
    {
        public string id { get; set; }
        public Source source { get; set; }
        public int timestamp { get; set; }
    }

    public class Source
    {
        public Enriched enriched { get; set; }
    }

    public class Enriched
    {
        public Article url { get; set; }
    }

    public class Article
    {
        public string title { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }

}