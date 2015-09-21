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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.Metadata;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.Objects.Transaction
{
    public interface ITransactionRequest : IBaseRequest
    {
        /// <summary>
        /// Handler identification, e.g. DefaultJsonHandler
        /// </summary>
        /// <returns>Handler identification</returns>
        string GetHandlerName();

        /// <summary>
        /// JSON object
        /// </summary>
        /// <returns>JSON object</returns>
        object GetTransactionRequest();

        /// <summary>
        /// Add a transaction item
        /// </summary>
        /// <returns>TransactionItem</returns>
        ITransactionItem AddTransaction();

        /// <summary>
        /// List of transaction items
        /// </summary>
        /// <returns></returns>
        List<ITransactionItem> Items();
    }

    public interface ITransactionItem : IBaseRequest
    {
        IObjectsAccount account { get; set; }

        List<IObjectsDevice> devices { get; set; }

        List<IObjectsObject> objects { get; set; }

        List<IObjectsObjectAddress> objectAddresses { get; set; }

        List<IObjectsRelationship> relationships { get; set; }

        List<IObjectsFile> files { get; set; }

        List<IObjectsMetadataBase> metadata { get; set; }

        IObjectsObject FindObject(string objectUrn);

        IObjectsObject AddObject(string objectUrn, string type, string name);

        IObjectsRelationship AddRelationship(EntityReferenceType entityReferenceType, string referenceUrn, string type,
            EntityReferenceType relatedEntityReferenceType, string relatedReferenceUrn);

        IObjectsObjectAddress AddObjectAddress(string objectUrn, string type, string line1, string postalCode, string city, string countryAbbreviation);

        IObjectsMetadataBase AddMetaDataAsString(EntityReferenceType entityReferenceType, string referenceUrn, string key, string value,
            MetadataDataType dataType = MetadataDataType.String);

        IObjectsMetadataBase AddMetaDataAsDouble(EntityReferenceType entityReferenceType, string referenceUrn, string key, double value);

        IObjectsMetadataBase AddMetaDataAsFloat(EntityReferenceType entityReferenceType, string referenceUrn, string key, float value);

        IObjectsMetadataBase AddMetaDataAsLong(EntityReferenceType entityReferenceType, string referenceUrn, string key, long value);
        
        IObjectsMetadataBase AddMetaDataAsInteger(EntityReferenceType entityReferenceType, string referenceUrn, string key, int value);

        IObjectsAccount SetAccount(string Id, string name);
    }

    public interface IObjectsAccount : IBaseRequest
    {
        string name { get; set; }

        string moniker { get; set; }

        string description { get; set; }

        bool activeFlag { get; set; }
    }

    public interface IObjectsDevice : IBaseRequest
    {
        string identification { get; set; }

        string name { get; set; }

        string type { get; set; }

        string description { get; set; }

        bool activeFlag { get; set; }

        string moniker { get; set; }
    }

    public interface IObjectsObject : IBaseRequest
    {
        string objectUrn { get; set; }

        string type { get; set; }

        string name { get; set; }

        string description { get; set; }

        bool activeFlag { get; set; }

        string moniker { get; set; }
    }

    public interface IObjectsObjectAddress : IBaseRequest
    {
        string objectUrn { get; set; }

        string type { get; set; }

        string line1 { get; set; }

        string line2 { get; set; }

        string city { get; set; }

        long timestamp { get; set; }

        string stateProvince { get; set; }

        string postalCode { get; set; }

        string countryAbbreviation { get; set; }
    }

    public interface IObjectsRelationship : IBaseRequest
    {
        EntityReferenceType entityReferenceType { get; set; }

        string referenceUrn { get; set; }

        string type { get; set; }

        EntityReferenceType relatedEntityReferenceType { get; set; }

        string relatedReferenceUrn { get; set; }

        string moniker { get; set; }
    }

    public interface IObjectsFile : IBaseRequest
    {
        EntityReferenceType entityReferenceType { get; set; }

        string referenceUrn { get; set; }

        string mimeType { get; set; }

        long timestamp { get; set; }

        string contentUrl { get; set; }

        string octedStream { get; set; }
    }


    public interface IObjectsMetadataBase : IBaseRequest
    {
        EntityReferenceType entityReferenceType { get; set; }

        string referenceUrn { get; set; }

        MetadataDataType dataType { get; set; }

        string key { get; set; }
    }
}