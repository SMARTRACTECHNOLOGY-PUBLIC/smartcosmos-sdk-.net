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

namespace Smartrac.SmartCosmos.Objects.Base
{
    /// <summary>
    /// Base class for all SMART COSMOS urn´s
    /// </summary>
    public class Urn
    {
        private string UUID_;

        /// <summary>
        /// Any API call that requires a {referenceUrn} parameter must provide a full URN UUID reference, e.g. urn:uuid:66b7d3e9-69e1-499e-a867-9c4a939c6f7d
        /// </summary>
        public string UUID
        {
            get
            {
                return UUID_;
            }
        }

        public Urn(string uuid)
            : base()
        {
            this.UUID_ = uuid;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(UUID) &&
                UUID.Length <= 1024;
        }

        public Urn()
        {
        }
    }
}