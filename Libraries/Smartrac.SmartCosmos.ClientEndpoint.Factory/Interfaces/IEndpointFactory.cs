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
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.Objects.Registration;
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.Profiles.PlatformAvailability;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.Profiles.TagVerification;

namespace Smartrac.SmartCosmos.ClientEndpoint.Factory
{
    public interface IEndpointFactory
    {
        string ProfilesServerURL { get; set; }
        bool KeepAlive { get; set; }
        bool AllowInvalidServerCertificates { get; set; }
        string AcceptLanguage { get; set; }
        IMessageLogger Logger { get; set; }
        string UserName { get; set; }
        string UserPassword { get; set; }

        #region Profiles
        IPlatformAvailabilityEndpoint CreatePlatformAvailabilityEndpoint();
        IDataImportEndpoint CreateDataImportEndpoint();
        ITagVerificationEndpoint CreateTagVerificationEndpoint();
        ITagMetadataEndpoint CreateTagMetadataEndpoint();
        #endregion

        #region Objects
        IFileEndpoint CreateFileEndpoint();
        IRegistrationEndpoint CreateRegistrationEndpoint();
        #endregion
    }
}
