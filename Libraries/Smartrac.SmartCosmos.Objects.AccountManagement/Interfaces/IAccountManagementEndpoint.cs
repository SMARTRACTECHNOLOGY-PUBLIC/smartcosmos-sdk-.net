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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.AccountManagement
{
    public enum AccountActionResult
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

    public interface IAccountManagementEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Lookup my account details
        /// </summary>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        AccountActionResult GetAccountDetails(ViewType? viewType, out AccountDetailsResponse responseData);

        /// <summary>
        /// Change the authenticated user's password, presuming they know their existing password to change to a new password.
        /// </summary>
        /// <param name="requestData">old and new password</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        AccountActionResult ChangeYourPassword(ChangeYourPasswordRequest requestData, out ChangeYourPasswordResponse responseData);

        /// <summary>
        /// Trigger a password reset workflow via email for the specified Account associated with the indicated email address.
        /// </summary>
        /// <param name="requestData">contains your emailAddress</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        AccountActionResult ResetLostPassword(ResetLostPasswordRequest requestData, out ResetLostPasswordResponse responseData);
    }
}
