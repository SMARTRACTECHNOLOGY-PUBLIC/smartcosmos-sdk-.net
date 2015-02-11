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
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System.IO;

namespace Smartrac.SmartCosmos.Objects.File
{
    public enum FileActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed,

        /// <summary>
        /// File exists and already has had content uploaded
        /// </summary>
        Conflict
    }

    public interface IFileEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Define the metadata of a file in preparation of an actual file upload
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>FileActionResult</returns>
        FileActionResult GetFileDefinition(FileDefinitionRequest requestData, out FileDefinitionResponse responseData);

        /// <summary>
        /// Upload actual file bytes to the cloud using an octet-stream
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>FileActionResult</returns>
        FileActionResult UploadFileAsOctetStream(Urn fileUrn, Stream data, out FileUploadResponse responseData);

        FileActionResult UploadFileAsOctetStream(Urn fileUrn, string file, out FileUploadResponse responseData);

        /// <summary>
        /// Upload a file stream as multi part form
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="data">File or memory stream</param>
        /// <param name="responseData">File upload response</param>
        /// <returns>FileActionResult</returns>
        FileActionResult UploadFileAsMultiPartForm(Urn fileUrn, Stream data, string fileName, out FileUploadResponse responseData);

        /// <summary>
        /// Retrieves the file definition
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="view">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">File properties</param>
        /// <returns>FileActionResult</returns>
        FileActionResult LookupDefinition(Urn fileUrn, out FileDefinitionRetrievalResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Retrieves the actual file content from the cloud
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <param name="responseData">FileContentRetrievalResponse</param>
        /// <returns>FileActionResult</returns>
        FileActionResult LookupContent(Urn fileUrn, out FileContentRetrievalResponse responseData);

        /// <summary>
        /// Retrieves the complete collection of file definitions associated with the specified entity
        /// </summary>
        /// <param name="entityReferenceType">Valid EntityReferenceType enum value</param>
        /// <param name="urn">Case-sensitive urn of an existing entity of type entityReferenceType</param>
        /// <param name="view">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">File properties</param>
        /// <returns>FileActionResult</returns>
        FileActionResult LookupDefinitions(EntityReferenceType entityReferenceType, Urn referenceUrn, out FileDefinitionRetrievalListResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Deletes an existing relationship by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">System-assigned URN assigned at creation of file definition</param>
        /// <returns>FileActionResult</returns>
        FileActionResult Delete(Urn fileUrn);
    }
}