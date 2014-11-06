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
                    return new Polygon();

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