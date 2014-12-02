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

using Smartrac.Logging;
using Smartrac.SmartCosmos.Flows.BusinessRule;
using Smartrac.SmartCosmos.Flows.AccountManagement;
using Smartrac.SmartCosmos.Objects.AccountManagement;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.Objects.GeospatialManagement;
using Smartrac.SmartCosmos.Objects.HashTag;
using Smartrac.SmartCosmos.Objects.Metadata;
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

namespace Smartrac.SmartCosmos.ClientEndpoint.Factory
{
    public class EndpointFactory : IEndpointFactory
    {
        /// <summary>
        /// URL of SMART COSMOS Profiles server
        /// </summary>
        public string ProfilesServerURL { get; set; }

        /// <summary>
        /// URL of SMART COSMOS Objects server
        /// </summary>
        public string ObjectsServerURL { get; set; }

        /// <summary>
        /// URL of SMART COSMOS Flow server
        /// </summary>
        public string FlowsServerURL { get; set; }

        /// <summary>
        /// Defines if the connection should be keep alive
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// Defines if invalid server certificates are allowed
        /// </summary>
        public bool AllowInvalidServerCertificates { get; set; }

        /// <summary>
        /// Client language
        /// For a couple of functions the client can use the HTTP Accept-Language to define the language of the respond content.
        /// </summary>
        public string AcceptLanguage { get; set; }

        /// <summary>
        /// Used logger object
        /// e.g. console, file, ...
        /// </summary>
        public IMessageLogger Logger { get; set; }

        /// <summary>
        /// User name for SMART COSMOS Profiles
        /// </summary>
        public string ProfilesUserName { get; set; }

        /// <summary>
        /// User password for SMART COSMOS Profiles
        /// </summary>
        public string ProfilesUserPassword { get; set; }

        /// <summary>
        /// User name for SMART COSMOS Objects
        /// </summary>
        public string ObjectsUserName { get; set; }

        /// <summary>
        /// User password for SMART COSMOS Objects
        /// </summary>
        public string ObjectsUserPassword { get; set; }

        /// <summary>
        /// User name for SMART COSMOS Flow
        /// </summary>
        public string FlowsUserName { get; set; }

        /// <summary>
        /// User password for SMART COSMOS Flow
        /// </summary>
        public string FlowsUserPassword { get; set; }

        public EndpointFactory(IMessageLogger logger)
            : this(logger, "", "")
        {
        }

        public EndpointFactory(IMessageLogger logger, string userName, string userPassword)
            : base()
        {
            this.Logger = logger;
            this.KeepAlive = true;
            this.AcceptLanguage = "en";
            this.ProfilesServerURL = "";
            this.ObjectsServerURL = ""; // TODO
            this.FlowsServerURL = "";
            this.AllowInvalidServerCertificates = false;
            this.ProfilesUserName = userName;
            this.ProfilesUserPassword = userPassword;
        }

        #region FLOWS

        public virtual IFlowsAccountManagementEndpoint CreateFlowsAccountManagementEndpoint()
        {
            return new Smartrac.SmartCosmos.Flows.AccountManagement.FlowsAccountManagementEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(FlowsServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(FlowsUserName, FlowsUserPassword)
                .build();
        }

        public virtual IBusinessRuleEndpoint CreateBusinessRuleEndpoint()
        {
            return new BusinessRuleEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(FlowsServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(FlowsUserName, FlowsUserPassword)
                .build();
        }        

        #endregion FLOWS
        
        #region PROFILES

        public virtual IPlatformAvailabilityEndpoint CreatePlatformAvailabilityEndpoint()
        {
            return new PlatformAvailabilityEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .build();
        }

        public virtual IDataImportEndpoint CreateDataImportEndpoint()
        {
            return new DataImportEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(ProfilesUserName, ProfilesUserPassword)
                .build();
        }

        public virtual ITagVerificationEndpoint CreateTagVerificationEndpoint()
        {
            return new TagVerificationEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(ProfilesUserName, ProfilesUserPassword)
                .build();
        }

        public virtual ITagMetadataEndpoint CreateTagMetadataEndpoint()
        {
            return new TagMetadataEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(ProfilesUserName, ProfilesUserPassword)
                .build();
        }

        #endregion PROFILES

        #region OBJECTS

        public virtual IFileEndpoint CreateFileEndpoint()
        {
            return new FileEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IRegistrationEndpoint CreateRegistrationEndpoint()
        {
            return new RegistrationEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IAccountManagementEndpoint CreateAccountManagementEndpoint()
        {
            return new AccountManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IUserManagementEndpoint CreateUserManagementEndpoint()
        {
            return new UserManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IObjectManagementEndpoint CreateObjectManagementEndpoint()
        {
            return new ObjectManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IObjectInteractionEndpoint CreateObjectInteractionEndpoint()
        {
            return new ObjectInteractionEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IRelationshipManagementEndpoint CreateRelationshipManagementEndpoint()
        {
            return new RelationshipManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IGeospatialManagementEndpoint CreateGeospatialManagementEndpoint()
        {
            return new GeospatialManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IHashTagEndpoint CreateHashTagEndpoint()
        {
            return new HashTagEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IMetadataEndpoint CreateMetadataEndpoint()
        {
            return new MetadataEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        public virtual IObjectInteractionSessionEndpoint CreateObjectInteractionSessionEndpoint()
        {
            return new ObjectInteractionSessionEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ObjectsServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(ObjectsUserName, ObjectsUserPassword)
            .build();
        }

        #endregion OBJECTS
    }
}