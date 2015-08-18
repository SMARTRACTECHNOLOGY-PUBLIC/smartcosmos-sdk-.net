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
using System.ComponentModel;

namespace Smartrac.SmartCosmos.Objects.Transaction
{
    public enum TransactionDataType
    {
        [Description("StringType")]
        String,

        [Description("DateType")]
        Date,

        [Description("IntegerType")]
        Integer,

        [Description("LongType")]
        Long,

        [Description("BooleanType")]
        Boolean,

        [Description("FloatType")]
        Float,

        [Description("DoubleType")]
        Double,

        [Description("JSONType")]
        JSON,

        [Description("XMLType")]
        XML,

        [Description("Custom")]
        Custom
    }

    public enum TransactionActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed
    }

    public interface ITransactionEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Import data
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>TransactionActionResult</returns>
        TransactionActionResult Import(ITransactionRequest requestData, out ImportTransactionResponse responseData, int timeout = 20 * 60 * 1000);

        /// <summary>
        /// Create default request
        /// </summary>
        /// <returns>ITransactionRequest</returns>
        ITransactionRequest CreateDefaultTransaction();
    }
}