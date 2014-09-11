﻿#region License
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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smartrac.SmartCosmos.Profiles.DataImport
{
    [DataContract]
    public class ImportStateResponse
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public int state { get; set; }
        [DataMember]
        public int progress { get; set; }
        [DataMember]
        public string message { get; set; }
    }

    [DataContract]
    public class ImportStateRequest
    {
        [DataMember]
        public string importId { get; set; }

        public ImportStateRequest(string aImportId)
        {
            this.importId = aImportId;
        }
    }
}