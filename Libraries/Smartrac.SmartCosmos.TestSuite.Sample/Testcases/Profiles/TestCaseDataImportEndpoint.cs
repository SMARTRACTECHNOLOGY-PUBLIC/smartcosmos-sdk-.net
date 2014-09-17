using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    [TestSuiteAttribute]
    public class TestCaseDataImportEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            ITagDataContext dataContext = DataContextFactory.CreateTagDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip test cases for DataImportEndpoint, because of missing data context", LogType.Info);
                return true;
            }

            IDataImportEndpoint tester = EndpointFactory.CreateDataImportEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("DataImportEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            DataActionResult actionResult;

            OnBeforeTest("Profiles", "DataImportEndpoint", "CheckImportState");
            // create client for endpoint
            ImportStateResponse responseImportState;
            // call endpoint
            actionResult = tester.CheckImportState(new ImportStateRequest(dataContext.GetImportId()), out responseImportState);
            result = result && (actionResult == DataActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseImportState.ToJSON());
            OnAfterTest();

            return result; 
        }
    }
}
