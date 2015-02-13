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
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Smartrac.SmartCosmos.CredentialStore;

namespace Smartrac.SmartCosmos.ClientEndpoint.Factory
{
    [XmlRoot(ElementName = "configuration")]
    public class XMLCredentialStore : BaseCredentialStore
    {
        public static XMLCredentialStore ReadFromFile(string fileName)
        {
            return XMLDataSerializer<XMLCredentialStore>.DeSerialize(fileName);
        }
    }

    public class XMLDataSerializer<T> where T : class
    {
        public static T DeSerialize(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return null;
            }

            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextReader reader = new StreamReader(fileName))
            {
                return ser.Deserialize(reader) as T;
            }
        }

        public static bool Serialize(string fileName, T data)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlSerializer ser = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
            {
                ser.Serialize(writer, data, ns);
            }
            return File.Exists(fileName);
        }
    }
}
