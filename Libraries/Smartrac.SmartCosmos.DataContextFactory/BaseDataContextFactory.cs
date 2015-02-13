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

using Smartrac.SmartCosmos.AccountManager.DataContext;
using Smartrac.SmartCosmos.Flows.DataContext;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext;

namespace Smartrac.SmartCosmos.DataContextFactory
{
    public class BaseDataContextFactory : IDataContextFactory
    {
        public BaseDataContextFactory()
            : base()
        {
        }

        public virtual ITagDataContext CreateTagDataContext()
        {
            return new BaseTagDataContext();
        }

        public virtual IFileDataContext CreateFileDataContext()
        {
            return new BaseFileDataContext();
        }

        public virtual IRegistrationDataContext CreateRegistrationDataContext()
        {
            return new BaseRegistrationDataContext();
        }

        public virtual IAccountManagementDataContext CreateAccountManagementDataContext()
        {
            return new BaseAccountManagementDataContext();
        }

        public virtual IUserManagementDataContext CreateUserManagementDataContext()
        {
            return new BaseUserManagementDataContext();
        }

        public virtual IObjectManagementDataContext CreateObjectManagementDataContext()
        {
            return new BaseObjectManagementDataContext();
        }

        public virtual IObjectInteractionDataContext CreateObjectInteractionDataContext()
        {
            return new BaseObjectInteractionDataContext();
        }

        public virtual IRelationshipManagementDataContext CreateRelationshipManagementDataContext()
        {
            return new BaseRelationshipManagementDataContext();
        }

        public virtual IGeospatialManagementDataContext CreateGeospatialManagementDataContext()
        {
            return new BaseGeospatialManagementDataContext();
        }

        public virtual IHashTagDataContext CreateHashTagDataContext()
        {
            return new BaseHashTagDataContext();
        }

        public virtual IObjectInteractionSessionDataContext CreateObjectInteractionSessionDataContext()
        {
            return new BaseObjectInteractionSessionDataContext();
        }

        public virtual IMetadataDataContext CreateMetadataDataContext()
        {
            return new BaseMetadataDataContext();
        }

        public virtual IDeviceDataContext CreateDeviceDataContext()
        {
            return new BaseDeviceDataContext();
        }

        public virtual INotificationDataContext CreateNotificationDataContext()
        {
            return new BaseNotificationDataContext();
        }

        //Flows

        public virtual IFlowsAccountManagementDataContext CreateFlowsAccountManagementDataContext()
        {
            return new BaseFlowsAccountManagementDataContext();
        }

        public virtual IBusinessRuleDataContext CreateBusinessRuleDataContext()
        {
            return new BaseBusinessRuleDataContext();
        }

        //Account Manager

        public virtual IUserDataContext CreateUserDataContext()
        {
            return new BaseUserDataContext();
        }

        public virtual IRoleDataContext CreateRoleDataContext()
        {
            return new BaseRoleDataContext();
        }
        
    }
}