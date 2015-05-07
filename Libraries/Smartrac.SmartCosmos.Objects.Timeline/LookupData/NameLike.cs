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

namespace Smartrac.SmartCosmos.Objects.Timeline
{
    /// <summary>
    /// Class for nameLike property for Lookup
    /// </summary>
    public class Name
    {
        private string nameLike;

        /// <summary>
        /// Any API call that requires a {nameLike} parameter must provide string value
        /// </summary>
        public string NameLike
        {
            get
            {
                return nameLike;
            }
        }

        public Name(string nameLike)
            : base()
        {
            this.nameLike = nameLike;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(nameLike) &&
                nameLike.Length <= 255;
        }

        public Name()
        {
        }
    }
}