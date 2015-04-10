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
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Flows.Base;
using System;

namespace Smartrac.SmartCosmos.Flows.BusinessRule
{
    /// <summary>
    /// Defines the name and description of a new rule, defaulted in a STOPPED state.
    /// </summary>
    public class BusinessRuleRequest : BaseRequest
    {
        public string name { get; set; }

        public string description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FlowsServerStartType onServerStart { get; set; }

        public BusinessRuleRequest()
            : base()
        {
            onServerStart = FlowsServerStartType.Manual;
        }

        public override bool IsValid()
        {
            return base.IsValid() &&
                (String.IsNullOrEmpty(description) || description.Length <= 1024) &&
                !String.IsNullOrEmpty(name) &&
                (name.Length <= 1024);
        }
    }
}