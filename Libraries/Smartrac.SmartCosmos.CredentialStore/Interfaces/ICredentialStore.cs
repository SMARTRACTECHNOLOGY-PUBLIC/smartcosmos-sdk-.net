﻿#region License

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
namespace Smartrac.SmartCosmos.CredentialStore
{
    public enum SmartCosmosComponent
    {
        [DescriptionAttribute("SMART COSMOS Profiles")]
        Profiles = 1,

        [DescriptionAttribute("SMART COSMOS Objects")]
        Objects = 2,

        [DescriptionAttribute("SMART COSMOS Flows")]
        Flows = 3,

        [DescriptionAttribute("SMART COSMOS Account Manager")]
        AccountManager = 4
    }
    
    public interface ICredential
    {
        string Username { get; set; }
        string Password { get; set; }
        string Url { get; set; }
    }

    public interface ICredentialStore
    {
        ICredential GetCredentials(SmartCosmosComponent component);
        bool GetCredentials(SmartCosmosComponent component, out ICredential cred);
        bool ValidateCredentials(SmartCosmosComponent component);
    }
}