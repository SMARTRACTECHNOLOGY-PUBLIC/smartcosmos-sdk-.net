#region License

// SMART COSMOS .Net SDK
// (C) Copyright 2013 - 2015 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.IO;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.Transaction
{
    /// <summary>
    /// Client for Transaction Endpoints
    /// </summary>
    internal class TransactionEndpoint : BaseObjectsEndpoint, ITransactionEndpoint
    {
        public DefaultTransactionRequest CreateDefaultTransaction()
        {
            return new DefaultTransactionRequest();
        }

        /// <summary>
        /// Import data
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>TransactionActionResult</returns>
        public TransactionActionResult Import(ITransactionRequest requestData, out ImportTransactionResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return TransactionActionResult.Failed;
            }

            Uri url = new Uri("transaction", UriKind.Relative).
                AddQuery("handler", requestData.GetHandlerName());

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<object, ImportTransactionResponse>(request, requestData.GetTransactionRequest(), out responseData);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return TransactionActionResult.Successful;
                    default: return TransactionActionResult.Failed;
                }
            }
            return TransactionActionResult.Failed;
        }
    }
}