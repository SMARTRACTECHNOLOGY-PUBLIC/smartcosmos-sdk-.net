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

using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class BaseRelationshipManagementDataContext : BaseDataContext, IRelationshipManagementDataContext
    {
        public virtual Urn GetRelatedReferenceUrn()
        {
            return null;
        }

        public virtual Urn GetReferenceUrn()
        {
            return null;
        }

        public virtual string GetRelationshipType()
        {
            return "";
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        /// <returns>EntityReferenceType</returns>
        public virtual EntityReferenceType GetEntityReferenceType()
        {
            return EntityReferenceType.Object;
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        /// <returns>EntityReferenceType</returns>
        public virtual EntityReferenceType GetRelatedEntityReferenceType()
        {
            return EntityReferenceType.Object;
        }

        public virtual ViewType GetViewType()
        {
            return ViewType.Standard;
        }
    }
}