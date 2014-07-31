#region License
// SMART COSMOS Profiles SDK
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
#endregion

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
    class PlatformAvailabilityEndpoint : BaseEndpoint, IPlatformAvailabilityEndpoint
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
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    switch(response.StatusCode)
                    {
                        case HttpStatusCode.NoContent: return PlatformAvailabilityActionResult.Successful;
                        case HttpStatusCode.ServiceUnavailable: return PlatformAvailabilityActionResult.Unavailable;
                        default: return PlatformAvailabilityActionResult.Failed;
                    }
                }
            }
            catch(Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return PlatformAvailabilityActionResult.Failed;
            }
        }
    }
}
