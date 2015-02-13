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

using System;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
namespace Smartrac.SmartCosmos.Objects.DataContext
{
   public class EmptyHashTagDataContext : BaseHashTagDataContext
    {

        public string urn
        {
            get
            {
                return urn_.UUID;
            }
            set
            {
                urn_ = new Urn(value);
            }
        }

        Urn urn_;

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

        public string moniker { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public EntityReferenceType entityReferenceType { get; set; }

        public ViewType viewType { get; set; }
       
        public override Urn GetUrn()
        {
            return urn_;
        }

        public override string GetDescription()
        {
            return description;
        }

        public override string GetName()
        {
            return name;
        }

        public override string GetMoniker()
        {
            return moniker;
        }

        public override bool GetActiveFlag()
        {
            return activeFlag;
        }

        public override ViewType GetViewType()
        {
            return viewType;
        }

        public override Urn GetReferenceUrn()
        {
            return referenceUrn_;
        }

        public override EntityReferenceType GetEntityReferenceType()
        {
            return entityReferenceType;
        }

        public override void Prepare()
        {
            moniker = moniker.ResolveVariables();
            name = name.ResolveVariables();
            description = description.ResolveVariables();
        }
    }

}