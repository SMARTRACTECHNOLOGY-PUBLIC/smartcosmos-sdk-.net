﻿#region License

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

using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Flows.DataContext;
using Smartrac.SmartCosmos.AccountManager.DataContext;

namespace Smartrac.SmartCosmos.DataContextFactory
{
    public interface IDataContextFactory
    {
        IAccountManagementDataContext CreateAccountManagementDataContext();

        IFileDataContext CreateFileDataContext();

        IGeospatialManagementDataContext CreateGeospatialManagementDataContext();

        IHashTagDataContext CreateHashTagDataContext();

        IMetadataDataContext CreateMetadataDataContext();

        IObjectInteractionDataContext CreateObjectInteractionDataContext();

        IObjectInteractionSessionDataContext CreateObjectInteractionSessionDataContext();

        IObjectManagementDataContext CreateObjectManagementDataContext();

        IRegistrationDataContext CreateRegistrationDataContext();

        IRelationshipManagementDataContext CreateRelationshipManagementDataContext();

        ITagDataContext CreateTagDataContext();

        IUserManagementDataContext CreateUserManagementDataContext();

        IDeviceDataContext CreateDeviceDataContext();

        INotificationDataContext CreateNotificationDataContext();

        //FLows
        IFlowsAccountManagementDataContext CreateFlowsAccountManagementDataContext();

        IBusinessRuleDataContext CreateBusinessRuleDataContext();

        //Account Manager
        IUserDataContext CreateUserDataContext();

        IRoleDataContext CreateRoleDataContext();
    }
}