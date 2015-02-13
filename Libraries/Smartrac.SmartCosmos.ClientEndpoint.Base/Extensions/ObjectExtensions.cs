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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;

namespace Smartrac.SmartCosmos.ClientEndpoint.BaseObject
{
    /// <summary>
    /// Object extensions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// serialize the object to JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON<T>(this T obj) where T : class
        {
            if (obj == null)
                return "";
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        /// <summary>
        /// serialize the object to (indented) JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="formatIndented"></param>
        /// <returns></returns>
        public static string ToJSON<T>(this T obj, bool formatIndented) where T : class
        {
            if (obj == null)
                return "";
            return JsonConvert.SerializeObject(obj,
                (formatIndented) ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        /// <summary>
        /// serialize the object to (indented) JSON dependend on settings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="formatIndented"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string ToJSON<T>(this T obj, bool formatIndented, JsonSerializerSettings settings) where T : class
        {
            if (obj == null)
                return "";
            return JsonConvert.SerializeObject(obj,
                (formatIndented) ? Formatting.Indented : Formatting.None,
                settings);
        }

        /// <summary>
        /// deserialize objects from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// deserialize objects from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="jsonBytes"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this T obj, byte[] jsonBytes) where T : class
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(jsonBytes));
        }

        /// <summary>
        /// deserialize objects from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="jsonStream"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this T obj, Stream jsonStream) where T : class
        {
            // convert stream to string
            StreamReader reader = new StreamReader(jsonStream);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        /// <summary>
        /// deserialize objects from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="jsonStream"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this T obj, Stream jsonStream, JsonSerializerSettings settings) where T : class
        {
            // convert stream to string
            StreamReader reader = new StreamReader(jsonStream);
            if (settings == null)
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            else
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd(), settings);
        }
    }
}