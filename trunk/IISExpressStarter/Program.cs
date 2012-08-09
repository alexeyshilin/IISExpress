using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IISExpressStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Launching IIS Express...");

            string config = System.Configuration.ConfigurationSettings.AppSettings["Config"];
            string site = System.Configuration.ConfigurationSettings.AppSettings["Site"];
            string apppool = System.Configuration.ConfigurationSettings.AppSettings["ApplicationPool"];

            //IISExpress.IISExpress iis1 = IISExpress.IISExpress.Start(@"C:\Users\Administrator\Documents\IISExpress\config\applicationhost.config", @"WebSite1(1)", @"Clr4IntegratedAppPool");
            //IISExpress iis2 = IISExpress.Start(@"C:\Users\Administrator\Documents\IISExpress\config\applicationhost2.config", @"WebSite1(1)", @"Clr4IntegratedAppPool");

            IISExpress.IISExpress iis1 = IISExpress.IISExpress.Start(config, site, apppool);

            Console.Out.WriteLine("Press ENTER to kill");
            
            Console.In.ReadLine();

            iis1.Stop();
            //iis2.Stop();
        }
    }
}
