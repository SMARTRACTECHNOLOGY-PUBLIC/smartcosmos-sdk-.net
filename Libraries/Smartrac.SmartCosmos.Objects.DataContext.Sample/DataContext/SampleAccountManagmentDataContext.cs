﻿#region License
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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.DataContext;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    public class SampleAccountManagementDataContext : BaseAccountManagementDataContext
    {
        public override ViewType GetViewType()
        {
            return ViewType.Standard;
        }

        public override string GetOldPassword()
        {
            return "111";
        }

        public override string GetNewPassword()
        {
            return "222";
        }

        public override string GeteMailAddress()
        {
            return "you@foo.com";
        }
    }
}