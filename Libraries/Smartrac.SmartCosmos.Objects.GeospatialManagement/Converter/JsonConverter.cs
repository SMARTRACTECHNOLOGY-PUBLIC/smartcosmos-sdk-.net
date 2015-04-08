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

using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jsonObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class GeoJSONCreationConverter : JsonCreationConverter<GeoJSONObject>
    {
        protected override GeoJSONObject Create(Type objectType, JObject jsonObject)
        {
            string typeName = (jsonObject["type"]).ToString();
            switch (typeName)
            {
                case "FeatureCollection":
                    return new FeatureCollection(null);

                case "Point":
                    return new Point(null);

                case "MultiPoint":
                    return new MultiPoint();

                case "LineString":
                    return new LineString(null);

                case "MultiLineString":
                    return new MultiLineString(null);

                case "Polygon":
                    return new Polygon(null);

                case "MultiPolygon":
                    return new MultiPolygon();

                case "GeometryCollection":
                    return new GeometryCollection();

                case "Feature":
                    return new Feature(null);

                default:
                    return null;
            }
        }
    }
}