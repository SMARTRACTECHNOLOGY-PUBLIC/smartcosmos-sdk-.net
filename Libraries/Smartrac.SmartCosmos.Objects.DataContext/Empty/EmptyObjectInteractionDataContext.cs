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

using System;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class EmptyObjectInteractionDataContext : BaseObjectInteractionDataContext
    {
        public string objectUrn 
        {
            get
            {
                return objectUrn_.UUID;
            }
            set
            {
                objectUrn_ = new Urn(value);
            } 
        }

        Urn objectUrn_;

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

        public string interactionType { get; set; }

        public long recordedTimestamp { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public EntityReferenceType entityReferenceType { get; set; }

        public ViewType viewType { get; set; }

        public override Urn GetObjectUrn()
        {
            return objectUrn_;
        }

        public override Urn GetReferenceUrn()
        {
            return referenceUrn_;
        }

        /// <summary>
        /// The type field is available to create an ontology or high level categories that can be used to group related interactions. The platform makes no inferences about this case-sensitive field.
        /// </summary>
        /// <returns>interaction type</returns>
        public override string GetInteractionType()
        {
            return interactionType;
        }

        public override long GetRecordedTimestamp()
        {
            return recordedTimestamp;
        }

        public override string GetDescription()
        {
            return description;
        }

        public override bool GetActiveFlag()
        {
            return activeFlag;
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        /// <returns>EntityReferenceType</returns>
        public override EntityReferenceType GetEntityReferenceType()
        {
            return entityReferenceType;
        }

        public override ViewType GetViewType()
        {
            return viewType;
        }

        public override void Prepare()
        {
            interactionType = interactionType.ResolveVariables();
            description = description.ResolveVariables();
        }
    }
  
}