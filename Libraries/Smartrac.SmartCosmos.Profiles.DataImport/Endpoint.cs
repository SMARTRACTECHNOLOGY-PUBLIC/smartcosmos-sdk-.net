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

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Profiles.Base;

namespace Smartrac.SmartCosmos.Profiles.DataImport
{
    /// <summary>
    /// Client for data import endpoint
    /// </summary>
    internal class DataImportEndpoint : BaseProfileEndpoint, IDataImportEndpoint
    {
        /// <summary>
        /// Upload a file stream as octet stream
        /// </summary>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public DataActionResult UploadFileAsOctetStream(Stream data, out FileUploadResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/data/files/octet", WebRequestOption.Authorization | WebRequestOption.AcceptLanguage);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/octet-stream";

                request.ContentLength = data.Length;
                using (var writer = request.GetRequestStream())
                {
                    data.CopyTo(writer);
                }

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    if (response == null)
                    {
                        return DataActionResult.Failed;
                    }

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FileUploadResponse));
                    responseData = serializer.ReadObject(response.GetResponseStream()) as FileUploadResponse;

                    if (response.StatusCode == HttpStatusCode.OK)
                        return DataActionResult.Successful;
                    else
                        return DataActionResult.Failed;
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DataActionResult.Failed;
            }
        }

        /// <summary>
        /// Upload a file stream as octet stream
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public DataActionResult UploadFileAsOctetStream(string file, out FileUploadResponse responseData)
        {
            responseData = null;

            if (!File.Exists(file))
            {
                if (null != Logger)
                    Logger.AddLog("Import file does´t exists: " + file, LogType.Error);
                return DataActionResult.Failed;
            }

            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open);
                try
                {
                    return UploadFileAsOctetStream(fileStream, out responseData);
                }
                finally
                {
                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DataActionResult.Failed;
            }
        }

        /// <summary>
        /// Upload a file stream as multi part form
        /// </summary>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public DataActionResult UploadFileAsMultiPartForm(Stream data, out FileUploadResponse responseData)
        {
            if (null != Logger)
                Logger.AddLog("UploadFileAsMultiPartForm is not yet implemented", LogType.Error);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Request the current import state for a given importId
        /// </summary>
        /// <param name="requestData">Import identification (e.g. importId)</param>
        /// <param name="responseData">Import state</param>
        /// <returns>HTTP status code</returns>
        public DataActionResult CheckImportState(ImportStateRequest requestData, out ImportStateResponse responseData)
        {
            responseData = null;
            try
            {
                WebRequest request = CreateWebRequest("/data/files/importstate", WebRequestOption.Authorization | WebRequestOption.AcceptLanguage);
                var returnHTTPCode = ExecuteWebRequestJSON<ImportStateRequest, ImportStateResponse>(request, requestData, out responseData);

                if (returnHTTPCode == HttpStatusCode.OK)
                    return DataActionResult.Successful;
                else
                    return DataActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DataActionResult.Failed;
            }
        }
    }
}