using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging.Console;
using Smartrac.SmartCosmos.ClientEndpoint.TestSuite;

namespace Smartrac.SmartCosmos.SampleClient.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartCosmosTestSuite testSuite = new SmartCosmosTestSuite();

            // CONFIGURATION ----

            // General settings
            testSuite.UserName = "";        // please enter you SmartCosmos user name
            testSuite.UserPassword = "";      // please enter you SmartCosmos password

            // Data settings
            testSuite.TagIds.Add("0EEEE100000001");     // existing dummy TID 
            testSuite.TagIds.Add("0EEEE200000002");     // existing dummy TID 
            testSuite.TagProperties.Add("plantId");     // Manufacturer production side ID
            testSuite.TagProperties.Add("batchId");     // Roll number / batch ID
            testSuite.TagProperties.Add("delivDate");   // Delivery date
            testSuite.TagProperties.Add("delivQty");    // Delivery quantity
            testSuite.VerificationType = "RR";          // verification id for RoundRock
            testSuite.ImportId = "20140624_104505-720"; // dummy import Id

            // Output
            testSuite.Logger = new ConsoleLoggerService();

            // Configuration for production server
            testSuite.ServerURL = "https://www.smart-cosmos.com/service/rest";
            testSuite.AllowInvalidServerCertificates = false; // production server has a valid certificate

            // Configuration for Apiary mock server
            //testSuite.ServerURL = "https://smartcosmos.apiary-mock.com";
            //testSuite.AllowInvalidServerCertificates = true;


            // START TESTING ----

            // SAMPLE 1 - Test cases for platform availability endpoint
            testSuite.TestCase_PlatformAvailabilityEndpoint();

            // SAMPLE 2 - Test cases for tag metadata endpoint
            testSuite.TestCase_TagMetadataEndpoint();

            // SAMPLE 3 - Test cases for data import endpoint
            testSuite.TestCase_DataImportEndpoint();

            // SAMPLE 4 - Test cases for tag verification endpoint
            testSuite.TestCase_TagVerificationEndpoint();

            System.Console.ReadLine();
        }

    }

}
