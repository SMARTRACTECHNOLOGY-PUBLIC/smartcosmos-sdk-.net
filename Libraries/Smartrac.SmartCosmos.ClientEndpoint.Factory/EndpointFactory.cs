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

using Smartrac.SmartCosmos.Logging;
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
using Smartrac.SmartCosmos.Objects.Device;
using Smartrac.SmartCosmos.Objects.Notification;
using Smartrac.SmartCosmos.AccountManager.User;
using Smartrac.SmartCosmos.AccountManager.Role;
using Smartrac.SmartCosmos.CredentialStore;

namespace Smartrac.SmartCosmos.ClientEndpoint.Factory
{
    public class EndpointFactory : IEndpointFactory
    {
        public ICredentialStore CredentialStore { get; set; }

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

        public EndpointFactory(IMessageLogger logger, ICredentialStore credentialStore)
            : base()
        {
            this.Logger = logger;
            this.CredentialStore = credentialStore;
            this.KeepAlive = true;
            this.AcceptLanguage = "en";
            this.AllowInvalidServerCertificates = false;
        }

        private bool GetCredentials(SmartCosmosComponent component, out ICredential cred)
        {
            cred = (CredentialStore != null) ? CredentialStore.GetCredentials(component) : null;
            return (cred != null);
        }

        #region ACCOUNT_MANAGER

        public virtual IUserEndpoint CreateUserEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.AccountManager, out cred)) 
              return null;

            return new UserEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        public virtual IRoleEndpoint CreateRoleEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.AccountManager, out cred))
                return null;

            return new RoleEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        #endregion ACCOUNT_MANAGER

        #region FLOWS

        public virtual IFlowsAccountManagementEndpoint CreateFlowsAccountManagementEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Flows, out cred))
                return null;
            
            return new Smartrac.SmartCosmos.Flows.AccountManagement.FlowsAccountManagementEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        public virtual IBusinessRuleEndpoint CreateBusinessRuleEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Flows, out cred))
                return null;

            return new BusinessRuleEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        #endregion FLOWS

        #region PROFILES

        public virtual IPlatformAvailabilityEndpoint CreatePlatformAvailabilityEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Profiles, out cred))
                return null;

            return new PlatformAvailabilityEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .build();
        }

        public virtual IDataImportEndpoint CreateDataImportEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Profiles, out cred))
                return null;

            return new DataImportEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        public virtual ITagVerificationEndpoint CreateTagVerificationEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Profiles, out cred))
                return null;

            return new TagVerificationEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        public virtual ITagMetadataEndpoint CreateTagMetadataEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Profiles, out cred))
                return null;

            return new TagMetadataEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(cred.Url)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(cred.Username, cred.Password)
                .build();
        }

        #endregion PROFILES

        #region OBJECTS

        public virtual IFileEndpoint CreateFileEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new FileEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IRegistrationEndpoint CreateRegistrationEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new RegistrationEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IAccountManagementEndpoint CreateAccountManagementEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new AccountManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IUserManagementEndpoint CreateUserManagementEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new UserManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IObjectManagementEndpoint CreateObjectManagementEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new ObjectManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IObjectInteractionEndpoint CreateObjectInteractionEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new ObjectInteractionEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IRelationshipManagementEndpoint CreateRelationshipManagementEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new RelationshipManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IGeospatialManagementEndpoint CreateGeospatialManagementEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new GeospatialManagementEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IHashTagEndpoint CreateHashTagEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new HashTagEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IMetadataEndpoint CreateMetadataEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new MetadataEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IObjectInteractionSessionEndpoint CreateObjectInteractionSessionEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new ObjectInteractionSessionEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual IDeviceEndpoint CreateDeviceEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new DeviceEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        public virtual INotificationEndpoint CreateNotificationEndpoint()
        {
            ICredential cred;
            if (!GetCredentials(SmartCosmosComponent.Objects, out cred))
                return null;

            return new NotificationEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(cred.Url)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(cred.Username, cred.Password)
            .build();
        }

        #endregion OBJECTS
    }
}