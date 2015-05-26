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

namespace Smartrac.SmartCosmos.AccountManager.Role
{
    public enum RoleActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed

        /// <summary>
        /// item already exists
        /// </summary>
        //Conflict
    }

    public interface IRoleEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Creates a role within a directory.
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <param name="responseData">result</param>
        /// <returns>RoleActionResult</returns>
        RoleActionResult Create(RoleNameRequest name, out DefaultResponse responseData);

        /// <summary>
        /// Gets all roles from an application that belong to a single directory.
        /// </summary>
        /// <param name="responseData">result</param>
        /// <returns>RoleActionResult</returns>
        RoleActionResult Lookup(out RolesResponse responseData);

        /// <summary>
        /// Deletes a role within a directory.
        /// </summary>
        /// <param name="roleUrn"></param>
        /// <param name="responseData">result</param>
        /// <returns>RoleActionResult</returns>
        RoleActionResult Delete(Urn roleUrn, out DefaultResponse responseData);
    }
}