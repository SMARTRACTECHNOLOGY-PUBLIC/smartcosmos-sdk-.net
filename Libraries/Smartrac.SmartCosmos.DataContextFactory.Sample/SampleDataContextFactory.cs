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
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.DataContext.Sample;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext.Sample;

namespace Smartrac.SmartCosmos.DataContextFactory.Sample
{
    public class SampleDataContextFactory : BaseDataContextFactory
    {
        public SampleDataContextFactory()
            : base()
        {
        }

        public override ITagDataContext CreateTagDataContext()
        {
            return new SampleTagDataContext();
        }

        public override IFileDataContext CreateFileDataContext()
        {
            return new SampleFileDataContext();
        }

        public override IRegistrationDataContext CreateRegistrationDataContext()
        {
            return new SampleRegistrationDataContext();
        }

        public override IAccountManagementDataContext CreateAccountManagementDataContext()
        {
            return new SampleAccountManagementDataContext();
        }

        public override IUserManagementDataContext CreateUserManagementDataContext()
        {
            return new SampleUserManagementDataContext();
        }

        public override IObjectManagementDataContext CreateObjectManagementDataContext()
        {
            return new SampleObjectManagementDataContext();
        }

        public override IObjectInteractionDataContext CreateObjectInteractionDataContext()
        {
            return new SampleObjectInteractionDataContext();
        }

        public override IRelationshipManagementDataContext CreateRelationshipManagementDataContext()
        {
            return new SampleRelationshipManagementDataContext();
        }

        public override IGeospatialManagementDataContext CreateGeospatialManagementDataContext()
        {
            return new SampleGeospatialManagementDataContext();
        }
    }
}
