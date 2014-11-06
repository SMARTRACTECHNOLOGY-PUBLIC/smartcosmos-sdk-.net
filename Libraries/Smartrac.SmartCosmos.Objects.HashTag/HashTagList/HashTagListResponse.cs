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
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Collections.Generic;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.HashTag
{
    public class HashTagListResponse : List<HashTagListItem>, IHttpStatusCode
    {
        public HttpStatusCode HTTPStatusCode { get; set; }

        public HashTagListResponse()
            : base()
        {
            this.HTTPStatusCode = HttpStatusCode.NotImplemented;
        }
    }

    public class HashTagListItem : IResponseMessage
    {
        [JsonIgnore]
        public Urn tagUrn
        {
            get
            {
                if (String.IsNullOrEmpty(message) ||
                    (!message.Contains("urn:uuid:")))
                    return null;
                else
                    return new Urn(message);
            }
        }

        public int code { get; set; }

        public string message { get; set; }

        public HashTagListItem()
            : base()
        {
            this.code = 0;
        }
    }
}