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

using System;

namespace Smartrac.SmartCosmos.TestCase.Base
{
    [Flags]
    public enum SmartCosmosService
    {
        Profiles = 1,
        Objects = 2,
        Flows = 4,
        AccountManager = 8,
        All = Profiles | Objects | Flows | AccountManager
    }

    /// <summary>
    /// Test case attribute to mark valid SMART COSMOS test cases
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : System.Attribute
    {
        public readonly SmartCosmosService SmartCosmosService;

        public ServiceAttribute(SmartCosmosService smartCosmosService)
            : base()
        {
            this.SmartCosmosService = smartCosmosService;
        }
    }
}