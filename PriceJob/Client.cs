using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static PriceJob.Model.BTCE;

namespace PriceJob.Model
{
    class Client
    {
        WebClient _client;
        String _api;

        public Client()
        {
            _client = new WebClient();
            _api = "https://btc-e.com/api/3/ticker/btc_usd";
        }

        public void getData()
        {
            _client = new WebClient();
            try
            {
                var data = JsonConvert.DeserializeObject<BTCE>(_client.DownloadString(_api)).btc_usd;
                using (var db = new Context())
                {
                    db.priceData.Add(data);
                    db.SaveChanges();
                }
                Console.WriteLine("Last: " + data.last);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }           
        }

        #region Context
        public class Context : DbContext
        {
            public Context() : base("SentDB")
            { }
            public DbSet<Btc_Usd> priceData { get; set; }
        }
        #endregion
    }
}
