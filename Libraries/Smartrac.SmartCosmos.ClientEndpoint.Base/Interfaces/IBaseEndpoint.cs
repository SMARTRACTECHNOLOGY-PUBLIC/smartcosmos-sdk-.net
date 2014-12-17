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

using Smartrac.SmartCosmos.Logging;

namespace Smartrac.SmartCosmos.ClientEndpoint.Base
{
    /// <summary>
    /// base interface of a endpoint
    /// </summary>
    public interface IBaseEndpoint
    {
        /// <summary>
        /// Server Url
        /// </summary>
        string ServerURL { get; set; }

        /// <summary>
        /// Keep connection alive
        /// </summary>
        bool KeepAlive { get; set; }

        /// <summary>
        /// Allow invalid server certificates
        /// </summary>
        bool AllowInvalidServerCertificates { get; set; }

        /// <summary>
        /// Client accept lanugage
        /// </summary>
        string AcceptLanguage { get; set; }

        /// <summary>
        /// logger interface
        /// </summary>
        IMessageLogger Logger { get; set; }

        /// <summary>
        /// Optional: credentials
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="userPassword">user password</param>
        void setUserAccount(string userName, string userPassword);
    }
}