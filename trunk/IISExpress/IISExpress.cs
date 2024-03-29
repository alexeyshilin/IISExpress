﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IISExpress
{
    public class IISExpress
    {
        internal class NativeMethods
        {
            // Methods
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr GetTopWindow(IntPtr hWnd);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        }

        public static void SendStopMessageToProcess(int PID)
        {
            try
            {
                for (IntPtr ptr = NativeMethods.GetTopWindow(IntPtr.Zero); ptr != IntPtr.Zero; ptr = NativeMethods.GetWindow(ptr, 2))
                {
                    uint num;
                    NativeMethods.GetWindowThreadProcessId(ptr, out num);
                    if (PID == num)
                    {
                        HandleRef hWnd = new HandleRef(null, ptr);
                        NativeMethods.PostMessage(hWnd, 0x12, IntPtr.Zero, IntPtr.Zero);
                        return;
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }

        //const string IIS_EXPRESS = @"C:\Program Files\IIS Express\iisexpress.exe";
        const string IIS_EXPRESS = @"c:\Program Files (x86)\IIS Express\iisexpress.exe";
        const string CONFIG = "config";
        const string SITE = "site";
        const string APP_POOL = "apppool";

        Process process;

        
        IISExpress(string config, string site, string apppool)
        {
            Config = config;
            Site = site;
            AppPool = apppool;

            StringBuilder arguments = new StringBuilder();
            if (!string.IsNullOrEmpty(Config))
                arguments.AppendFormat("/{0}:{1} ", CONFIG, Config);

            if (!string.IsNullOrEmpty(Site))
                arguments.AppendFormat("/{0}:{1} ", SITE, Site);

            if (!string.IsNullOrEmpty(AppPool))
                arguments.AppendFormat("/{0}:{1} ", APP_POOL, AppPool);

            process = Process.Start(new ProcessStartInfo()
            {
                FileName = IIS_EXPRESS,
                Arguments = arguments.ToString(),
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
        }
        

        /*
        IISExpress(string config, string site, string apppool, string iis) : this(config, site, apppool)
        {
            IISExpressPath = iis;
        }
        */

        
        IISExpress(string config, string site, string apppool, string iis)
        {
            IISExpressPath = iis;

            Config = config;
            Site = site;
            AppPool = apppool;

            StringBuilder arguments = new StringBuilder();
            if (!string.IsNullOrEmpty(Config))
                arguments.AppendFormat("/{0}:{1} ", CONFIG, Config);

            if (!string.IsNullOrEmpty(Site))
                arguments.AppendFormat("/{0}:{1} ", SITE, Site);

            if (!string.IsNullOrEmpty(AppPool))
                arguments.AppendFormat("/{0}:{1} ", APP_POOL, AppPool);

            process = Process.Start(new ProcessStartInfo()
            {
                FileName = IISExpressPath,
                Arguments = arguments.ToString(),
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
        }
        

        /*
        IISExpress(string config, string site, string apppool)
        {
            string Config = @"c:\Users\merc\Documents\IISExpress\config\applicationhost.config";
            string Site = @"MercedesBenzTerminal.example";
            string AppPool = @"Clr4IntegratedAppPool";
            string iis= @"c:\Program Files (x86)\IIS Express\iisexpress.exe";

            StringBuilder arguments = new StringBuilder();
            if (!string.IsNullOrEmpty(Config))
                arguments.AppendFormat("/{0}:{1} ", CONFIG, Config);

            if (!string.IsNullOrEmpty(Site))
                arguments.AppendFormat("/{0}:{1} ", SITE, Site);

            if (!string.IsNullOrEmpty(AppPool))
                arguments.AppendFormat("/{0}:{1} ", APP_POOL, AppPool);

            process = Process.Start(new ProcessStartInfo()
            {
                FileName = iis,
                Arguments = arguments.ToString(),
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
        }

        IISExpress(string config, string site, string apppool, string iis) : this(config, site, apppool)
        {
        }
        */

        public string Config { get; protected set; }
        public string Site { get; protected set; }
        public string AppPool { get; protected set; }
        public string IISExpressPath { get; protected set; }

        public static IISExpress Start(string config, string site, string apppool)
        {
            return new IISExpress(config, site, apppool);
        }

        public static IISExpress Start(string config, string site, string apppool, string iis)
        {
            return new IISExpress(config, site, apppool, iis);
        }

        public void Stop()
        {
            SendStopMessageToProcess(process.Id);
            process.Close();
        }
    }

}
