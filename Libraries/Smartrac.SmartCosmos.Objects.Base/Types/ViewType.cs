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

using System.ComponentModel;

namespace Smartrac.SmartCosmos.Objects.Base
{
    /// <summary>
    /// Every GET endpoint allows the developer to control the verbosity of the JSON response.
    /// Generally, this fact is encapsulated and of little concern to most developers.
    /// However, it is possible in rare situations to observe the same identical object, as indicated by its URN key,
    /// serialized with more (or less) fields. The serialization levels supported by the platform include:
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// All Published fields, plus highly pertinent fields
        /// </summary>
        [DescriptionAttribute("Minimum")]
        Minimum = 0,

        /// <summary>
        ///  [**default**] All Minimum fields, plus additional contextual fields
        /// </summary>
        [DescriptionAttribute("Standard")]
        Standard = 1,

        /// <summary>
        /// All Standard fields, plus fields rarely referenced
        /// </summary>
        [DescriptionAttribute("Full")]
        Full = 2,

        /// <summary>
        /// Publicly accessible fields (specific to the Extension Framework)
        /// </summary>
        [DescriptionAttribute("Published")]
        Published = 3
    }
}