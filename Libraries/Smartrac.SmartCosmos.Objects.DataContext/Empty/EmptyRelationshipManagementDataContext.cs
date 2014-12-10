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
    public class EmptyRelationshipManagementDataContext : BaseRelationshipManagementDataContext
    {
        public string relatedReferenceUrn
        {
            get
            {
                return relatedReferenceUrn_.UUID;
            }
            set
            {
                relatedReferenceUrn_ = new Urn(value);
            }
        }

        Urn relatedReferenceUrn_;

        public string referenceUrn
        {
            get
            {
                return referenceUrn_.UUID;
            }
            set
            {
                referenceUrn_ = new Urn(value);
            }
        }

        Urn referenceUrn_;

        public string relationshipType { get; set; }

        public EntityReferenceType entityReferenceType { get; set; }

        public EntityReferenceType relatedEntityReferenceType { get; set; }

        public ViewType viewType { get; set; }

        
        public override Urn GetRelatedReferenceUrn()
        {
            return relatedReferenceUrn_;
        }

        public override Urn GetReferenceUrn()
        {
            return referenceUrn_;
        }

        public override string GetRelationshipType()
        {
            return relationshipType;
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        /// <returns>EntityReferenceType</returns>
        public override EntityReferenceType GetEntityReferenceType()
        {
            return entityReferenceType;
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        /// <returns>EntityReferenceType</returns>
        public override EntityReferenceType GetRelatedEntityReferenceType()
        {
            return relatedEntityReferenceType;
        }

        public override ViewType GetViewType()
        {
            return viewType;
        }
    }  
}