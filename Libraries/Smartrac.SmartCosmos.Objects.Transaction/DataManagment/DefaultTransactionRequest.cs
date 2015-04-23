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

    public class ObjectsMetadataBase
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }
        public string referenceUrn { get; set; }
        [JsonProperty("dataType")]
        public string dataTypeString
        {
            get
            {
                return dataType.GetDescription();
            }

            set
            {
                dataType = MetadataDataType.Boolean.GetFlagByDescription(value);
            }
        }

        [JsonIgnore]
        public MetadataDataType dataType { get; set; }
        public string key { get; set; }
        public bool alreadyEncoded { get; set; }

        public bool ShouldSerializealreadyEncoded() { return alreadyEncoded; } // default = false

        public ObjectsMetadataBase()
        {
            entityReferenceType = EntityReferenceType.Object;
            dataType = MetadataDataType.String;
            alreadyEncoded = false;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(referenceUrn) &&
                //!String.IsNullOrEmpty(dataType) &&
                !String.IsNullOrEmpty(key) &&
                (this.GetType().Name != "ObjectsMetadataBase");
            //&&
            //!String.IsNullOrEmpty(value);
        }
    }

    public class ObjectsMetadataBoolean : ObjectsMetadataBase
    {
        public bool value { get; set; }

        public ObjectsMetadataBoolean()
            : base()
        {
            dataType = MetadataDataType.Boolean;
            value = true;
        }
    }

    public class ObjectsMetadataLong : ObjectsMetadataBase
    {
        public long value { get; set; }

        public ObjectsMetadataLong()
            : base()
        {
            dataType = MetadataDataType.Long;
            value = 0;
        }
    }

    public class ObjectsMetadataDouble : ObjectsMetadataBase
    {
        public double value { get; set; }

        public ObjectsMetadataDouble()
            : base()
        {
            dataType = MetadataDataType.Double;
            value = 0;
        }
    }

    public class ObjectsMetadataInteger : ObjectsMetadataBase
    {
        public int value { get; set; }

        public ObjectsMetadataInteger()
            : base()
        {
            dataType = MetadataDataType.Integer;
            value = 0;
        }
    }

    public class ObjectsMetadataFloat : ObjectsMetadataBase
    {
        public float value { get; set; }

        public ObjectsMetadataFloat()
        {
            dataType = MetadataDataType.Float;
            value = 0;
        }
    }

    public class ObjectsMetadataDate : ObjectsMetadataString
    {
        public ObjectsMetadataDate()
            : base()
        {
            dataType = MetadataDataType.Date;
        }
        /*
        [JsonIgnore]
        public DateTime valueJavaDate
        {
            get
            {
                return Smartrac.Base.DateTimeExtensions.FromJavaTimestamp(value);
            }
            set
            {
                this.value = Smartrac.Base.DateTimeExtensions.ToJavaTimestamp(value);
            }
        }
         */
    }

    public class ObjectsMetadataString : ObjectsMetadataBase
    {
        public string value { get; set; }

        public ObjectsMetadataString()
            : base()
        {
            dataType = MetadataDataType.String;
        }

        public override bool IsValid()
        {
            return base.IsValid()
                && !String.IsNullOrEmpty(value);
        }
    }

    public class ObjectsMetadataCustom : ObjectsMetadataString
    {
        public ObjectsMetadataCustom()
            : base()
        {
            dataType = MetadataDataType.Custom;
        }
    }

    public class ObjectsMetadataJSON : ObjectsMetadataString
    {
        public ObjectsMetadataJSON()
        {
            dataType = MetadataDataType.JSON;
        }
    }

    public class ObjectsMetadataXML : ObjectsMetadataString
    {
        public ObjectsMetadataXML()
        {
            dataType = MetadataDataType.XML;
        }
    }



    public class TransactionItem
    {
        public ObjectsAccount account { get; set; }
        public List<ObjectsDevice> devices { get; set; }
        public List<ObjectsObject> objects { get; set; }
        public List<ObjectsObjectAddress> objectAddresses { get; set; }
        public List<ObjectsRelationship> relationships { get; set; }
        public List<ObjectsFile> files { get; set; }
        public List<ObjectsMetadataBase> metadata { get; set; }

        public bool ShouldSerializeaccount() { return ((account != null) && (account.name != "")); }
        public bool ShouldSerializedevices() { return ((devices != null) && (devices.Count > 0)); }
        public bool ShouldSerializeobjects() { return ((objects != null) && (objects.Count > 0)); }
        public bool ShouldSerializeobjectaddresses() { return ((objectAddresses != null) && (objectAddresses.Count > 0)); }
        public bool ShouldSerializerelationships() { return ((relationships != null) && (relationships.Count > 0)); }
        public bool ShouldSerializefiles() { return ((files != null) && (files.Count > 0)); }
        public bool ShouldSerializemetadata() { return ((metadata != null) && (metadata.Count > 0)); }

        public TransactionItem()
            : base()
        {
            devices = new List<ObjectsDevice>();
            objects = new List<ObjectsObject>();
            objectAddresses = new List<ObjectsObjectAddress>();
            relationships = new List<ObjectsRelationship>();
            files = new List<ObjectsFile>();
            metadata = new List<ObjectsMetadataBase>();
        }

        public bool IsValid()
        {
            return ((account == null) || account.IsValid())
                && ((devices.Count == 0) || (devices.TrueForAll(i => i.IsValid())))
                && ((objects.Count == 0) || (objects.TrueForAll(i => i.IsValid())))
                && ((objectAddresses.Count == 0) || (objectAddresses.TrueForAll(i => i.IsValid())))
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

        public ObjectsObjectAddress AddObjectAddress(string objectUrn, string type, string line1, string postalCode, string city, string countryAbbreviation)
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
            objectAddresses.Add(obj);
            return obj;
        }

        public ObjectsMetadataBase AddMetaDataAsString(EntityReferenceType entityReferenceType, string referenceUrn, string key, string value,
            MetadataDataType dataType = MetadataDataType.String,
            bool alreadyEncoded = false)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            if (dataType.HasFlag(MetadataDataType.String | MetadataDataType.XML | MetadataDataType.Custom | MetadataDataType.Date))
                throw new MissingFieldException("invalid dataType " + dataType.GetDescription());

            ObjectsMetadataString obj = new ObjectsMetadataString
            {
                entityReferenceType = Objects.Base.EntityReferenceType.Object,
                referenceUrn = referenceUrn,
                dataType = dataType,
                key = key,
                value = value,
                alreadyEncoded = alreadyEncoded
            };
            metadata.Add(obj);
            return obj;
        }

        public ObjectsMetadataBase AddMetaDataAsDouble(EntityReferenceType entityReferenceType, string referenceUrn, string key, double value,
            bool alreadyEncoded = false)
        {
            ObjectsMetadataDouble obj = new ObjectsMetadataDouble
            {
                entityReferenceType = Objects.Base.EntityReferenceType.Object,
                referenceUrn = referenceUrn,
                key = key,
                value = value,
                alreadyEncoded = alreadyEncoded
            };
            metadata.Add(obj);
            return obj;
        }

        public ObjectsMetadataBase AddMetaDataAsFloat(EntityReferenceType entityReferenceType, string referenceUrn, string key, float value,
            bool alreadyEncoded = false)
        {
            ObjectsMetadataFloat obj = new ObjectsMetadataFloat
            {
                entityReferenceType = Objects.Base.EntityReferenceType.Object,
                referenceUrn = referenceUrn,
                key = key,
                value = value,
                alreadyEncoded = alreadyEncoded
            };
            metadata.Add(obj);
            return obj;
        }

        public ObjectsMetadataBase AddMetaDataAsLong(EntityReferenceType entityReferenceType, string referenceUrn, string key, long value,
            bool alreadyEncoded = false)
        {
            ObjectsMetadataLong obj = new ObjectsMetadataLong
            {
                entityReferenceType = Objects.Base.EntityReferenceType.Object,
                referenceUrn = referenceUrn,
                key = key,
                value = value,
                alreadyEncoded = alreadyEncoded
            };
            metadata.Add(obj);
            return obj;
        }

        public ObjectsMetadataBase AddMetaDataAsInteger(EntityReferenceType entityReferenceType, string referenceUrn, string key, int value,
            bool alreadyEncoded = false)
        {
            ObjectsMetadataInteger obj = new ObjectsMetadataInteger
            {
                entityReferenceType = Objects.Base.EntityReferenceType.Object,
                referenceUrn = referenceUrn,
                key = key,
                value = value,
                alreadyEncoded = alreadyEncoded
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