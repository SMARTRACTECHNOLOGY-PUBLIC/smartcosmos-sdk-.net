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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Smartrac.SmartCosmos.DataContextFactory.XML
{
    public sealed class XMLDataContextSerializer<T>
        where T : BaseDataContext
    {
        /// <summary>
        /// deserialize objects from XML entry
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static T DeSerialize(XElement elem)
        {
            if (elem == null) return null;

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(elem.ToString())))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));

                T obj = (T)xmlSerializer.Deserialize(memoryStream);
                if (obj != null)
                    obj.Prepare();
                return obj;
            }
        }
    }
}