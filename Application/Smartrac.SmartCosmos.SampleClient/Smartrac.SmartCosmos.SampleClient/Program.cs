using System;
using Smartrac.Logging.Console;
using Smartrac.SmartCosmos.ClientEndpoint.TestSuite;

namespace Smartrac.SmartCosmos.SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartCosmosTestSuite testSuite = new SmartCosmosTestSuite();

            // CONFIGURATION ----

            // General settings
            testSuite.UserName = "finelinetech";
            testSuite.UserPassword = "smt1kcde7!";

            // Data settings
            testSuite.TagIds.Add("0EEEE100000001");
            testSuite.TagIds.Add("0EEEE200000002");
            testSuite.TagProperties.Add("plantId");
            testSuite.TagProperties.Add("batchId");
            testSuite.TagProperties.Add("delivDate");
            testSuite.TagProperties.Add("delivQty");
            testSuite.VerificationType = "RR";

            // Output
            //IMessageLogger logger;
            testSuite.MessageLogger = new ConsoleLoggerService();

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

            Console.ReadLine();
        }

    }
}
