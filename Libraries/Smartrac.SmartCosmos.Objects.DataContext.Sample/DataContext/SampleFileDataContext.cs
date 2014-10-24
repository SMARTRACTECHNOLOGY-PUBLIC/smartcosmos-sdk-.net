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

using System.Collections.Generic;
using System.IO;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    public class SampleFileDataContext : BaseFileDataContext
    {
        public override EntityReferenceType GetEntityReferenceType()
        {
            return EntityReferenceType.Object;
        }

        public override ViewType GetViewType()
        {
            return ViewType.Standard;
        }

        public override Urn GetUrnReference()
        {
            return MyDataContext.GetSampleObjectUrn();
        }

        public override IEnumerable<FileDefinition> GetFileDefinitions()
        {
            List<FileDefinition> FileDefinitions = new List<FileDefinition>();
            string sampleDataFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"..\..\..\..\..\Documentation\SampleData\objects.png");
            if (System.IO.File.Exists(sampleDataFile))
            {
                FileDefinition fileDef = new FileDefinition
                {
                    fileName = Path.GetFileName(sampleDataFile),
                    mimeType = "image/png"
                };

                FileStream file = new FileStream(sampleDataFile, FileMode.Open);
                try
                {
                    fileDef.file = new MemoryStream();
                    file.CopyTo(fileDef.file);
                }
                finally
                {
                    file.Close();
                }
                FileDefinitions.Add(fileDef);
            }

            return FileDefinitions;
        }
    }
}