using System;
using System.Net;

namespace Sonic_06_Toolkit
{
    public class TimedWebClient : WebClient
    {
        public int Timeout { get; set; }

        public TimedWebClient()
        {
            Timeout = 100000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = Timeout;
            return objWebRequest;
        }
    }
}
