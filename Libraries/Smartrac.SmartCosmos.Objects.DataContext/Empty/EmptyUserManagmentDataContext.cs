﻿#region License

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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;
using System;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class EmptyUserManagementDataContext : BaseUserManagementDataContext
    {
        public string userUrn
        {
            get
            {
                return userUrn_.UUID;
            }
            set
            {
                userUrn_ = new Urn(value);
            }
        }

        private Urn userUrn_;

        public string newPassword { get; set; }

        public string eMailAddress { get; set; }

        public string givenName { get; set; }

        public string surname { get; set; }

        public RoleType roleType { get; set; }

        public ViewType viewType { get; set; }

        public override Urn GetUserUrn()
        {
            return userUrn_;
        }

        public override ViewType GetViewType()
        {
            return viewType;
        }

        public override string GetNewPassword()
        {
            return newPassword;
        }

        public override string GeteMailAddress()
        {
            return eMailAddress;
        }

        public override RoleType GetRoleType()
        {
            return roleType;
        }

        public override string GetGivenName()
        {
            return givenName;
        }

        public override string GetSurname()
        {
            return surname;
        }

        public override void Prepare()
        {
            newPassword = newPassword.ResolveVariables();
            eMailAddress = newPassword.ResolveVariables();
            givenName = newPassword.ResolveVariables();
            surname = newPassword.ResolveVariables();
        }
    }
}