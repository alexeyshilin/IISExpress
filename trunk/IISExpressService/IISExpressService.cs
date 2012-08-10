using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;

namespace IISExpressService
{
    public partial class IISExpressService : ServiceBase
    {
        private Thread thread;
        private System.Timers.Timer timer;

        public IISExpressService()
        {
            InitializeComponent();
        }

        /*
        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
        */

        IISExpress.IISExpress iis;


        public IISExpressService(string sonfig, string site, string apppool)
        {
            this.config = config;
            this.site = site;
            this.apppool = apppool;
        }

        public IISExpressService(string sonfig, string site, string apppool, string iis)
            : this(sonfig, site, apppool)
        {
            this.iispath = iis;
        }


        /*
        protected override void OnStart(string[] args)
        {
            iis = IISExpress.IISExpress.Start(@"C:\Users\Administrator\Documents\IISExpress\config\applicationhost.config", @"WebSite1(1)", @"Clr4IntegratedAppPool");
        }

        protected override void OnStop()
        {
            if (iis != null)
            {
                iis.Stop();
            }
        }

        protected override void OnShutdown()
        {
            OnStop();
        }
        */

        string config = string.Empty;
        string site = string.Empty;
        string apppool = string.Empty;
        string iispath = string.Empty;

        
        protected override void OnStart(string[] args)
        {
            //iis = IISExpress.IISExpress.Start(@"C:\Users\Administrator\Documents\IISExpress\config\applicationhost.config", @"WebSite1(1)", @"Clr4IntegratedAppPool");

            double num1 = 1000 * 60*60;
            this.timer = new System.Timers.Timer(num1);
            this.timer.AutoReset = true;
            this.timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);

            this.timer.Start();

            this.thread = new Thread(new ThreadStart(this.RunIIS));
            this.thread.Priority = ThreadPriority.Normal;
            this.thread.Start();

        }

        protected override void OnStop()
        {
            if (iis != null)
            {
                iis.Stop();
            }
        }
        

        protected override void OnShutdown()
        {
            Stop();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.DoSomething();
        }

        private void DoSomething()
        {
            string site = iis.Site;
            System.Threading.Thread.Sleep(100);
        }

        private void RunIIS()
        {
            if (iispath == string.Empty || iispath == null)
                iis = IISExpress.IISExpress.Start(config, site, apppool);
            else
                iis = IISExpress.IISExpress.Start(config, site, apppool, iispath);   
        }
    }
}
