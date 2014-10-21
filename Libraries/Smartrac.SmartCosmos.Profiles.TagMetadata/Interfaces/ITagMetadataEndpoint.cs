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

using System.ComponentModel;
using Smartrac.SmartCosmos.ClientEndpoint.Base;

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
{
    public enum TagMetaDataActionResult
    {
        /// <summary>
        /// Minimum 1 tag found and result available
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

    public enum TagPropertyString
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        [DescriptionAttribute("custId")]
        customerId,

        /// <summary>
        /// Order ID
        /// </summary>
        [DescriptionAttribute("orderId")]
        orderId,

        /// <summary>
        /// Order quantity unit
        /// </summary>
        [DescriptionAttribute("orderQtyU")]
        orderQuantityUnit,

        /// <summary>
        /// Customer purchase order number
        /// </summary>
        [DescriptionAttribute("customerPO")]
        customerPurchaseOrder,

        /// <summary>
        /// Supplier purchase order number
        /// </summary>
        [DescriptionAttribute("supplPO")]
        supplierPurchaseOrder,

        /// <summary>
        /// Delivery ID
        /// </summary>
        [DescriptionAttribute("delivId")]
        deliveryId,

        /// <summary>
        /// Roll number / batch ID
        /// </summary>
        [DescriptionAttribute("batchId")]
        batchId,

        /// <summary>
        /// Chip manufacturer
        /// </summary>
        //[DescriptionAttribute("custId")]
        //chipManufacturer = "chipManuf",

        /// <summary>
        /// Sub roll number / sub batch ID
        /// </summary>
        [DescriptionAttribute("subRoll")]
        subRoll,

        /// <summary>
        /// Manufacturer production side ID
        /// </summary>
        [DescriptionAttribute("plantId")]
        plantId,

        /// <summary>
        /// Inlay type
        /// </summary>
        [DescriptionAttribute("inlayType")]
        inlayType,

        /// <summary>
        /// Delivery quantity unit
        /// </summary>
        [DescriptionAttribute("delivQtyU")]
        deliveryQuantityUnit
    }

    public enum TagPropertyLong
    {
        /// <summary>
        /// Order date
        /// </summary>
        [DescriptionAttribute("orderDate")]
        orderDate,

        /// <summary>
        /// Delivery date
        /// </summary>
        [DescriptionAttribute("delivDate")]
        delivDate,

        /// <summary>
        /// Inlay manufacturer date
        /// </summary>
        [DescriptionAttribute("inlayManufDate")]
        inlayManufacturerDate
    }

    public enum TagPropertyNumber
    {
        /// <summary>
        /// Order quantity
        /// </summary>
        [DescriptionAttribute("orderQty")]
        orderQuantity,

        /// <summary>
        /// Delivery quantity
        /// </summary>
        [DescriptionAttribute("delivQty")]
        deliveryQuantity,

        /// <summary>
        /// Batch/Roll yield [%]
        /// </summary>
        [DescriptionAttribute("yield")]
        yield,

        /// <summary>
        /// Attenuation in dB
        /// </summary>
        [DescriptionAttribute("attenuation")]
        attenuation,

        /// <summary>
        /// Check state of tag 0=failed; 1=passed
        /// </summary>
        [DescriptionAttribute("checkState")]
        checkState
    }

    public interface ITagMetadataEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Get tag related data
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagMetaDataActionResult</returns>
        TagMetaDataActionResult GetTagMetadata(TagMetaDataRequest requestData, out TagMetaDataResponse responseData);

        /// <summary>
        /// Get a message to a single tag code
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagMetaDataActionResult</returns>
        TagMetaDataActionResult GetTagMessage(TagMessageRequest requestData, out TagMessageResponse responseData);
    }
}