using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.Base
{
    /// <summary>
    /// Options for the Web request
    /// </summary>
    public enum WebRequestOption
    {
        Authorization, // HTTP Header "Authorization" is required
        AcceptLanguage // HTTP Header "Accept-Language" is required
    }


    /// <summary>
    /// Base class for all endpoints
    /// </summary>
    public class CommonEndpoint
    {
        protected string AuthorizationToken = "";
        public bool KeepAlive { get; set; }
        public string AcceptLanguage { get; set; }

        protected IMessageLogger Logger { get; set; }

        public CommonEndpoint(string aServerURL, bool allowInvalidServerCertificates, IMessageLogger logger)
        {
            this.ServerURL = aServerURL;
            this.KeepAlive = true;
            this.AcceptLanguage = "en";
            this.Logger = logger;

            if (allowInvalidServerCertificates)
            {
                if (null != Logger)
                    Logger.AddLog("Invalid certificates are allowed...");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }
        }

        public CommonEndpoint(IMessageLogger logger)
            : this("https://www.smart-cosmos.com/service/rest", false, logger)
        {
        }

        private string ServerURL_; 
        protected string ServerURL 
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
        /// Set the user account which should be used for the authorization
        /// </summary>
        /// <param name="UserName">User name</param>
        /// <param name="userPassword">User password</param>
        public void SetUserAccount(string userName, string userPassword)
        {
            if (null != Logger)
                Logger.AddLog("Login with user " + userName);

            // UserName and hased password are combined into a string "UserName:hashedpassword"
            // For example, if the user agent uses 'Aladdin' as the UserName and 'open sesame' as the password then the header is formed as follows:.
            // SHA512 hash of the password: 8470cdd3bf1ef85d5f092bce5ae5af97ce50820481bf43b2413807fec37e2785b533a65d4c7d71695b141d81ebcd4b6c4def4284e6067f0b9ddc318b1b230205
            // Authorization: Basic QWxhZGRpbjo4NDcwY2RkM2JmMWVmODVkNWYwOTJiY2U1YWU1YWY5N2NlNTA4MjA0ODFiZjQzYjI0MTM4MDdmZWMzN2UyNzg1YjUzM2E2NWQ0YzdkNzE2OTViMTQxZDgxZWJjZDRiNmM0ZGVmNDI4NGU2MDY3ZjBiOWRkYzMxOGIxYjIzMDIwNQ==
            AuthorizationToken =
                "Basic " +
                Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + BitConverter.ToString(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(userPassword))).Replace("-", "").ToLower()));
        }


        /// <summary>
        /// Create and setup a web request for a URL endpoint without options
        /// </summary>
        /// <param name="subUrl">Sub url for the endpoint which extends the server url</param>
        /// <returns>Configured web request object</returns>
        protected WebRequest CreateWebRequest(string subUrl)
        {
            return CreateWebRequest(subUrl, 0);
        }


        /// <summary>
        /// Create and setup a web request for a URL endpoint with options
        /// </summary>
        /// <param name="subUrl">Sub url for the endpoint which extends the server url</param>
        /// <param name="options">Web request options, e.g. to define if authorization is required</param>
        /// <returns>Configured web request object</returns>
        protected WebRequest CreateWebRequest(string subUrl, WebRequestOption options)
        {
            var request = System.Net.WebRequest.Create(ServerURL + subUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = KeepAlive;
            if (options.HasFlag(WebRequestOption.Authorization) && (AuthorizationToken != ""))
                request.Headers.Add("authorization", AuthorizationToken);
            if (options.HasFlag(WebRequestOption.AcceptLanguage) && (AcceptLanguage != ""))
                request.Headers.Add("Accept-Language", AcceptLanguage);
            return request;
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
        protected HttpStatusCode ExecuteWebRequestJSON(WebRequest request, Type requestType, object requestData, Type responseType, out object responseData)
        {
            responseData = null;
            try
            {
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/json";

                // Copy object to a JSON byte array
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(requestData.ToJSON(requestType));
                request.ContentLength = byteArray.Length;
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                // call the server
                HttpWebResponse response = null;
                try
                {
                    response = request.GetResponse() as System.Net.HttpWebResponse;

                }
                catch (WebException e)
                {
                    response = e.Response as System.Net.HttpWebResponse;
                }

                if (response != null)
                {
                    if ((response.StatusCode == HttpStatusCode.OK) ||
                         (response.StatusCode == HttpStatusCode.BadRequest)
                       )
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(responseType);
                        responseData = serializer.ReadObject(response.GetResponseStream());
                    }
                    return response.StatusCode;
                }

                if (null != Logger)
                    Logger.AddLog("No respond", LogType.Warning);
                return HttpStatusCode.InternalServerError;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
