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

using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Profiles.DataContext.Sample
{
    [TestCaseAttribute(30)]
    public class TestCaseDataImportEndpoint : BaseProfilesTestCase
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