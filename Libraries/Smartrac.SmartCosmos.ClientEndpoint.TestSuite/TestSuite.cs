using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.ClientEndpoint.DataImport;
using Smartrac.SmartCosmos.ClientEndpoint.PlatformAvailability;
using Smartrac.SmartCosmos.ClientEndpoint.TagMetadata;
using Smartrac.SmartCosmos.ClientEndpoint.TagVerification;

namespace Smartrac.SmartCosmos.ClientEndpoint.TestSuite
{
    /// <summary>
    /// Test suite for SmartCosmos
    /// </summary>
    public class SmartCosmosTestSuite
    {
        // Settings
        public string UserName;
        public string UserPassword;
        public string ServerURL;
        public bool AllowInvalidServerCertificates;
        public List<string> TagIds;
        public List<string> TagProperties;
        public string VerificationType;
        public string ImportId;

        public IMessageLogger Logger = null;
        private Stopwatch stopwatch = new Stopwatch();

        public SmartCosmosTestSuite()
        {
            // define defaults
            this.ServerURL = "https://www.smart-cosmos.com/service/rest";
            this.AllowInvalidServerCertificates = false;
            this.TagIds = new List<string>();
            this.TagProperties = new List<string>();
            this.VerificationType = "RR";
        }

        /// <summary>
        /// Test cases for verification endpoint
        /// </summary>
        public void TestCase_TagVerificationEndpoint()
        {
            HttpStatusCode result;

            OnBeforeTest("TagVerificationEndpoint", "VerifyTags");
            // create client for endpoint
            TagVerificationEndpoint testerTagVerification = new TagVerificationEndpoint(ServerURL, AllowInvalidServerCertificates, Logger);
            // set login data
            testerTagVerification.SetUserAccount(UserName, UserPassword);
            // create request
            VerifyTagsRequest requestVerifyTags = new VerifyTagsRequest();
            requestVerifyTags.tagIds.AddRange(TagIds);
            requestVerifyTags.verificationType = VerificationType;
            VerifyTagsResponse responseVerifyTags;
            // call endpoint
            result = testerTagVerification.VerifyTags(requestVerifyTags, out responseVerifyTags);
            // log response 
            Logger.AddLog("Result: " + (result == HttpStatusCode.OK));
            Logger.AddLog("Result HttpStatusCode: " + result);
            Logger.AddLog("Result Data: " + responseVerifyTags.ToJSON());
            OnAfterTest();

            OnBeforeTest("TagVerificationEndpoint", "GetVerificationMessage");
            // create request
            VerificationMessageRequest requestVerificationMessage = new VerificationMessageRequest();
            requestVerificationMessage.verificationState = 0;
            requestVerificationMessage.verificationType = VerificationType;
            VerificationMessageResponse responseVerificationMessage;
            // call endpoint
            result = testerTagVerification.GetVerificationMessage(requestVerificationMessage, out responseVerificationMessage);
            // log response 
            Logger.AddLog("Result: " + (result == HttpStatusCode.OK));
            Logger.AddLog("Result HttpStatusCode: " + result);
            Logger.AddLog("Result Data: " + responseVerificationMessage.ToJSON());
            OnAfterTest();
        }

        public void TestCase_DataImportEndpoint()
        {
            OnBeforeTest("DataImportEndpoint", "CheckImportState");
            // create client for endpoint
            DataImportEndpoint testerDataImport = new DataImportEndpoint(ServerURL, AllowInvalidServerCertificates, Logger);
            // set login data
            testerDataImport.SetUserAccount(UserName, UserPassword);

            ImportStateResponse responseImportState;
            // call endpoint
            HttpStatusCode result = testerDataImport.CheckImportState(new ImportStateRequest(ImportId), out responseImportState);
            // log response 
            Logger.AddLog("Result: " + (result == HttpStatusCode.OK));
            Logger.AddLog("Result HttpStatusCode: " + result);
            Logger.AddLog("Result Data: " + responseImportState.ToJSON()); 
            OnAfterTest();
        }

        public void TestCase_TagMetadataEndpoint()
        {
            HttpStatusCode result;

            OnBeforeTest("TagMetadataEndpoint", "GetTagMetadata");
            TagMetadataEndpoint testerTagMetadata = new TagMetadataEndpoint(ServerURL, AllowInvalidServerCertificates, Logger);
            // set login data
            testerTagMetadata.SetUserAccount(UserName, UserPassword);
            // create request
            TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest();
            requestTagMetaData.tagIds.AddRange(TagIds);
            requestTagMetaData.verificationTypes.Add(VerificationType);
            requestTagMetaData.properties.AddRange(TagProperties);

            TagMetaDataResponse responseTagMetaData;
            // call endpoint
            result = testerTagMetadata.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
            // log response 
            Logger.AddLog("Result: " + (result == HttpStatusCode.OK));
            Logger.AddLog("Result HttpStatusCode: " + result);
            Logger.AddLog("Result Data: " + responseTagMetaData.ToJSON());            
            OnAfterTest();

            OnBeforeTest("TagMetadataEndpoint", "GetTagMessage");
            // create request
            TagMessageRequest requestTagMessage = new TagMessageRequest();
            requestTagMessage.tagCode = 0;
            TagMessageResponse responseTagMessage;
            // call endpoint
            result = testerTagMetadata.GetTagMessage(requestTagMessage, out responseTagMessage);
            // log response 
            Logger.AddLog("Result: " + (result == HttpStatusCode.OK));
            Logger.AddLog("Result HttpStatusCode: " + result);
            Logger.AddLog("Result Data: " + responseTagMessage.ToJSON());           
            OnAfterTest();
        }

        public void TestCase_PlatformAvailabilityEndpoint()
        {
            OnBeforeTest("PlatformAvailabilityEndpoint", "Ping");
            PlatformAvailabilityEndpoint testerPlatformAvailability = new PlatformAvailabilityEndpoint(ServerURL, AllowInvalidServerCertificates, Logger);
            // call endpoint & send response to console
            HttpStatusCode result = testerPlatformAvailability.Ping();            
            Logger.AddLog("Result: " + (result == HttpStatusCode.NoContent));
            Logger.AddLog("Result HttpStatusCode: " + result);
            OnAfterTest();
        }

        private void OnBeforeTest(string endpoint, string function)
        {
            Logger.AddLog("-----------------------");
            Logger.AddLog("Endpoint: " + endpoint);
            Logger.AddLog("Function: " + function);
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void OnAfterTest()
        {
            stopwatch.Stop();
            Logger.AddLog("Required time: " + stopwatch.Elapsed);
            Logger.AddLog("");
            Logger.AddLog("");
        }
    }
}
