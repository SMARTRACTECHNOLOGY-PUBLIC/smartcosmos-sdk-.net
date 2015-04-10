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

using GeoJSON.Net;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext
{
    public class BaseGeospatialManagementDataContext : BaseDataContext, IGeospatialManagementDataContext
    {
        public virtual Urn GetGeospatialUrn()
        {
            return null;
        }

        public virtual string GetName()
        {
            return null;
        }

        public virtual string GetCategory()
        {
            return null;
        }

        public virtual string GetDescription()
        {
            return null;
        }

        public virtual bool GetActiveFlag()
        {
            return true;
        }

        public virtual GeoJSONObject GetGeometricShape()
        {
            return null;
        }

        public virtual ViewType GetViewType()
        {
            return ViewType.Standard;
        }
    }
}