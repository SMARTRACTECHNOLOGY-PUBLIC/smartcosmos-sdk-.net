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
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    /// <summary>
    /// Client for Metadata Endpoints
    /// </summary>
    internal class MetadataEndpoint : BaseObjectsEndpoint, IMetadataEndpoint
    {
        /// <summary>
        /// Inserts a new key-value or updates an existing key value related to the specified entity
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Create(CreateMetadataRequest requestData, out CreateMetadataResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            Uri url = new Uri("/metadata", UriKind.Relative).
                AddSubfolder(requestData.entityReferenceType.GetDescription()).
                AddSubfolder(requestData.referenceUrn.UUID);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<List<MetadataItem>, CreateMetadataResponse>(request, requestData.MetaDataList, out responseData, WebRequestMethods.Http.Put);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return MetadataActionResult.Successful;
                    default: return MetadataActionResult.Failed;
                }
            }

            return MetadataActionResult.Failed;
        }

        /// <summary>
        /// Deletes an existing metadata key by its system-assigned URN key
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data, is null in case of success</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Delete(DeleteMetadataRequest requestData, out DeleteMetadataResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            Uri url = new Uri("/metadata", UriKind.Relative).
                AddSubfolder(requestData.entityReferenceType.GetDescription()).
                AddSubfolder(requestData.entityUrn.UUID).
                AddSubfolder(requestData.key);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            request.Method = "DELETE";

            HttpWebResponse response = request.GetResponseSafe() as System.Net.HttpWebResponse;
            if (response != null)
            {
                try
                {
                    if ((response.StatusCode == HttpStatusCode.NoContent)) //&&
                    //(response.Headers.Get("SmartCosmos-Event") == "MetadataDeleted"))
                    {
                        return MetadataActionResult.Successful;
                    }
                    else
                    {
                        responseData = responseData.FromJSON(response.GetResponseStream());
                        if (responseData is IHttpStatusCode)
                        {
                            (responseData as IHttpStatusCode).HTTPStatusCode = response.StatusCode;
                        }
                    }
                }
                finally
                {
                    response.Close();
                }
            }

            return MetadataActionResult.Failed;
        }

        /// <summary>
        /// Lookup all metadata that matches the specified key pattern
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Lookup(Urn referenceUrn, EntityReferenceType entityReferenceType, string key, out LookupMetadataResponse responseData, ViewType viewType = ViewType.Standard)
        //LookupMetadataRequest requestData, out LookupMetadataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            if ((null == referenceUrn) || !referenceUrn.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            //metadata/{entityReferenceType}/{referenceUrn}{?view,key}
            Uri url = new Uri("/metadata", UriKind.Relative).
                AddSubfolder(entityReferenceType.GetDescription()).
                AddSubfolder(referenceUrn.UUID).
                AddQuery("view", viewType.GetDescription()).
                AddQuery("key", key);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<LookupMetadataResponse>(request, out responseData);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return MetadataActionResult.Successful;
                    default: return MetadataActionResult.Failed;
                }
            }

            return MetadataActionResult.Failed;
        }
    }
}