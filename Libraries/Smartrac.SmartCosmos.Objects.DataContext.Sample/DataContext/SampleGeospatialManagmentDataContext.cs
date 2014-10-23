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

using System.Collections.Generic;
using GeoJSON.Net;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    public class SampleGeospatialManagementDataContext : BaseGeospatialManagementDataContext
    {
        public override Urn GetGeospatialUrn()
        {
            return null;
        }

        public override string GetName()
        {
            return "Campus at 5th Street";
        }

        public override string GetCategory()
        {
            return "Building";
        }

        public override string GetDescription()
        {
            return "Campus at 5th Street descr";
        }

        public override bool GetActiveFlag()
        {
            return true;
        }

        public override GeoJSONObject GetGeometricShape()
        {
            var point = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.GeographicPosition(45.79012, 15.94107));
            var featureProperties = new Dictionary<string, object> { { "Name", "Foo" } };
            var model = new GeoJSON.Net.Feature.Feature(point, featureProperties, "FeatureCollection");
            return model;
        }

        public override ViewType GetViewType()
        {
            return ViewType.Standard;
        }
    }
}