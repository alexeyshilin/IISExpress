using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace IISExpressService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            // Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\" + "~/templates/templates.xml".Replace("~/", string.Empty)

            string config = String.Empty;
            string site = String.Empty;
            string apppool = String.Empty;
            string iisPath = String.Empty;

            try
            {
                config = System.Configuration.ConfigurationSettings.AppSettings["Config"];
                site = System.Configuration.ConfigurationSettings.AppSettings["Site"];
                apppool = System.Configuration.ConfigurationSettings.AppSettings["ApplicationPool"];
                iisPath = System.Configuration.ConfigurationSettings.AppSettings["IISExpress"];
            }
            catch
            {
                /*
                config = @"c:\Users\merc\Documents\IISExpress\config\applicationhost.config";
                site = @"MercedesBenzTerminal.example";
                apppool = @"Clr4IntegratedAppPool";
                iisPath = @"c:\Program Files (x86)\IIS Express\iisexpress.exe";
                */
            }


            ServiceBase[] ServicesToRun;

            if (iisPath == String.Empty || iisPath == null)
                ServicesToRun = new ServiceBase[] { new IISExpressService(config, site, apppool) };
            else
                ServicesToRun = new ServiceBase[] { new IISExpressService(config, site, apppool, iisPath) };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
