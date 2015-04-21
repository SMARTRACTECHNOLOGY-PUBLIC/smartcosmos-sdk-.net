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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Smartrac.SmartCosmos.CredentialStore
{
    [XmlRoot(ElementName = "configuration")]
    public class BaseCredentialStore : ICredentialStore
    {
        [XmlElement(ElementName = "servers")]
        public Servers Servers { get; set; }

        public BaseCredentialStore()
        {
            Servers = new Servers();
        }

        public ICredential GetCredentials(SmartCosmosComponent component)
        {
            return Servers.Server.Find(i => i.Component == component);
        }

        public bool ValidateCredentials(SmartCosmosComponent component)
        {
            ICredential cred = Servers.Server.Find(i => i.Component == component);
            return (cred != null) && !String.IsNullOrEmpty(cred.Url);
        }

        public bool GetCredentials(SmartCosmosComponent component, out ICredential cred)
        {
            cred = GetCredentials(component);
            return cred != null;
        }

        public void AddServer(string url, string userName, string password, SmartCosmosComponent component)
        {
            Servers.Server.Add(new Server
            {
                Component = component,
                Name = component.GetDescription(),
                Password = password,
                Username = userName
            });
        }
    }

    [XmlRoot(ElementName = "server")]
    public class Server : ICredential
    {
        /// <summary>
        /// Component name for SMART COSMOS service
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name
        {
            get
            {
                return Component.GetDescription();
            }

            set
            {
                Component = SmartCosmosComponent.Profiles.GetFlagByDescription(value);
            }
        }

        [XmlIgnore]
        public SmartCosmosComponent Component { get; set; }

        /// <summary>
        /// User name for SMART COSMOS service
        /// </summary>
        [XmlElement(ElementName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Password for SMART COSMOS service
        /// </summary>
        [XmlElement(ElementName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// URL for SMART COSMOS service
        /// </summary>
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }

    [XmlRoot(ElementName = "servers")]
    public class Servers
    {
        [XmlElement(ElementName = "server")]
        public List<Server> Server { get; set; }
    }
}