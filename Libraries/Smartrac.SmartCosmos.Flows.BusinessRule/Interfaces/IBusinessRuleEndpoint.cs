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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Flows.Base;
using System.IO;

namespace Smartrac.SmartCosmos.Flows.BusinessRule
{
    public enum BusinessRuleActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed
    }

    public interface IBusinessRuleEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Defines the name and description of a new rule, defaulted in a STOPPED state.
        /// </summary>
        /// <param name="requestData">Business rule data</param>
        /// <param name="responseData">result</param>
        /// <returns>BusinessRuleActionResult</returns>
        BusinessRuleActionResult Create(BusinessRuleRequest requestData, out BusinessRuleResponse responseData);
        
        /// <summary>
        /// Retrieve the specific business rule associated with the specified urn.
        /// </summary>
        /// <param name="ruleUrn">System-assigned URN assigned at creation</param>
        /// <param name="responseData">business rule data</param>
        /// <returns>BusinessRuleActionResult</returns>
        BusinessRuleActionResult Lookup(Urn ruleUrn, out BusinessRuleDataResponse responseData);

        /// <summary>
        /// List a collection of all defined business rules.
        /// </summary>
        /// <param name="responseData">business rules data</param>
        /// <returns>BusinessRuleActionResult</returns>
        BusinessRuleActionResult Lookup(out BusinessRuleListResponse responseData);

        /// <summary>
        /// Start or stop a specific business rule associated with the specified urn.
        /// </summary>
        /// <param name="ruleUrn">object reference</param>
        /// <param name="responseData">Response data</param>
        /// <returns>BusinessRuleActionResult</returns>
        BusinessRuleActionResult SetStatus(Urn ruleUrn, StatusBusinessRule requestData, out BusinessRuleActionResponse responseData);

        
        /// <summary>
        /// Defines the name and description of a new rule, defaulted in a STOPPED state.
        /// </summary>
        /// <param name="requestData">Business rule data</param>
        /// <param name="responseData">result</param>
        /// <returns>BusinessRuleActionResult</returns>
        BusinessRuleActionResult SetState(BusinessRuleStateRequest requestData, out BusinessRuleActionResponse responseData);

        /// <summary>
        /// Update an existing business rule with a new XML definition.
        /// </summary>
        /// <param name="ruleUrn">object reference</param>
        /// <param name="xmlFile">business rule as xml</param>
        /// <param name="responseData">result</param>
        /// <returns>BusinessRuleActionResult</returns>
        BusinessRuleActionResult Update(Urn ruleUrn, BusinessRuleRequest ruleData, out BusinessRuleActionResponse responseData);

        /// <summary>
        /// Deletes an existing business rule by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        BusinessRuleActionResult Delete(Urn ruleUrn);
    }
}