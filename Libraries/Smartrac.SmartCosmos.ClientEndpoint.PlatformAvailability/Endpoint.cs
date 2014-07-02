using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;

namespace Smartrac.SmartCosmos.ClientEndpoint.PlatformAvailability
{
    /// <summary>
    /// Client for platfom availability endpoint
    /// </summary>
    public class PlatformAvailabilityEndpoint : CommonEndpoint
    {
        public PlatformAvailabilityEndpoint(string aServerURL, bool allowInvalidServerCertificates, IMessageLogger logger)
            : base(aServerURL, allowInvalidServerCertificates, logger)
        {
        }

        public PlatformAvailabilityEndpoint(IMessageLogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Resource for checking the Platform availability
        /// </summary>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode Ping()
        {
            try
            {
                WebRequest request = CreateWebRequest("/test/ping");
                request.Method = WebRequestMethods.Http.Get;
                request.ContentLength = 0;
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    return response.StatusCode;
                }
            }
            catch(Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
