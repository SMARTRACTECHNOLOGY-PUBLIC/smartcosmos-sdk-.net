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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System.Collections.Generic;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.File
{
    public class FileDefinitionRetrievalResponse : DefaultResponse
    {
        private Urn urn_;

        public string urn
        {
            get
            {
                return (urn_ != null) ? urn_.UUID : "";
            }
            set
            {
                urn_ = new Urn(value);
            }
        }

        public long timestamp { get; set; }

        public long lastModifiedTimestamp { get; set; }

        public string fileName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        public string mimeType { get; set; }

        [JsonIgnore]
        public Urn Urn
        {
            get
            {
                return urn_;
            }
        }

        public string SmartCosmosEvent { get; set; }
    }

    public class FileDefinitionRetrievalListResponse : List<FileDefinitionRetrievalResponse>, IHttpStatusCode
    {
        private HttpStatusCode HTTPStatusCode_;

        public HttpStatusCode HTTPStatusCode
        {
            get
            {
                return HTTPStatusCode_;
            }

            set
            {
                HTTPStatusCode_ = value;
                ForEach(i => i.HTTPStatusCode = value);
            }
        }

        public FileDefinitionRetrievalListResponse()
            : base()
        {
            this.HTTPStatusCode = HttpStatusCode.NotImplemented;
        }
    }
}