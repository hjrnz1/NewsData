using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataJob;
using System.Data.Entity;
using System.Timers;
using System.Diagnostics;

namespace DataJob.Service
{
    class Services
    {
        WebClient client;
        private string sService;
        private string nService;
        private Sresponse s;
        private Nresponse n;
        private string sQuery;
        private string nQuery;
        private string apiKey;
        private int start;
        private string startq;

        public Services(string apikey)
        {
            apiKey = apikey;
            client = new WebClient();
            nService = "https://gateway-a.watsonplatform.net/calls/data/GetNews?";
            nQuery = "outputMode=json&start=now-7d&end=now&count=1000&q.enriched.url.title=O[Bitcoin^Blockchain]&return=enriched.url.url&apikey=";
            sService = "http://gateway-a.watsonplatform.net/calls/url/URLGetTargetedSentiment?";
            sQuery = "outputMode=json&sourceText=cleaned_or_raw&targets=Bitcoin&url=";
            startq = "start=";
        }

        private Nresponse getArticles()
        {
            //Find latest article retrieved
            using (Context db = new Context())
            {
                Sentiment f = db.Sentiment.OrderByDescending(x => x.timestamp).First();
                if (f != null)
                {
                    start = f.timestamp + 1;
                }
            }           
            return n = JsonConvert.DeserializeObject<Nresponse>(client.DownloadString(nService + startq + start + "&" + nQuery + apiKey));
        }

        private Sresponse getSentiment(DataJob.Doc d)
        {
            this.client = new WebClient();
            this.s = new Sresponse();
            s = JsonConvert.DeserializeObject<Sresponse>(client.DownloadString(sService + sQuery + d.source.enriched.url.url + "&apikey=" + apiKey));
            
            if (s.status == "OK" && s.results != null)
            {
                Sentiment r = s.results[0].sentiment;

                using (Context db = new Context())
                {
                    r.docid = d.id;
                    r.timestamp = d.timestamp;
                    r.url = d.source.enriched.url.url;
                    r.title = d.source.enriched.url.title;

                    db.Sentiment.Add(r);
                    db.SaveChanges();
                    Console.WriteLine("Added new article: " + r.url);
                }
            }

            return s;
        }

        public void updateData()
        {
            try
            {
                Nresponse n = getArticles();
                if (n.status == "OK")
                {
                    if (n.result.docs != null)
                    {
                        foreach (Doc d in n.result.docs)
                        {
                            //If we don't already have the article, get sentiment info and add to DB
                            using (Context db = new Context())
                            {
                                if (db.Sentiment.Where(x => x.docid == d.id).Count() == 0)
                                {
                                   getSentiment(d);
                                }
                            }
                        }
                    }
                    else
                        Console.WriteLine("No new articles - " + DateTime.Now);
                }
                else
                {
                    throw new Exception(client.ResponseHeaders["X-AlchemyAPI-Error-Msg"]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error calling web service: " + e.Message);
            }
        }
    }
    #region Context
    public class Context : DbContext
    {
        public Context() : base("SentDB")
        { }
        public DbSet<Sentiment> Sentiment { get; set; }
    }
    #endregion
}
