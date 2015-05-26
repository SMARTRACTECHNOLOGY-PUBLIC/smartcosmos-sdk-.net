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

using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.DataContext.XML;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext.XML;
using System.Xml.Linq;

namespace Smartrac.SmartCosmos.DataContextFactory.XML
{
    public class XMLDataContextFactory : BaseDataContextFactory
    {
        protected XDocument doc = null;
        protected ITagDataContext TagDataContext = null;
        protected IFileDataContext FileDataContext = null;
        protected IDeviceDataContext DeviceDataContext = null;
        protected IMetadataDataContext MetadataDataContext = null;
        protected IObjectInteractionDataContext ObjectInteractionDataContext = null;
        protected IObjectInteractionSessionDataContext ObjectInteractionSessionDataContext = null;
        protected IObjectManagementDataContext ObjectManagementDataContext = null;
        protected IRegistrationDataContext RegistrationDataContext = null;
        protected IRelationshipManagementDataContext RelationshipManagementDataContext = null;
        protected IUserManagementDataContext UserManagementDataContext = null;
        protected IHashTagDataContext HashTagDataContext = null;
        protected IGeospatialManagementDataContext GeospatialManagementDataContext = null;
        protected IAccountManagementDataContext AccountManagementDataContext = null;

        public XMLDataContextFactory(string XMLfile)
            : base()
        {
            doc = XDocument.Load(XMLfile);
        }

        public override ITagDataContext CreateTagDataContext()
        {
            if (TagDataContext == null)
            {
                TagDataContext = XMLDataContextSerializer<XMLTagDataContext>.DeSerialize(doc.Element("data").Element("TagDataContext")) as ITagDataContext;
            }

            return TagDataContext;
        }

        public override IFileDataContext CreateFileDataContext()
        {
            if (FileDataContext == null)
            {
                FileDataContext = XMLDataContextSerializer<XMLFileDataContext>.DeSerialize(doc.Element("data").Element("FileDataContext")) as IFileDataContext;
            }
            return FileDataContext;
        }

        public override IDeviceDataContext CreateDeviceDataContext()
        {
            if (DeviceDataContext == null)
            {
                DeviceDataContext = XMLDataContextSerializer<XMLDeviceDataContext>.DeSerialize(doc.Element("data").Element("DeviceDataContext")) as IDeviceDataContext;
            }
            return DeviceDataContext;
        }

        public override IRegistrationDataContext CreateRegistrationDataContext()
        {
            if (RegistrationDataContext == null)
            {
                RegistrationDataContext = XMLDataContextSerializer<XMLRegistrationDataContext>.DeSerialize(doc.Element("data").Element("RegistrationDataContext")) as IRegistrationDataContext;
            }
            return RegistrationDataContext;
        }

        public override IAccountManagementDataContext CreateAccountManagementDataContext()
        {
            if (AccountManagementDataContext == null)
            {
                AccountManagementDataContext = XMLDataContextSerializer<XMLAccountManagementDataContext>.DeSerialize(doc.Element("data").Element("AccountManagementDataContext")) as IAccountManagementDataContext;
            }
            return AccountManagementDataContext;
        }

        public override IUserManagementDataContext CreateUserManagementDataContext()
        {
            if (UserManagementDataContext == null)
            {
                UserManagementDataContext = XMLDataContextSerializer<XMLUserManagementDataContext>.DeSerialize(doc.Element("data").Element("UserManagementDataContext")) as IUserManagementDataContext;
            }
            return UserManagementDataContext;
        }

        public override IObjectManagementDataContext CreateObjectManagementDataContext()
        {
            if (ObjectManagementDataContext == null)
            {
                ObjectManagementDataContext = XMLDataContextSerializer<XMLObjectManagementDataContext>.DeSerialize(doc.Element("data").Element("ObjectManagementDataContext")) as IObjectManagementDataContext;
            }
            return ObjectManagementDataContext;
        }

        public override IObjectInteractionDataContext CreateObjectInteractionDataContext()
        {
            if (ObjectInteractionDataContext == null)
            {
                ObjectInteractionDataContext = XMLDataContextSerializer<XMLObjectInteractionDataContext>.DeSerialize(doc.Element("data").Element("ObjectInteractionDataContext")) as IObjectInteractionDataContext;
            }
            return ObjectInteractionDataContext;
        }

        public override IRelationshipManagementDataContext CreateRelationshipManagementDataContext()
        {
            if (RelationshipManagementDataContext == null)
            {
                RelationshipManagementDataContext = XMLDataContextSerializer<XMLRelationshipManagementDataContext>.DeSerialize(doc.Element("data").Element("RelationshipManagementDataContext")) as IRelationshipManagementDataContext;
            }
            return RelationshipManagementDataContext;
        }

        public override IGeospatialManagementDataContext CreateGeospatialManagementDataContext()
        {
            if (GeospatialManagementDataContext == null)
            {
                GeospatialManagementDataContext = XMLDataContextSerializer<XMLGeospatialManagementDataContext>.DeSerialize(doc.Element("data").Element("GeospatialManagementDataContext")) as IGeospatialManagementDataContext;
            }
            return GeospatialManagementDataContext;
        }

        public override IHashTagDataContext CreateHashTagDataContext()
        {
            if (HashTagDataContext == null)
            {
                HashTagDataContext = XMLDataContextSerializer<XMLHashTagDataContext>.DeSerialize(doc.Element("data").Element("HashTagDataContext")) as IHashTagDataContext;
            }
            return HashTagDataContext;
        }

        public override IObjectInteractionSessionDataContext CreateObjectInteractionSessionDataContext()
        {
            if (ObjectInteractionSessionDataContext == null)
            {
                ObjectInteractionSessionDataContext = XMLDataContextSerializer<XMLObjectInteractionSessionDataContext>.DeSerialize(doc.Element("data").Element("ObjectInteractionSessionDataContext")) as IObjectInteractionSessionDataContext;
            }
            return ObjectInteractionSessionDataContext;
        }

        public override IMetadataDataContext CreateMetadataDataContext()
        {
            if (MetadataDataContext == null)
            {
                MetadataDataContext = XMLDataContextSerializer<XMLMetadataDataContext>.DeSerialize(doc.Element("data").Element("MetadataDataContext")) as IMetadataDataContext;
            }
            return MetadataDataContext;
        }
    }
}