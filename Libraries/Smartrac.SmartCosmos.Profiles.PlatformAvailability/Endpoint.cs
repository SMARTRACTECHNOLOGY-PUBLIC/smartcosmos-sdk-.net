#region License

// SMART COSMOS .Net SDK
// (C) Copyright 2014 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion License

using Smartrac.Logging;

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Profiles.Base;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Profiles.PlatformAvailability
{
    /// <summary>
    /// Client for platfom availability endpoint
    /// </summary>
    internal class PlatformAvailabilityEndpoint : BaseProfileEndpoint, IPlatformAvailabilityEndpoint
    {
        /// <summary>
        /// Resource for checking the Platform availability
        /// </summary>
        /// <returns>HTTP status code</returns>
        public PlatformAvailabilityActionResult Ping()
        {
            try
            {
                WebRequest request = CreateWebRequest("/test/ping");
                request.Method = WebRequestMethods.Http.Get;
                request.ContentLength = 0;
                using (var response = request.GetResponseSafe() as System.Net.HttpWebResponse)
                {
                    response.Close();
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NoContent: return PlatformAvailabilityActionResult.Successful;
                        case HttpStatusCode.ServiceUnavailable: return PlatformAvailabilityActionResult.Unavailable;
                        default: return PlatformAvailabilityActionResult.Failed;
                    }
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return PlatformAvailabilityActionResult.Failed;
            }
        }
    }
}