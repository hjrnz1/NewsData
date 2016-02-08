using System;
using DataJob.Service;
using System.Diagnostics;

namespace DataJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        public static string apiKey = System.Configuration.ConfigurationManager.AppSettings["apiKey"];

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            Services s = new Services(apiKey);
            Console.WriteLine("Application Started - " + DateTime.Now);
            s.updateData();
        }
    }
}
