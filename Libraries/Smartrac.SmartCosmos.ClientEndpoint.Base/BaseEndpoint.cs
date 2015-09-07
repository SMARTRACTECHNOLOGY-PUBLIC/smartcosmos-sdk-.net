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

using Newtonsoft.Json;
using Smartrac.SmartCosmos.Logging;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace Smartrac.SmartCosmos.ClientEndpoint.Base
{
    /// <summary>
    /// Options for the Web request
    /// </summary>
    [Flags]
    public enum WebRequestOption
    {
        /// <summary>
        /// HTTP Header "Authorization" is required
        /// </summary>
        Authorization = 1,

        /// <summary>
        /// HTTP Header "Accept-Language" is required
        /// </summary>
        AcceptLanguage = 2,

        /// <summary>
        /// Hack to avoid: http://example.com/%2F gets translated into http://example.com// before transmitting it.
        /// </summary>
        ForceCanonicalPathAndQuery = 3,

        /// <summary>
        /// Hack to avoid: http://stackoverflow.com/questions/856885/httpwebrequest-to-url-with-dot-at-the-end
        /// </summary>
        FixDotAtEndIssue = 4
    }

    /// <summary>
    /// Base class for all endpoints
    /// </summary>
    public class BaseEndpoint : IBaseEndpoint
    {
        protected string AuthorizationToken = "";
        private string ServerURL_;
        private string ServiceSubUrl_;
        private bool AllowInvalidServerCertificates_;

        public BaseEndpoint()
        {
            this.KeepAlive = true;
            this.AcceptLanguage = "en";
            this.ServerURL = "";
            this.AllowInvalidServerCertificates_ = false;
            this.ServiceSubUrl = "";
        }

        /// <summary>
        /// For a couple of functions the client can use the HTTP Accept-Language to define the lanugage of the respond content.
        /// If the header attribute is missing or the server does not support the client language, English will be used.
        /// </summary>
        public string AcceptLanguage { get; set; }

        /// <summary>
        /// Logger
        /// </summary>
        public IMessageLogger Logger { get; set; }

        /// <summary>
        /// Allow invalid server certificates of SMART COSMOS server?
        /// </summary>
        public bool AllowInvalidServerCertificates
        {
            get
            {
                return AllowInvalidServerCertificates_;
            }
            set
            {
                AllowInvalidServerCertificates_ = value;
                if (AllowInvalidServerCertificates)
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return AllowInvalidServerCertificates_; };
                    if (null != Logger)
                        Logger.AddLog("Invalid certificates are allowed...");
                }
            }
        }

        /// <summary>
        /// Keep server connection alive
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// URL of the SMART COSMOS server
        /// </summary>
        public string ServerURL
        {
            get
            {
                return ServerURL_;
            }
            set
            {
                if (value.EndsWith("/"))
                    ServerURL_ = value.Remove(value.Length - 1);
                else
                    ServerURL_ = value;
            }
        }

        /// <summary>
        /// Base subfolder of service URL of the defined SMART COSMOS service
        /// </summary>
        public string ServiceSubUrl
        {
            get
            {
                return ServiceSubUrl_;
            }
            set
            {
                if (value.StartsWith("/") || value.Length == 0)
                    ServiceSubUrl_ = value;
                else
                    ServiceSubUrl_ = "/" + value;
            }
        }

        /// <summary>
        /// Set the user account which should be used for the authorization
        /// </summary>
        /// <param name="UserName">User name</param>
        /// <param name="userPassword">User password</param>
        public virtual void setUserAccount(string userName, string userPassword)
        {
            if ((userName == "") || (userPassword == ""))
            {
                if (null != Logger)
                    Logger.AddLog("Clear authorization token");

                AuthorizationToken = "";
                return;
            }

            AuthorizationToken =
                 "Basic " +
                 Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + userPassword));
        }

        /// <summary>
        /// Create and setup a web request for a URL endpoint without options
        /// </summary>
        /// <param name="subUrl">Sub url for the endpoint which extends the server url</param>
        /// <returns>Configured web request object</returns>
        protected WebRequest CreateWebRequest(string url, WebRequestOption options = 0)
        {
            return CreateWebRequest(new Uri(url, UriKind.Relative), options);
        }

        /// <summary>
        /// Create and setup a web request for a URL endpoint without options
        /// </summary>
        /// <param name="subUrl">Sub url for the endpoint which extends the server url</param>
        /// <returns>Configured web request object</returns>
        protected WebRequest CreateWebRequest(Uri url)
        {
            return CreateWebRequest(url, 0);
        }

        // Hack
        // --> So http://example.com/%2F gets translated into http://example.com// before transmitting it.
        // http://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash
        private void ForceCanonicalPathAndQuery(Uri uri)
        {
            string paq = uri.PathAndQuery; // need to access PathAndQuery
            FieldInfo flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            ulong flags = (ulong)flagsFieldInfo.GetValue(uri);
            flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
            flagsFieldInfo.SetValue(uri, flags);
        }

        // Hack
        // http://stackoverflow.com/questions/856885/httpwebrequest-to-url-with-dot-at-the-end
        static void FixDotAtEndIssue(Uri uri)
        {
            System.Reflection.MethodInfo getSyntax = typeof(UriParser).GetMethod("GetSyntax", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            System.Reflection.FieldInfo flagsField = typeof(UriParser).GetField("m_Flags", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (getSyntax != null && flagsField != null)
            {
                foreach (string scheme in new[] { "http", "https" })
                {
                    UriParser parser = (UriParser)getSyntax.Invoke(null, new object[] { scheme });
                    if (parser != null)
                    {
                        int flagsValue = (int)flagsField.GetValue(parser);
                        // Clear the CanonicalizeAsFilePath attribute
                        if ((flagsValue & 0x1000000) != 0)
                            flagsField.SetValue(parser, flagsValue & ~0x1000000);
                    }
                }
            }
        }

        /// <summary>
        /// Create and setup a web request for a URL endpoint with options
        /// </summary>
        /// <param name="subUrl">Sub url for the endpoint which extends the server url</param>
        /// <param name="options">Web request options, e.g. to define if authorization is required</param>
        /// <returns>Configured web request object</returns>
        protected WebRequest CreateWebRequest(Uri url, WebRequestOption options)
        {
            string urlString = url.OriginalString;
            if (!urlString.StartsWith("/") && (urlString.Length != 0))
                urlString = "/" + urlString;

            Uri urlFinal = new Uri(ServerURL + ServiceSubUrl + urlString, UriKind.Absolute);
            if (options.HasFlag(WebRequestOption.ForceCanonicalPathAndQuery))
                ForceCanonicalPathAndQuery(urlFinal);

            if (options.HasFlag(WebRequestOption.FixDotAtEndIssue))
                FixDotAtEndIssue(urlFinal);

            var request = System.Net.WebRequest.Create(urlFinal) as System.Net.HttpWebRequest;
            request.KeepAlive = KeepAlive;

            if ((null != Logger) && Logger.CanLog(LogType.Debug))
                Logger.AddLog("AbsoluteUri = " + request.RequestUri.AbsoluteUri, LogType.Debug);

            if (options.HasFlag(WebRequestOption.Authorization) && (AuthorizationToken != ""))
                request.Headers.Add("authorization", AuthorizationToken);
            if (options.HasFlag(WebRequestOption.AcceptLanguage) && (AcceptLanguage != ""))
                request.Headers.Add("Accept-Language", AcceptLanguage);
            return request;
        }

        protected virtual JsonSerializerSettings GetJsonSerializerSettings()
        {
            return null;
        }

        /// <summary>
        /// ExecuteWebRequestJSON
        /// </summary>
        /// <typeparam name="requestType">requestType</typeparam>
        /// <typeparam name="responseType">responseType</typeparam>
        /// <param name="request">web request</param>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="webResponse">HttpWebResponse</param>
        /// <param name="sendMethod">send method</param>
        /// <returns>HttpStatusCode</returns>
        protected HttpStatusCode ExecuteWebRequestJSON<requestType, responseType>(WebRequest request,
            requestType requestData,
            out responseType responseData,
            out HttpWebResponse webResponse,
            string sendMethod = WebRequestMethods.Http.Post)
            where requestType : class
            where responseType : class, new()
        {
            webResponse = null;
            responseData = null;
            try
            {
                request.Method = sendMethod;
                request.ContentType = "application/json";

                // Copy object to a JSON byte array
                if (requestData != null)
                {
                    //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(requestData.ToJSON(requestType));
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(requestData.ToJSON());
                    request.ContentLength = byteArray.Length;
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    if ((null != requestData) && (null != Logger) && Logger.CanLog(LogType.Debug))
                        Logger.AddLog("Request data: " + requestData.ToJSON(true), LogType.Debug);
                }

                return ExecuteWebRequestJSON<responseType>(request, out responseData, out webResponse, sendMethod);
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// ExecuteWebRequestJSON
        /// </summary>
        /// <typeparam name="responseType">responseType</typeparam>
        /// <param name="request">web request</param>
        /// <param name="responseData">response data</param>
        /// <param name="webResponse">HttpWebResponse</param>
        /// <param name="sendMethod">send method</param>
        /// <returns>HttpStatusCode</returns>
        public HttpStatusCode ExecuteWebRequestJSON<responseType>(WebRequest request,
            out responseType responseData,
            string sendMethod = WebRequestMethods.Http.Post)
            where responseType : class, new()
        {
            responseData = null;
            HttpWebResponse webResponse = null;
            return ExecuteWebRequestJSON<responseType>(request, out responseData, out webResponse, sendMethod);
        }

        /// <summary>
        /// ExecuteWebRequestJSON
        /// </summary>
        /// <typeparam name="responseType">responseType</typeparam>
        /// <param name="request">web request</param>
        /// <param name="responseData">response data</param>
        /// <param name="webResponse">HttpWebResponse</param>
        /// <param name="sendMethod">send method</param>
        /// <returns>HttpStatusCode</returns>
        public HttpStatusCode ExecuteWebRequestJSON<responseType>(WebRequest request,
            out responseType responseData,
            out HttpWebResponse webResponse,
            string sendMethod = WebRequestMethods.Http.Post)
            where responseType : class, new()
        {
            webResponse = null;
            responseData = null;
            try
            {
                if ((null != Logger) && Logger.CanLog(LogType.Debug))
                    Logger.AddLog("Request url [" + request.Method + "]: " + request.RequestUri.AbsoluteUri, LogType.Debug);

                // call the server
                if (GetResponse(request, out webResponse))
                {
                    return HttpStatusCode.InternalServerError;
                }

                try
                {
                    if ((webResponse.StatusCode == HttpStatusCode.NoContent) ||
                            (webResponse.StatusCode == HttpStatusCode.InternalServerError))
                    {
                        responseData = new responseType();
                    }
                    else
                    {
                        // convert stream to string
                        if (webResponse.ContentType == "application/json")
                            responseData = responseData.FromJSON(webResponse.GetResponseStream(), GetJsonSerializerSettings());
                        else
                        {
                            responseData = new responseType();
                            if ((webResponse.ContentType == "text/plain") &&
                                (responseData is IResponseMessage))
                            {
                                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                                (responseData as IResponseMessage).message = reader.ReadToEnd();
                            }
                        }
                    }

                    if (responseData is IHttpStatusCode)
                    {
                        (responseData as IHttpStatusCode).HTTPStatusCode = webResponse.StatusCode;
                    }

                    if ((null != responseData) && (null != Logger) && Logger.CanLog(LogType.Debug))
                        Logger.AddLog("Respond data [" + webResponse.StatusCode + "]: " + responseData.ToJSON(true), LogType.Debug);
                }
                finally
                {
                    webResponse.Close();
                }
                return webResponse.StatusCode;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// Execute a web request and get the response data as JSON (GET only)
        /// </summary>
        /// <param name="request">Created WebRequest object</param>
        /// <param name="responseType">Type of the responseData parameter</param>
        /// <param name="responseData">Response data</param>
        /// <returns>HttpStatusCode</returns>
        protected HttpStatusCode ExecuteWebRequestJSON<responseType>(WebRequest request, out responseType responseData)
            where responseType : class, new()
        {
            return ExecuteWebRequestJSON<object, responseType>(request, null, out responseData, WebRequestMethods.Http.Get);
        }

        /// <summary>
        /// GET: Execute a web request and get the response data as JSON
        /// </summary>
        /// <param name="request">Created WebRequest object</param>
        /// <param name="responseType">Type of the responseData parameter</param>
        /// <param name="responseData">Response data</param>
        /// <returns>HttpStatusCode</returns>
        protected HttpStatusCode ExecuteWebRequestJSON<responseType>(WebRequest request, out responseType responseData, out HttpWebResponse webResponse)
            where responseType : class, new()
        {
            responseData = null;
            webResponse = null;
            return ExecuteWebRequestJSON<object, responseType>(request, null, out responseData, out webResponse, WebRequestMethods.Http.Get);
        }

        /// <summary>
        /// Execute a web request and submit the request and response data as JSON
        /// </summary>
        /// <param name="request">Created WebRequest object</param>
        /// <param name="requestType">Type of the requestData parameter</param>
        /// <param name="requestData">Request data</param>
        /// <param name="responseType">Type of the responseData parameter</param>
        /// <param name="responseData">Response data</param>
        /// <returns>HttpStatusCode</returns>
        protected HttpStatusCode ExecuteWebRequestJSON<requestType, responseType>(WebRequest request, requestType requestData, out responseType responseData, string sendMethod = WebRequestMethods.Http.Post)
            where requestType : class
            where responseType : class, new()
        {
            HttpWebResponse webResponse;
            return ExecuteWebRequestJSON<requestType, responseType>(request, requestData, out responseData, out webResponse, sendMethod);
        }

        protected HttpWebResponse GetResponse(WebRequest request)
        {
            HttpWebResponse response = null;
            try
            {
                response =  request.GetResponseSafe() as System.Net.HttpWebResponse;
                if (response == null)
                {
                    Logger.AddLog("No response from server", LogType.Warning);
                }
            }
            catch (Exception e)
            {
                Logger.AddLog(this.GetType().Name + ".GetResponse failed: " + e.Message, LogType.Error);
                return null;
            }

            return response;
        }

        protected bool GetResponse(WebRequest request, out HttpWebResponse response)
        {
            response = GetResponse(request);
            return response != null;
        }
    }
}