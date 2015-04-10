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
using Smartrac.SmartCosmos.Flows.Base;
using Smartrac.SmartCosmos.Logging;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Flows.BusinessRule
{
    /// <summary>
    /// Client for BusinessRule Endpoints
    /// </summary>
    internal class BusinessRuleEndpoint : BaseFlowsEndpoint, IBusinessRuleEndpoint
    {
        /// <summary>
        /// Defines the name and description of a new rule, defaulted in a STOPPED state.
        /// </summary>
        /// <param name="requestData">Business rule data</param>
        /// <param name="responseData">result</param>
        /// <returns>BusinessRuleActionResult</returns>
        public BusinessRuleActionResult Create(BusinessRuleRequest requestData, out BusinessRuleResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return BusinessRuleActionResult.Failed;
                }

                var request = CreateWebRequest("/rules", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<BusinessRuleRequest, BusinessRuleResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            //responseData.ruleUrn = new Urn(responseData.message);
                            return BusinessRuleActionResult.Successful;

                        default: return BusinessRuleActionResult.Failed;
                    }
                }

                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }

        /// <summary>
        /// Retrieve the specific business rule associated with the specified urn.
        /// </summary>
        /// <param name="ruleUrn">System-assigned URN assigned at creation</param>
        /// <param name="responseData">business rule data</param>
        /// <returns>BusinessRuleActionResult</returns>
        public BusinessRuleActionResult Lookup(Urn ruleUrn, out BusinessRuleDataResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == ruleUrn) || (!ruleUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return BusinessRuleActionResult.Failed;
                }

                Uri url = new Uri("/rules", UriKind.Relative).
                    AddSubfolder(ruleUrn.UUID);

                HttpWebResponse webResponse;
                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                ExecuteWebRequestJSON<BusinessRuleDataResponse>(request, out responseData, out webResponse);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                {
                    if (webResponse.ContentType == "text/xml")
                    {
                        webResponse.GetResponseStream().CopyTo(responseData.xmlFile);
                    }

                    return BusinessRuleActionResult.Successful;
                }
                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }

        /// <summary>
        /// List a collection of all defined business rules.
        /// </summary>
        /// <param name="responseData">business rules data</param>
        /// <returns>BusinessRuleActionResult</returns>
        public BusinessRuleActionResult Lookup(out BusinessRuleListResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/rules", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<BusinessRuleListResponse>(request, out responseData);
                if ((responseData != null) &&
                    ((responseData.HTTPStatusCode == HttpStatusCode.OK) ||
                     (responseData.HTTPStatusCode == HttpStatusCode.NoContent)))
                {
                    return BusinessRuleActionResult.Successful;
                }
                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }

        /// <summary>
        /// Start or stop a specific business rule associated with the specified urn.
        /// </summary>
        /// <param name="ruleUrn">object reference</param>
        /// <param name="xmlFile">business rule as xml</param>
        /// <param name="responseData">Response data</param>
        /// <returns>BusinessRuleActionResult</returns>
        public BusinessRuleActionResult SetStatus(Urn ruleUrn, StatusBusinessRule statusBusinessRule, out BusinessRuleActionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == ruleUrn) || (!ruleUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is not valid", LogType.Error);
                    return BusinessRuleActionResult.Failed;
                }

                Uri url = new Uri("/rules", UriKind.Relative).
                    AddSubfolder(ruleUrn.UUID).
                    AddSubfolder("status");

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                ExecuteWebRequestJSON<StatusBusinessRuleRequest, BusinessRuleActionResponse>(request, new StatusBusinessRuleRequest { action = statusBusinessRule }, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return BusinessRuleActionResult.Successful;
                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }

        /// <summary>
        /// Defines the name and description of a new rule, defaulted in a STOPPED state.
        /// </summary>
        /// <param name="requestData">Business rule data</param>
        /// <param name="responseData">result</param>
        /// <returns>BusinessRuleActionResult</returns>
        public BusinessRuleActionResult SetState(BusinessRuleStateRequest requestData, out BusinessRuleActionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return BusinessRuleActionResult.Failed;
                }

                Uri url = new Uri("/rules", UriKind.Relative).
                    AddSubfolder(requestData.ruleUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = "DELETE";
                ExecuteWebRequestJSON<BusinessRuleStateRequest, BusinessRuleActionResponse>(request, requestData, out responseData);
                if ((responseData != null) &&
                    (responseData.HTTPStatusCode == HttpStatusCode.NoContent))
                {
                    return BusinessRuleActionResult.Failed;
                }

                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }

        /// <summary>
        /// Update an existing business rule with a new JSON definition.
        /// </summary>
        /// <param name="ruleUrn">object reference</param>
        /// <param name="xmlFile">business rule as xml</param>
        /// <param name="responseData">result</param>
        /// <returns>BusinessRuleActionResult</returns>
        public BusinessRuleActionResult Update(Urn ruleUrn, BusinessRuleRequest ruleData, out BusinessRuleActionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == ruleUrn) || (!ruleUrn.IsValid() || (ruleData == null)))
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is not valid", LogType.Error);
                    return BusinessRuleActionResult.Failed;
                }

                Uri url = new Uri("/rules", UriKind.Relative).
                    AddSubfolder(ruleUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                ExecuteWebRequestJSON<BusinessRuleRequest, BusinessRuleActionResponse>(request, ruleData, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.NoContent))
                    return BusinessRuleActionResult.Successful;
                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }

        /// <summary>
        /// Deletes an existing business rule by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        public BusinessRuleActionResult Delete(Urn ruleUrn)
        {
            try
            {
                if ((null == ruleUrn) || (!ruleUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid ruleUrn: " + ruleUrn.UUID, LogType.Error);
                    return BusinessRuleActionResult.Failed;
                }

                Uri url = new Uri("/rules", UriKind.Relative).
                    AddSubfolder(ruleUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = "DELETE";

                using (var response = request.GetResponseSafe() as System.Net.HttpWebResponse)
                {
                    if (response != null)
                    {
                        try
                        {
                            if ((response.StatusCode == HttpStatusCode.NoContent) // &&
                                //(response.Headers.Get("SmartCosmos-Event") == "FileDeleted")
                               )
                            {
                                return BusinessRuleActionResult.Successful;
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                }
                return BusinessRuleActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BusinessRuleActionResult.Failed;
            }
        }
    }
}