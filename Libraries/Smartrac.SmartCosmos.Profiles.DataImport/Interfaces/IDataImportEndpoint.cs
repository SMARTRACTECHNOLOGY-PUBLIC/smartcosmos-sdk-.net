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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;

namespace Smartrac.SmartCosmos.Profiles.DataImport
{
    public enum DataActionResult
    {
        /// <summary>
        /// validation of import package was successful and data import will start immediately
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed,

        /// <summary>
        /// user not authorized
        /// </summary>
        Unauthorized
    }

    public interface IDataImportEndpoint : IBaseEndpoint
    {
        DataActionResult UploadFileAsOctetStream(Stream data, out FileUploadResponse responseData);
        DataActionResult UploadFileAsOctetStream(string file, out FileUploadResponse responseData);

        //public DataActionResult UploadFileAsMultiPartForm(Stream data, out FileUploadResponse responseData)

        DataActionResult CheckImportState(ImportStateRequest requestData, out ImportStateResponse responseData);
    }
}
