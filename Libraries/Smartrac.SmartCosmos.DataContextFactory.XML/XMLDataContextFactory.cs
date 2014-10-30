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

using System.Xml.Linq;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext;

namespace Smartrac.SmartCosmos.DataContextFactory.XML
{
    public class XMLDataContextFactory : BaseDataContextFactory
    {
        protected XDocument doc = null;
        protected ITagDataContext TagDataContext = null;
        protected IFileDataContext FileDataContext = null;
        
        public XMLDataContextFactory(string XMLfile)
            : base()
        {
            doc = XDocument.Load(XMLfile);
        }

        public override ITagDataContext CreateTagDataContext()
        {
            if (TagDataContext == null)
            {
                EmptyTagDataContext tagDataContext = new EmptyTagDataContext();
                XElement dataContext = doc.Element("tagDataContext");
                
                    foreach (var elem in dataContext.Descendants("tag"))
                        tagDataContext.TagIds.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("tagProperty"))
                        tagDataContext.TagProperties.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("verificationType"))
                        tagDataContext.VerificationTypes.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("importId"))
                        tagDataContext.ImportIds.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("tagDataFile"))
                        tagDataContext.TagDataFiles.Add(elem.Value);
                
                TagDataContext = tagDataContext;
            }

            return TagDataContext;
        }

        public override IFileDataContext CreateFileDataContext()
        {
            return null;
            /*
            if (TagDataContext == null)
            {
                EmptyFileDataContext fileDataContext = new EmptyFileDataContext();
                foreach (var dataContext in doc.Descendants("fileDataContext"))
                {
                    foreach (var elem in dataContext.Descendants("viewType"))
                        fileDataContext.CurrentViewType = ViewType . elem.Value
                    foreach (var elem in dataContext.Descendants("tagProperty"))
                        tagDataContext.TagProperties.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("verificationType"))
                        tagDataContext.VerificationTypes.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("importId"))
                        tagDataContext.ImportIds.Add(elem.Value);
                    foreach (var elem in dataContext.Descendants("tagDataFile"))
                        tagDataContext.TagDataFiles.Add(elem.Value);
                }
                FileDataContext = fileDataContext;
            }

            return TagDataContext;
             */
        }

        public override IRegistrationDataContext CreateRegistrationDataContext()
        {
            return null; // new SampleRegistrationDataContext();
        }

        public override IAccountManagementDataContext CreateAccountManagementDataContext()
        {
            return null; // new SampleAccountManagementDataContext();
        }

        public override IUserManagementDataContext CreateUserManagementDataContext()
        {
            return null; // new SampleUserManagementDataContext();
        }

        public override IObjectManagementDataContext CreateObjectManagementDataContext()
        {
            return null; // new SampleObjectManagementDataContext();
        }

        public override IObjectInteractionDataContext CreateObjectInteractionDataContext()
        {
            return null; // new SampleObjectInteractionDataContext();
        }

        public override IRelationshipManagementDataContext CreateRelationshipManagementDataContext()
        {
            return null; // new SampleRelationshipManagementDataContext();
        }

        public override IGeospatialManagementDataContext CreateGeospatialManagementDataContext()
        {
            return null; // new SampleGeospatialManagementDataContext();
        }

        public override IHashTagDataContext CreateHashTagDataContext()
        {
            return null; // new SampleHashTagDataContext();
        }

        public override IObjectInteractionSessionDataContext CreateObjectInteractionSessionDataContext()
        {
            return null; // new SampleObjectInteractionSessionDataContext();
        }

        public override IMetadataDataContext CreateMetadataDataContext()
        {
            return null; // new SampleMetadataDataContext();
        }
    }
}