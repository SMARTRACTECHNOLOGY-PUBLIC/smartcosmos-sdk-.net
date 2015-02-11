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

using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class BaseObjectInteractionDataContext : BaseDataContext, IObjectInteractionDataContext
    {
        public virtual Urn GetObjectUrn()
        {
            return null;
        }

        public virtual Urn GetReferenceUrn()
        {
            return null;
        }

        /// <summary>
        /// The type field is available to create an ontology or high level categories that can be used to group related interactions. The platform makes no inferences about this case-sensitive field.
        /// </summary>
        /// <returns>interaction type</returns>
        public virtual string GetInteractionType()
        {
            return "";
        }

        public virtual long GetRecordedTimestamp()
        {
            return 0;
        }

        public virtual string GetDescription()
        {
            return "";
        }

        public virtual bool GetActiveFlag()
        {
            return true;
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        /// <returns>EntityReferenceType</returns>
        public virtual EntityReferenceType GetEntityReferenceType()
        {
            return EntityReferenceType.Object;
        }

        public virtual ViewType GetViewType()
        {
            return ViewType.Standard;
        }
    }
}