using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
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
            return JsonConvert.SerializeObject(obj);
            /*
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
             */
        }

        public static string ToJSON<T>(this T obj, Type aType) where T : class
        {
            return JsonConvert.SerializeObject(obj);
            /*
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(aType);
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }*/
        }

        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
            /*
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }*/
        }

        public static T FromJSON<T>(this T obj, byte[] jsonBytes) where T : class
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(jsonBytes));
            /*
            using (MemoryStream stream = new MemoryStream(jsonBytes))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
             */
        }

        public static T FromJSON<T>(this T obj, Stream jsonStream) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            return serializer.ReadObject(jsonStream) as T;
        }
    }
}
