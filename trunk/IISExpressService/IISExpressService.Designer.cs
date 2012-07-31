namespace IISExpressService
{
    partial class IISExpressService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        IISExpress.IISExpress iis;

        
        public IISExpressService(string sonfig, string site, string apppool)
        {
            this.config = config;
            this.site = site;
            this.apppool = apppool;
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

        public void Start()
        {
            //iis = IISExpress.IISExpress.Start(@"C:\Users\Administrator\Documents\IISExpress\config\applicationhost.config", @"WebSite1(1)", @"Clr4IntegratedAppPool");
            iis = IISExpress.IISExpress.Start(config, site, apppool);
        }

        public void Stop()
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "IIS Express Service";
        }

        #endregion
    }
}
