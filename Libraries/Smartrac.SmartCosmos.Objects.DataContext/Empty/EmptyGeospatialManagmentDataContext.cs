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
using GeoJSON.Net;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;
using System.Collections.Generic;
namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class EmptyGeospatialManagementDataContext : BaseGeospatialManagementDataContext
    {

        public string geospatialUrn
        {
            get
            {
                return geospatialUrn_.UUID;
            }
            set
            {
                geospatialUrn_ = new Urn(value);
            }
        }

        Urn geospatialUrn_;

        /*public GeoJSONObjectBuilder geometricShape
        {
            get;
            set;
        }*/

        //Urn referenceUrn_;

        public string name { get; set; }

        public string category { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }
        
        public GeoJSONObject geometricShape { get; set; }

        public ViewType viewType { get; set; }
        
        public override Urn GetGeospatialUrn()
        {
            return geospatialUrn_;
        }

        public override string GetName()
        {
            return name;
        }

        public override string GetCategory()
        {
            return category;
        }

        public override string GetDescription()
        {
            return description;
        }

        public override bool GetActiveFlag()
        {
            return activeFlag;
        }

        public override GeoJSONObject GetGeometricShape()
        {
            return geometricShape;
        }

        public override ViewType GetViewType()
        {
            return viewType;
        }

        public override void Prepare()
        {
            name = name.ResolveVariables();
            category = category.ResolveVariables();
            description = description.ResolveVariables();
        }
    }

    /*In future can be improved
     * 
     * public class GeoJSONObjectBuilder
    {
        public double geometricShapeWidth;

        public double geometricShapeHeight;

        public string geometricShapeDictionaryKey { get; set; }

        public string geometricShapeDictionaryValue { get; set; }

        public GeoJSONObject getGeoJSONObject()
        {
            var point = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.GeographicPosition(geometricShapeWidth, geometricShapeHeight));
            var featureProperties = new Dictionary<string, object> { { geometricShapeDictionaryKey, geometricShapeDictionaryValue } };
            var model = new GeoJSON.Net.Feature.Feature(point, featureProperties);
            return model;
        }
    }*/

}