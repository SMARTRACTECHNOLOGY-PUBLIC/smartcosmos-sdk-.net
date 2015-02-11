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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Flows.AccountManagement;
using Smartrac.SmartCosmos.Flows.BusinessRule;
using Smartrac.SmartCosmos.Objects.AccountManagement;
using Smartrac.SmartCosmos.Objects.Device;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.Objects.GeospatialManagement;
using Smartrac.SmartCosmos.Objects.HashTag;
using Smartrac.SmartCosmos.Objects.Metadata;
using Smartrac.SmartCosmos.Objects.Notification;
using Smartrac.SmartCosmos.Objects.ObjectInteraction;
using Smartrac.SmartCosmos.Objects.ObjectInteractionSession;
using Smartrac.SmartCosmos.Objects.ObjectManagement;
using Smartrac.SmartCosmos.Objects.Registration;
using Smartrac.SmartCosmos.Objects.RelationshipManagement;
using Smartrac.SmartCosmos.Objects.UserManagement;
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.Profiles.PlatformAvailability;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.Profiles.TagVerification;
using Smartrac.SmartCosmos.AccountManager.User;
using Smartrac.SmartCosmos.AccountManager.Role;
using Smartrac.SmartCosmos.CredentialStore;

namespace Smartrac.SmartCosmos.ClientEndpoint.Factory
{
    public interface IEndpointFactory
    {
        ICredentialStore CredentialStore { get; set; }
        
        /// <summary>
        /// Defines if the connection should be keep alive
        /// </summary>
        bool KeepAlive { get; set; }

        /// <summary>
        /// Defines if invalid server certificates are allowed
        /// </summary>
        bool AllowInvalidServerCertificates { get; set; }

        /// <summary>
        /// Client language
        /// For a couple of functions the client can use the HTTP Accept-Language to define the language of the respond content.
        /// </summary>
        string AcceptLanguage { get; set; }

        /// <summary>
        /// Used logger object
        /// e.g. console, file, ...
        /// </summary>
        IMessageLogger Logger { get; set; }
  

        #region Profiles

        /// <summary>
        /// Create platform availability endpoint
        /// </summary>
        /// <returns>IPlatformAvailabilityEndpoint</returns>
        IPlatformAvailabilityEndpoint CreatePlatformAvailabilityEndpoint();

        /// <summary>
        /// Create data import endpoint
        /// </summary>
        /// <returns>IDataImportEndpoint</returns>
        IDataImportEndpoint CreateDataImportEndpoint();

        /// <summary>
        /// Create tag verification endpoint
        /// </summary>
        /// <returns>ITagVerificationEndpoint</returns>
        ITagVerificationEndpoint CreateTagVerificationEndpoint();

        /// <summary>
        /// Create tag metadata endpoint
        /// </summary>
        /// <returns>ITagMetadataEndpoint</returns>
        ITagMetadataEndpoint CreateTagMetadataEndpoint();

        #endregion Profiles

        #region Objects

        /// <summary>
        /// Create account management endpoint
        /// </summary>
        /// <returns>IAccountManagementEndpoint</returns>
        IAccountManagementEndpoint CreateAccountManagementEndpoint();

        IFileEndpoint CreateFileEndpoint();

        IGeospatialManagementEndpoint CreateGeospatialManagementEndpoint();

        IHashTagEndpoint CreateHashTagEndpoint();

        IMetadataEndpoint CreateMetadataEndpoint();

        IObjectInteractionEndpoint CreateObjectInteractionEndpoint();

        IObjectInteractionSessionEndpoint CreateObjectInteractionSessionEndpoint();

        IObjectManagementEndpoint CreateObjectManagementEndpoint();

        IRegistrationEndpoint CreateRegistrationEndpoint();

        IRelationshipManagementEndpoint CreateRelationshipManagementEndpoint();

        IUserManagementEndpoint CreateUserManagementEndpoint();

        IDeviceEndpoint CreateDeviceEndpoint();

        INotificationEndpoint CreateNotificationEndpoint();

        #endregion Objects

        #region Flows

        /// <summary>
        /// Create account management endpoints
        /// </summary>
        /// <returns>IAccountManagementEndpoint</returns>
        IFlowsAccountManagementEndpoint CreateFlowsAccountManagementEndpoint();

        /// <summary>
        /// Create business rule endpoints
        /// </summary>
        /// <returns>IBusinessRuleEndpoint</returns>
        IBusinessRuleEndpoint CreateBusinessRuleEndpoint();

        #endregion Flows

        #region AccountManager

        /// <summary>
        /// Create User's Accounts Endpoint
        /// </summary>
        /// <returns>IUserEndpoint</returns>
        IUserEndpoint CreateUserEndpoint();

        /// <summary>
        /// Create Roles Endpoint
        /// </summary>
        /// <returns>IRoleEndpoint</returns>
        IRoleEndpoint CreateRoleEndpoint();

        #endregion AccountManager
    }
}