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
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext;

namespace Smartrac.SmartCosmos.TestSuite
{
    /// <summary>
    /// Base class for test suites
    /// </summary>
    public class BaseTestSuite : ITestSuite
    {
        public IMessageLogger Logger { get; set; }
        public IEndpointFactory Factory { get; set; }

        public ITagDataContext TagDataContext { get; set; }
        public IFileDataContext FileDataContext { get; set; }
        public IRegistrationDataContext RegistrationDataContext { get; set; }
        public IAccountManagementDataContext AccountManagementDataContext { get; set; }
        public IUserManagementDataContext UserManagementDataContext { get; set; }
        public IObjectManagementDataContext ObjectManagementDataContext { get; set; }
        public IObjectInteractionDataContext ObjectInteractionDataContext { get; set; }
        public IRelationshipManagementDataContext RelationshipManagementDataContext { get; set; }

        /// <summary>
        /// Start testing
        /// </summary>
        /// <returns>test result</returns>
        public virtual bool Run()
        {
            return true;
        }
    }
}
