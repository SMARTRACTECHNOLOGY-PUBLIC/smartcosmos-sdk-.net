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

using Smartrac.SmartCosmos.Objects.Base;
using System;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class EmptyDeviceDataContext : BaseDeviceDataContext
    {
        public ViewType viewType { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public string identification { get; set; }

        public EmptyDeviceDataContext()
            : base()
        {
            viewType = ViewType.Standard;
        }

        public override ViewType GetViewType()
        {
            return ViewType.Standard;
        }

        public override string GetIdentification()
        {
            return identification;
        }

        public override string GetName()
        {
            return name;
        }

        public override string GetDeviceType()
        {
            return type;
        }

        public override void Prepare()
        {
            type = type.ResolveVariables();
            name = name.ResolveVariables();
            identification = identification.ResolveVariables();
        }
    }
}