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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Smartrac.SmartCosmos.Objects.Base
{
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
            get { return true; }
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