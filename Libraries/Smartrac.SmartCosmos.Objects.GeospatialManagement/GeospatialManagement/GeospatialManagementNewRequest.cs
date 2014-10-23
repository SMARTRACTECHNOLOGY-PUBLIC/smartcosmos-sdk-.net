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

#endregion License

using System;
using GeoJSON.Net;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    public class GeospatialManagementNewRequest : BaseRequest
    {
        public bool activeFlag { get; set; }

        public string description { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string moniker { get; set; }

        public GeoJSONObject geometricShape { get; set; }

        public GeospatialManagementNewRequest()
            : base()
        {
            description = null;
            activeFlag = true;
            moniker = null;
        }

        public override bool IsValid()
        {
            return base.IsValid() &&
                (geometricShape != null) &&
                !String.IsNullOrEmpty(type) &&
                (type.Length <= 255) &&
                !String.IsNullOrEmpty(name) &&
                (name.Length <= 255) &&
                (String.IsNullOrEmpty(moniker) || moniker.Length <= 2048) &&
                (String.IsNullOrEmpty(description) || description.Length <= 1024);
        }
    }
}