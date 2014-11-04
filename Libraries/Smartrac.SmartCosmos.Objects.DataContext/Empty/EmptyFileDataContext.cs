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
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class EmptyFileDataContext : BaseFileDataContext
    {
        public EntityReferenceType entityReferenceType { get; set; }
        public ViewType viewType { get; set; }
        public Urn UrnReference { get; set; }
        public List<FileDefinition> FileDefinitions { get; set; }

        public EmptyFileDataContext()
            : base()
        {
            entityReferenceType = EntityReferenceType.Object;
            viewType = ViewType.Standard;
            UrnReference = null;
            FileDefinitions = new List<FileDefinition>();
        }

        public override EntityReferenceType GetEntityReferenceType()
        {
            return entityReferenceType;
        }

        public override ViewType GetViewType()
        {
            return viewType;
        }

        public override Urn GetUrnReference()
        {
            return UrnReference;
        }

        public override IEnumerable<FileDefinition> GetFileDefinitions()
        {
            return FileDefinitions;
        }
    }
}