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

            string config = System.Configuration.ConfigurationSettings.AppSettings["Config"];
            string site = System.Configuration.ConfigurationSettings.AppSettings["Site"];
            string apppool = System.Configuration.ConfigurationSettings.AppSettings["ApplicationPool"];

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new IISExpressService(config, site, apppool) 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
