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
using System.Net.Http;

namespace Smartrac.SmartCosmos.Objects.File
{
    /// <summary>
    /// Client for file endpoint
    /// </summary>
    internal class FileEndpoint : BaseObjectsEndpoint, IFileEndpoint
    {
        /// <summary>
        /// Define the metadata of a file in preparation of an actual file upload
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult GetFileDefinition(FileDefinitionRequest requestData, out FileDefinitionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return FileActionResult.Failed;
                }

                var request = CreateWebRequest("/files", WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<FileDefinitionRequest, FileDefinitionResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);

                if (responseData != null)
                {
                    responseData.HTTPStatusCode = returnHTTPCode;
                    if (returnHTTPCode == HttpStatusCode.Created)
                    {
                        if (responseData != null)
                            responseData.fileUrn = new Urn(responseData.message);
                        return FileActionResult.Successful;
                    }
                }

                return FileActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Upload a file stream as octet stream
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult UploadFileAsOctetStream(Urn fileUrn, Stream data, out FileUploadResponse responseData)
        {
            responseData = null;
            try
            {
                if (null == fileUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("fileUrn is null", LogType.Error);
                    return FileActionResult.Failed;
                }

                if (!fileUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + fileUrn.UUID, LogType.Error);
                    return FileActionResult.Failed;
                }

                Uri url = new Uri("/files", UriKind.Relative).
                    AddSubfolder(fileUrn.UUID).
                    AddSubfolder("octet");

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/octet-stream";

                //request.ContentLength = data.Length;
                using (var writer = request.GetRequestStream())
                {
                    data.CopyTo(writer);
                }

                using (var response = GetResponse(request))
                {
                    if (response != null)
                    {
                        try
                        {
                            responseData = responseData.FromJSON(response.GetResponseStream());
                            if (responseData != null)
                            {
                                responseData.HTTPStatusCode = response.StatusCode;
                                switch (response.StatusCode)
                                {
                                    case HttpStatusCode.OK: return FileActionResult.Successful;
                                    case HttpStatusCode.BadRequest: return FileActionResult.Failed;
                                    case HttpStatusCode.Conflict: return FileActionResult.Conflict;
                                }
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                    else
                        Logger.AddLog("No response from server", LogType.Warning);
                }

                return FileActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Upload a file stream as octet stream
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <param name="file">File path</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult UploadFileAsOctetStream(Urn fileUrn, string file, out FileUploadResponse responseData)
        {
            responseData = null;

            if (!System.IO.File.Exists(file))
            {
                if (null != Logger)
                    Logger.AddLog("Import file does´t exists: " + file, LogType.Error);
                return FileActionResult.Failed;
            }

            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open);
                try
                {
                    return UploadFileAsOctetStream(fileUrn, fileStream, out responseData);
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
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Upload a file stream as multi part form
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public FileActionResult UploadFileAsMultiPartForm(Urn fileUrn, Stream data, string fileName, out FileUploadResponse responseData)
        {
            responseData = null;
            try
            {
                if (null == fileUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("fileUrn is null", LogType.Error);
                    return FileActionResult.Failed;
                }

                if (!fileUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + fileUrn.UUID, LogType.Error);
                    return FileActionResult.Failed;
                }

                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(data), Path.GetFileNameWithoutExtension(fileName), fileName);
                MemoryStream oPostStream = new MemoryStream();
                var copyTask = content.CopyToAsync(oPostStream);
                copyTask.Start();
                copyTask.Wait();

                Uri url = new Uri("/files", UriKind.Relative).
                    AddSubfolder(fileUrn.UUID).
                    AddSubfolder("multipart");

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "multipart/form-data";
                request.ContentLength = oPostStream.Length;

                using (var writer = request.GetRequestStream())
                {
                    oPostStream.CopyTo(writer);
                }

                using (var response = GetResponse(request))
                {
                    if (response != null)
                    {
                        try
                        {
                            responseData = responseData.FromJSON(response.GetResponseStream());
                            if (responseData != null)
                            {
                                responseData.HTTPStatusCode = response.StatusCode;
                                switch (response.StatusCode)
                                {
                                    case HttpStatusCode.OK: return FileActionResult.Successful;
                                    case HttpStatusCode.BadRequest: return FileActionResult.Failed;
                                    case HttpStatusCode.Conflict: return FileActionResult.Conflict;
                                }
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                    else
                        Logger.AddLog("No response from server", LogType.Warning);
                }
                return FileActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Retrieves the file definition
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="view">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">File properties</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult LookupDefinition(Urn fileUrn, out FileDefinitionRetrievalResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if (null == fileUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("fileUrn is null", LogType.Error);
                    return FileActionResult.Failed;
                }

                if (!fileUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + fileUrn.UUID, LogType.Error);
                    return FileActionResult.Failed;
                }

                Uri url = new Uri("/files", UriKind.Relative).
                    AddSubfolder(fileUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                HttpWebResponse webResponse;
                var returnHTTPCode = ExecuteWebRequestJSON<FileDefinitionRetrievalResponse>(request, out responseData, out webResponse);
                if ((responseData != null) && (webResponse != null))
                {
                    responseData.SmartCosmosEvent = webResponse.Headers.Get("SmartCosmos-Event");
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return FileActionResult.Successful;
                        case HttpStatusCode.BadRequest: return FileActionResult.Failed;
                        default: return FileActionResult.Failed;
                    }
                }
                return FileActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Retrieves the actual file content from the cloud
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="responseData">FileContentRetrievalResponse</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult LookupContent(Urn fileUrn, out FileContentRetrievalResponse responseData)
        {
            responseData = null;
            try
            {
                if (null == fileUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("fileUrn is null", LogType.Error);
                    return FileActionResult.Failed;
                }

                if (!fileUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + fileUrn.UUID, LogType.Error);
                    return FileActionResult.Failed;
                }

                Uri url = new Uri("/files", UriKind.Relative).
                    AddSubfolder(fileUrn.UUID).
                    AddSubfolder("contents");

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = WebRequestMethods.Http.Get;

                using (var response = GetResponse(request))
                {
                    if (response != null)
                    {
                        try
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                responseData = new FileContentRetrievalResponse(response.GetResponseStream());
                                responseData.HTTPStatusCode = response.StatusCode;
                                string fileName = response.Headers.Get("Content-Disposition");
                                if (fileName.Contains("filename="))
                                {
                                    foreach (var element in fileName.Split(';'))
                                    {
                                        if (element.Contains("filename="))
                                        {
                                            responseData.filename = fileName.Split('=')[1].Trim('"');
                                        }
                                    }
                                }
                                return FileActionResult.Successful;
                            }
                            else
                            {
                                responseData = responseData.FromJSON(response.GetResponseStream());
                                if (responseData != null)
                                {
                                    responseData.HTTPStatusCode = response.StatusCode;
                                }
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                    else
                        Logger.AddLog("No response from server", LogType.Warning);
                }
                return FileActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Retrieves the complete collection of file definitions associated with the specified entity
        /// </summary>
        /// <param name="entityReferenceType">Valid EntityReferenceType enum value</param>
        /// <param name="fileUrn">Case-sensitive fileUrn of an existing entity of type entityReferenceType</param>
        /// <param name="view">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">File properties</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult LookupDefinitions(EntityReferenceType entityReferenceType, Urn referenceUrn, out FileDefinitionRetrievalListResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if (null == referenceUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("fileUrn is null", LogType.Error);
                    return FileActionResult.Failed;
                }

                if (!referenceUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + referenceUrn.UUID, LogType.Error);
                    return FileActionResult.Failed;
                }

                Uri url = new Uri("/files", UriKind.Relative).
                    AddSubfolder(entityReferenceType.GetDescription()).
                    AddSubfolder(referenceUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<FileDefinitionRetrievalListResponse>(request, out responseData);
                if (responseData != null)
                {
                    // responseData.HTTPStatusCode = response.StatusCode;
                    //responseData.SmartCosmosEvent = response.Headers.Get("SmartCosmos-Event");
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return FileActionResult.Successful;
                        case HttpStatusCode.BadRequest: return FileActionResult.Failed;
                        default: return FileActionResult.Failed;
                    }
                }

                return FileActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }

        /// <summary>
        /// Deletes an existing relationship by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        public FileActionResult Delete(Urn fileUrn)
        {
            try
            {
                if (null == fileUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("fileUrn is null", LogType.Error);
                    return FileActionResult.Failed;
                }

                if (!fileUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + fileUrn.UUID, LogType.Error);
                    return FileActionResult.Failed;
                }

                Uri url = new Uri("/files", UriKind.Relative).
                    AddSubfolder(fileUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = "DELETE";

                using (var response = GetResponse(request))
                {
                    if (response != null)
                    {
                        try
                        {
                            if ((response.StatusCode == HttpStatusCode.NoContent)) // &&
                            //(response.Headers.Get("SmartCosmos-Event") == "FileDeleted"))
                            {
                                return FileActionResult.Successful;
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                    else
                        Logger.AddLog("No response from server", LogType.Warning);

                    return FileActionResult.Failed;
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return FileActionResult.Failed;
            }
        }
    }
}