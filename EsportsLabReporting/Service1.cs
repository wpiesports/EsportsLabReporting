using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;

namespace EsportsLabReporting
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Send an initial boot signal to server
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:80");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"client\":\"" + username + "," + "\"data\": {\"status\": \"online\", \"status\": \"idle\"}}";
                streamWriter.Write(json);
            }


        }

        protected override void OnStop()
        {
            // Send an offline signal to the server
            // This really shouldn't ever run
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://its33.wpi.edu");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"client\":\"" + username + "," + "\"data\": {\"status\": \"offline\", \"status\": \"idle\"}}";
                streamWriter.Write(json);
            }
        }
    }
}
