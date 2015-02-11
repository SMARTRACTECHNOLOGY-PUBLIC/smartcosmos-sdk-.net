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
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class EmptyNotificationDataContext : BaseNotificationDataContext
    {
        public ViewType viewType { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string eMailAddress { get; set; }

        public EmptyNotificationDataContext()
            : base()
        {
            viewType = ViewType.Standard;
        }

        public override ViewType GetViewType()
        {
            return ViewType.Standard;
        }

        public override string GetOldPassword()
        {
            return OldPassword;
        }

        public override string GetNewPassword()
        {
            return NewPassword;
        }

        public override string GeteMailAddress()
        {
            return eMailAddress;
        }

        public override void Prepare()
        {
            OldPassword = OldPassword.ResolveVariables();
            NewPassword = NewPassword.ResolveVariables();
            eMailAddress = eMailAddress.ResolveVariables();
        }
    }
}