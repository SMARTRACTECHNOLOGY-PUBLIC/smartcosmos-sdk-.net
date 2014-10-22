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

using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Smartrac.SmartCosmos.ClientEndpoint.BaseObject
{
    /// <summary>
    /// Object extensions
    /// </summary>
    public static class ObjectExtensions
    {
        public static string ToJSON<T>(this T obj) where T : class
        {
            if (obj == null)
                return "";
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToJSON<T>(this T obj, bool formatIndented) where T : class
        {
            if (obj == null)
                return "";
            return JsonConvert.SerializeObject(obj, (formatIndented) ? Formatting.Indented : Formatting.None );
        }

        /*
        public static string ToJSON<T>(this T obj, Type aType) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }
        */
        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T FromJSON<T>(this T obj, byte[] jsonBytes) where T : class
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(jsonBytes));
        }

        public static T FromJSON<T>(this T obj, Stream jsonStream) where T : class
        {
            // convert stream to string
            StreamReader reader = new StreamReader(jsonStream);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        /*
        public static T FromJSON<T>(this T obj, Type aType, Stream jsonStream) where T : class
        {
            // convert stream to string
            StreamReader reader = new StreamReader(jsonStream);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }
        */
    }
}