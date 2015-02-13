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

using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Flows.BusinessRule;
using System;


namespace Smartrac.SmartCosmos.Flows.DataContext.Sample
{
    public class SampleBusinessRuleDataContext : EmptyBusinessRuleDataContext
    {
        public SampleBusinessRuleDataContext()
        {
            urn = new Urn("urn:uuid:a911ab4a-2b3a-4a92-80a9-a9d2de2403b6");
            statusStart = StatusBusinessRule.Start;
            statusStop = StatusBusinessRule.Stop;
            ruleData = new BusinessRuleRequest();
            DateTime thisDay = DateTime.UtcNow;
            ruleData.name = "TestAPI_" + thisDay.ToString("d").Replace('.', '_');
            ruleData.description = "Last test was: " + thisDay.ToString().Replace('.', '_');
        }
    }
}
