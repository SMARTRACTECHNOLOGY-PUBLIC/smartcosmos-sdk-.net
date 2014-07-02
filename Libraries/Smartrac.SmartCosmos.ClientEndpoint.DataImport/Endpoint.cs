using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.DataImport
{
    /// <summary>
    /// Client for data import endpoint
    /// </summary>
    public class DataImportEndpoint : CommonEndpoint
    {

        public DataImportEndpoint(string aServerURL, bool allowInvalidServerCertificates, IMessageLogger logger)
            : base(aServerURL, allowInvalidServerCertificates, logger)
        {
        }

        public DataImportEndpoint(IMessageLogger logger)
            : base(logger)
        {
        }


        /// <summary>
        /// Upload a file stream as octet stream
        /// </summary>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode UploadFileAsOctetStream(Stream data, out FileUploadResponse responseData)
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
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FileUploadResponse));
                        responseData = serializer.ReadObject(response.GetResponseStream()) as FileUploadResponse;
                    }

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

        /// <summary>
        /// Upload a file stream as octet stream
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode UploadFileAsOctetStream(string file, out FileUploadResponse responseData)
        {
            if (!File.Exists(file))
            {
                responseData = null;
                return HttpStatusCode.NotFound;
            }

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

        /// <summary>
        /// Upload a file stream as multi part form
        /// </summary>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode UploadFileAsMultiPartForm(Stream data, out FileUploadResponse responseData)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Request the current import state for a given importId
        /// </summary>
        /// <param name="requestData">Import identification (e.g. importId)</param>
        /// <param name="responseData">Import state</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode CheckImportState(ImportStateRequest requestData, out ImportStateResponse responseData)
        {
            responseData = null;
            try
            {
                WebRequest request = CreateWebRequest("/data/files/importstate", WebRequestOption.Authorization | WebRequestOption.AcceptLanguage);
                object responseDataObj = null;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(ImportStateRequest), requestData, typeof(ImportStateResponse), out responseDataObj);

                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ImportStateResponse;
                }
                return returnHTTPCode;
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
