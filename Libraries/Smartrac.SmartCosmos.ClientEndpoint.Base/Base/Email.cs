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

namespace Smartrac.SmartCosmos.ClientEndpoint.Base
{
    /// <summary>
    ///
    /// </summary>
    public class Email
    {
        private string email_;

        /// <summary>
        /// Any API call that requires a {emailAddress} parameter must provide a proper email adress, e.g. volodymyr.bondarenko@smartrac-group.com
        /// </summary>
        public string Mail
        {
            get
            {
                return email_;
            }
        }

        public Email(string email)
            : base()
        {
            this.email_ = email;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(Mail) &&
                Mail.Contains("@");
        }

        public Email()
        {
        }
    }
}