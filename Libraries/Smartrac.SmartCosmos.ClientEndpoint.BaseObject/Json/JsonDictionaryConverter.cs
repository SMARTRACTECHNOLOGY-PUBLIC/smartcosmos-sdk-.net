﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace Smartrac.SmartCosmos.Objects.Base
{
    /*
    public class JsonDictionaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            foreach (var prop in jObject)
            {
                return new MyCustomClass { Key = prop.Key, Value = prop.Value.ToString() };
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Dictionary<string, object>).IsAssignableFrom(objectType);
        }
    }
    */

    // based on ExpandoObjectConverter, but using arrays instead of IList, to behave similar to System.Web.Script.Serialization.JavaScriptSerializer
    public class DictionaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // List<KeyValuePair<string, object>> list = value as List<KeyValuePair<string, object>>;
            Dictionary<string, object> list = value as Dictionary<string, object>;
            writer.WriteStartArray();
            foreach (var item in list)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(item.Key);
                writer.WriteValue(item.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ReadValue(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IDictionary<string, object>));
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        private object ReadValue(JsonReader reader)
        {
            while (reader.TokenType == JsonToken.Comment)
            {
                if (!reader.Read())
                    throw JsonSerializationExceptionCreate(reader, "Unexpected end when reading IDictionary<string, object>.");
            }

            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return ReadObject(reader);

                case JsonToken.StartArray:
                    return ReadList(reader);

                default:
                    if (IsPrimitiveToken(reader.TokenType))
                        return reader.Value;

                    throw JsonSerializationExceptionCreate(reader, string.Format(CultureInfo.InvariantCulture, "Unexpected token when converting IDictionary<string, object>: {0}", reader.TokenType));
            }
        }

        private object ReadList(JsonReader reader)
        {
            List<object> list = new List<object>();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Comment:
                        break;

                    default:
                        object v = ReadValue(reader);

                        list.Add(v);
                        break;

                    case JsonToken.EndArray:
                        return list;
                }
            }

            throw JsonSerializationExceptionCreate(reader, "Unexpected end when reading IDictionary<string, object>.");
        }

        private object ReadObject(JsonReader reader)
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        string propertyName = reader.Value.ToString();

                        if (!reader.Read())
                            throw JsonSerializationExceptionCreate(reader, "Unexpected end when reading IDictionary<string, object>.");

                        object v = ReadValue(reader);

                        dictionary[propertyName] = v;
                        break;

                    case JsonToken.Comment:
                        break;

                    case JsonToken.EndObject:
                        return dictionary;
                }
            }

            throw JsonSerializationExceptionCreate(reader, "Unexpected end when reading IDictionary<string, object>.");
        }

        //based on internal Newtonsoft.Json.JsonReader.IsPrimitiveToken
        internal static bool IsPrimitiveToken(JsonToken token)
        {
            switch (token)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.String:
                case JsonToken.Boolean:
                case JsonToken.Undefined:
                case JsonToken.Null:
                case JsonToken.Date:
                case JsonToken.Bytes:
                    return true;

                default:
                    return false;
            }
        }

        // based on internal Newtonsoft.Json.JsonSerializationException.Create
        private static JsonSerializationException JsonSerializationExceptionCreate(JsonReader reader, string message, Exception ex = null)
        {
            return JsonSerializationExceptionCreate(reader as IJsonLineInfo, reader.Path, message, ex);
        }

        // based on internal Newtonsoft.Json.JsonSerializationException.Create
        private static JsonSerializationException JsonSerializationExceptionCreate(IJsonLineInfo lineInfo, string path, string message, Exception ex)
        {
            message = JsonPositionFormatMessage(lineInfo, path, message);

            return new JsonSerializationException(message, ex);
        }

        // based on internal Newtonsoft.Json.JsonPosition.FormatMessage
        internal static string JsonPositionFormatMessage(IJsonLineInfo lineInfo, string path, string message)
        {
            if (!message.EndsWith(Environment.NewLine))
            {
                message = message.Trim();

                if (!message.EndsWith(".", StringComparison.Ordinal))
                    message += ".";

                message += " ";
            }

            message += string.Format(CultureInfo.InvariantCulture, "Path '{0}'", path);

            if (lineInfo != null && lineInfo.HasLineInfo())
                message += string.Format(CultureInfo.InvariantCulture, ", line {0}, position {1}", lineInfo.LineNumber, lineInfo.LinePosition);

            message += ".";

            return message;
        }
    }
}