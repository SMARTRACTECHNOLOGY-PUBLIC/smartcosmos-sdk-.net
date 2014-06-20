using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.ClientEndpoint.DataImport;
using Smartrac.SmartCosmos.ClientEndpoint.PlatformAvailability;
using Smartrac.SmartCosmos.ClientEndpoint.TagMetadata;
using Smartrac.SmartCosmos.ClientEndpoint.TagVerification;

namespace Smartrac.SmartCosmos.SampleClient
{
    class Program
    {
        // CONFIG
        /* Mock server
         private static string ServerURL = "https://smartcosmos.apiary-mock.com";
         private static bool allowInvalidServerCertificates = true;
         private static string userName = "Aladdin";
         private static string userPassword = "open sesame";
        */

        /* production server */
        private static string ServerURL = "https://www.smart-cosmos.com/service/rest";
        private static bool allowInvalidServerCertificates = false;
        private static string userName = "..."; 
        private static string userPassword = "..."; 

        static void Main(string[] args)
        {
            // SAMPLE 1 - Test cases for platform availability endpoint
            TestCase_PlatformAvailabilityEndpoint();

            // SAMPLE 2 - Test cases for tag metadata endpoint
            TestCase_TagMetadataEndpoint();

            // SAMPLE 3 - Test cases for data import endpoint
            TestCase_DataImportEndpoint();

            // SAMPLE 4 - Test cases for tag verification endpoint
            TestCase_TagVerificationEndpoint();

            Console.ReadLine();
        }

        private static void TestCase_TagVerificationEndpoint()
        {
            // create client for endpoint
            TagVerificationEndpoint testerTagVerification = new TagVerificationEndpoint(ServerURL, allowInvalidServerCertificates);
            // set login data
            testerTagVerification.SetUserAccount(userName, userPassword);
            // create request
            VerifyTagsRequest requestVerifyTags = new VerifyTagsRequest();

            requestVerifyTags.tagIds.Add("E28011302000380F1D5D010E");
            requestVerifyTags.tagIds.Add("E2801130200034FF1D46010E");

            //requestVerifyTags.tagIds.Add("E12345678912345777");
            //requestVerifyTags.tagIds.Add("E12345678912345888");
            //requestVerifyTags.tagIds.Add("E12345678912345999");
            requestVerifyTags.verificationType = "RR";
            VerifyTagsResponse responseVerifyTags;
            // call endpoint
            Console.WriteLine("VerifyTags result: " + testerTagVerification.VerifyTags(requestVerifyTags, out responseVerifyTags));
            // send response to console
            Console.WriteLine("VerifyTags content as JSON: " + responseVerifyTags.ToJSON());
            Console.WriteLine("");

            // create request
            VerificationMessageRequest requestVerificationMessage = new VerificationMessageRequest();
            requestVerificationMessage.verificationState = 0;
            requestVerificationMessage.verificationType = "RR";
            VerificationMessageResponse responseVerificationMessage;
            // call endpoint
            Console.WriteLine("GetVerificationMessage result: " + testerTagVerification.GetVerificationMessage(requestVerificationMessage, out responseVerificationMessage));
            // send response to console
            Console.WriteLine("GetVerificationMessage content as JSON: " + responseVerificationMessage.ToJSON());
            Console.WriteLine("");
        }

        private static void TestCase_DataImportEndpoint()
        {
            // create client for endpoint
            DataImportEndpoint testerDataImport = new DataImportEndpoint(ServerURL, allowInvalidServerCertificates);
            // set login data
            testerDataImport.SetUserAccount(userName, userPassword);

            ImportStateResponse responseImportState;
            // call endpoint
            Console.WriteLine("CheckImportState result: " + testerDataImport.CheckImportState(new ImportStateRequest("QWxhZGRpbjo4NDcwY2RkM2JmMW"), out responseImportState));
            // send response to console
            Console.WriteLine("CheckImportState content as JSON: " + responseImportState.ToJSON());
            Console.WriteLine("");
        }

        private static void TestCase_TagMetadataEndpoint()
        {
            TagMetadataEndpoint testerTagMetadata = new TagMetadataEndpoint(ServerURL, allowInvalidServerCertificates);
            // set login data
            testerTagMetadata.SetUserAccount(userName, userPassword);
            // create request
            TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest();
            requestTagMetaData.tagIds.Add("E12345678912345777");
            requestTagMetaData.tagIds.Add("E12345678912345888");
            requestTagMetaData.tagIds.Add("E12345678912345999");
            requestTagMetaData.verificationTypes.Add("RR");
            requestTagMetaData.verificationTypes.Add("TestLicense");
            requestTagMetaData.properties.Add("plantId");
            requestTagMetaData.properties.Add("custOrderId");
            requestTagMetaData.properties.Add("batchId");
            requestTagMetaData.properties.Add("delivDate");

            TagMetaDataResponse responseTagMetaData;
            // call endpoint
            Console.WriteLine("GetTagMetadata result: " + testerTagMetadata.GetTagMetadata(requestTagMetaData, out responseTagMetaData));
            // send response to console
            Console.WriteLine("GetTagMetadata content as JSON: " + responseTagMetaData.ToJSON());
            Console.WriteLine("");

            // create request
            TagMessageRequest requestTagMessage = new TagMessageRequest();
            requestTagMessage.tagCode = 0;
            TagMessageResponse responseTagMessage;
            // call endpoint
            Console.WriteLine("GetTagMessage result: " + testerTagMetadata.GetTagMessage(requestTagMessage, out responseTagMessage));
            // send response to console
            Console.WriteLine("GetTagMessage content as JSON: " + responseTagMessage.ToJSON());
            Console.WriteLine("");
        }

        private static void TestCase_PlatformAvailabilityEndpoint()
        {
            PlatformAvailabilityEndpoint testerPlatformAvailability = new PlatformAvailabilityEndpoint(ServerURL, allowInvalidServerCertificates);
            // call endpoint & send response to console
            Console.WriteLine("Ping result: " + (testerPlatformAvailability.Ping() == HttpStatusCode.NoContent));
            Console.WriteLine("");
        }
    }
}
