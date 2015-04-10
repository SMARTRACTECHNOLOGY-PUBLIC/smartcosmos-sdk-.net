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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.Metadata;
using System;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.Objects.Transaction
{
    public class ObjectsAccount
    {
        public string name { get; set; }

        public string moniker { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public bool ShouldSerializedescription()
        {
            return !String.IsNullOrEmpty(description);
        }

        public bool ShouldSerializemoniker()
        {
            return !String.IsNullOrEmpty(moniker);
        }

        public bool ShouldSerializeactiveFlag()
        {
            return activeFlag != true;
        }

        public ObjectsAccount()
        {
            activeFlag = true;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(name);
        }
    }

    public class ObjectsDevice
    {
        public string identification { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public string moniker { get; set; }

        public bool ShouldSerializedescription()
        {
            return !String.IsNullOrEmpty(description);
        }

        public bool ShouldSerializemoniker()
        {
            return !String.IsNullOrEmpty(moniker);
        }

        public bool ShouldSerializeactiveFlag()
        {
            return activeFlag != true;
        }

        public ObjectsDevice()
        {
            activeFlag = true;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(identification) &&
                !String.IsNullOrEmpty(name) &&
                !String.IsNullOrEmpty(type);
        }
    }

    public class ObjectsObject
    {
        public string objectUrn { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public string moniker { get; set; }

        public bool ShouldSerializedescription()
        {
            return !String.IsNullOrEmpty(description);
        }

        public bool ShouldSerializemoniker()
        {
            return !String.IsNullOrEmpty(moniker);
        }

        public bool ShouldSerializeactiveFlag()
        {
            return activeFlag != true;
        }

        public ObjectsObject()
        {
            activeFlag = true;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(objectUrn) &&
                !String.IsNullOrEmpty(name) &&
                !String.IsNullOrEmpty(type);
        }
    }

    public class ObjectsObjectAddress
    {
        public string objectUrn { get; set; }

        public string type { get; set; }

        public string line1 { get; set; }

        public string line2 { get; set; }

        public string city { get; set; }

        public long timestamp { get; set; }

        public string stateProvince { get; set; }

        public string postalCode { get; set; }

        public string countryAbbreviation { get; set; }

        public bool ShouldSerializeline1()
        {
            return !String.IsNullOrEmpty(line1);
        }

        public bool ShouldSerializeline2()
        {
            return !String.IsNullOrEmpty(line2);
        }

        public bool ShouldSerializetimestamp()
        {
            return timestamp > 0;
        }

        public bool ShouldSerializepostalCode()
        {
            return !String.IsNullOrEmpty(postalCode);
        }

        public bool ShouldSerializecountryAbbreviation()
        {
            return !String.IsNullOrEmpty(countryAbbreviation);
        }

        public bool ShouldSerializestateProvince()
        {
            return !String.IsNullOrEmpty(stateProvince);
        }

        public bool ShouldSerializecity()
        {
            return !String.IsNullOrEmpty(city);
        }

        public ObjectsObjectAddress()
        {
            timestamp = 0;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(objectUrn) &&
                !String.IsNullOrEmpty(type);
        }
    }

    public class ObjectsRelationship
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        public string referenceUrn { get; set; }

        public string type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType relatedEntityReferenceType { get; set; }

        public string relatedReferenceUrn { get; set; }

        public string moniker { get; set; }

        public bool ShouldSerializemoniker()
        {
            return !String.IsNullOrEmpty(moniker);
        }

        public ObjectsRelationship()
        {
            entityReferenceType = EntityReferenceType.Object;
            relatedEntityReferenceType = EntityReferenceType.Object;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(referenceUrn) &&
                !String.IsNullOrEmpty(type) &&
                !String.IsNullOrEmpty(relatedReferenceUrn);
        }
    }

    public class ObjectsFile
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        public string referenceUrn { get; set; }

        public string mimeType { get; set; }

        public long timestamp { get; set; }

        public string contentUrl { get; set; }

        public string octedStream { get; set; }

        public ObjectsFile()
        {
            entityReferenceType = EntityReferenceType.Object;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(referenceUrn) &&
                !String.IsNullOrEmpty(mimeType);
        }
    }

    public class ObjectsMetadata
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        public string referenceUrn { get; set; }

        public string dataType { get; set; }

        public string key { get; set; }

        public string decodedValue { get; set; }

        public ObjectsMetadata()
        {
            entityReferenceType = EntityReferenceType.Object;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(referenceUrn) &&
                !String.IsNullOrEmpty(dataType) &&
                !String.IsNullOrEmpty(key) &&
                !String.IsNullOrEmpty(decodedValue);
        }
    }

    public class TransactionItem
    {
        public ObjectsAccount account { get; set; }

        public List<ObjectsDevice> devices { get; set; }

        public List<ObjectsObject> objects { get; set; }

        public List<ObjectsObjectAddress> objectaddresses { get; set; }

        public List<ObjectsRelationship> relationships { get; set; }

        public List<ObjectsFile> files { get; set; }

        public List<ObjectsMetadata> metadata { get; set; }

        public bool ShouldSerializeaccount()
        {
            return ((account != null) && (account.name != ""));
        }

        public bool ShouldSerializedevices()
        {
            return ((devices != null) && (devices.Count > 0));
        }

        public bool ShouldSerializeobjects()
        {
            return ((objects != null) && (objects.Count > 0));
        }

        public bool ShouldSerializeobjectaddresses()
        {
            return ((objectaddresses != null) && (objectaddresses.Count > 0));
        }

        public bool ShouldSerializerelationships()
        {
            return ((relationships != null) && (relationships.Count > 0));
        }

        public bool ShouldSerializefiles()
        {
            return ((files != null) && (files.Count > 0));
        }

        public bool ShouldSerializemetadata()
        {
            return ((metadata != null) && (metadata.Count > 0));
        }

        public TransactionItem()
            : base()
        {
            devices = new List<ObjectsDevice>();
            objects = new List<ObjectsObject>();
            objectaddresses = new List<ObjectsObjectAddress>();
            relationships = new List<ObjectsRelationship>();
            files = new List<ObjectsFile>();
            metadata = new List<ObjectsMetadata>();
        }

        public bool IsValid()
        {
            return ((account == null) || account.IsValid())
                && ((devices.Count == 0) || (devices.TrueForAll(i => i.IsValid())))
                && ((objects.Count == 0) || (objects.TrueForAll(i => i.IsValid())))
                && ((objectaddresses.Count == 0) || (objectaddresses.TrueForAll(i => i.IsValid())))
                && ((relationships.Count == 0) || (relationships.TrueForAll(i => i.IsValid())))
                && ((files.Count == 0) || (files.TrueForAll(i => i.IsValid())))
                && ((metadata.Count == 0) || (metadata.TrueForAll(i => i.IsValid())));
        }

        public ObjectsObject AddObject(string objectUrn, string type, string name)
        {
            ObjectsObject obj = new ObjectsObject
            {
                objectUrn = objectUrn,
                type = type,
                name = name
            };
            objects.Add(obj);
            return obj;
        }

        public ObjectsRelationship AddRelationship(EntityReferenceType entityReferenceType, string referenceUrn, string type,
            EntityReferenceType relatedEntityReferenceType, string relatedReferenceUrn)
        {
            ObjectsRelationship obj = new ObjectsRelationship
            {
                entityReferenceType = entityReferenceType,
                referenceUrn = referenceUrn,
                type = type,
                relatedEntityReferenceType = relatedEntityReferenceType,
                relatedReferenceUrn = relatedReferenceUrn
            };
            relationships.Add(obj);
            return obj;
        }

        public ObjectsObjectAddress AddObjectaddress(string objectUrn, string type, string line1, string postalCode, string city, string countryAbbreviation)
        {
            ObjectsObjectAddress obj = new ObjectsObjectAddress
            {
                objectUrn = objectUrn,
                type = type,
                line1 = line1,
                postalCode = postalCode,
                countryAbbreviation = countryAbbreviation,
                city = city
            };
            objectaddresses.Add(obj);
            return obj;
        }

        public ObjectsMetadata AddMetaData(EntityReferenceType entityReferenceType, string referenceUrn, MetadataDataType dataType, string key, string decodedValue)
        {
            if (String.IsNullOrEmpty(decodedValue))
                return null;

            ObjectsMetadata obj = new ObjectsMetadata
            {
                entityReferenceType = Objects.Base.EntityReferenceType.Object,
                referenceUrn = referenceUrn,
                dataType = dataType.GetDescription(),
                key = key,
                decodedValue = decodedValue
            };
            metadata.Add(obj);
            return obj;
        }

        public ObjectsAccount SetAccount(string Id, string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                account = new ObjectsAccount
                {
                    name = name,
                    moniker = Id
                };
            }
            return account;
        }
    }

    public class DefaultTransactionRequest : List<TransactionItem>, ITransactionRequest
    {
        public bool IsValid()
        {
            return TrueForAll(i => i.IsValid());
        }

        public string GetHandler()
        {
            return "DefaultJsonHandler";
        }

        public object GetTransactionRequest()
        {
            return this;
        }
    }
}