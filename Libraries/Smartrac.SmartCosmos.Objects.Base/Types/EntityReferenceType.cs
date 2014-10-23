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

namespace Smartrac.SmartCosmos.Objects.Base
{
    /// <summary>
    /// An Entity Reference Type is used to provide context to a type-agnositc call, such as a search operation.
    /// </summary>
    public enum EntityReferenceType
    {
        [DescriptionAttribute("Account")]
        Account = 1,

        [DescriptionAttribute("Extension")]
        Extension = 2,

        [DescriptionAttribute("NotificationEndpoint")]
        NotificationEndpoint = 3,

        [DescriptionAttribute("Object")]
        Object = 4,

        [DescriptionAttribute("Relationship")]
        Relationship = 5,

        [DescriptionAttribute("ObjectInteraction")]
        ObjectInteraction = 6,

        [DescriptionAttribute("ObjectInteractionSession")]
        ObjectInteractionSession = 7,

        [DescriptionAttribute("Device")]
        Device = 8,

        [DescriptionAttribute("ObjectAddress")]
        ObjectAddress = 9,

        [DescriptionAttribute("File")]
        File = 10,

        [DescriptionAttribute("Metadata")]
        Metadata = 11,

        [DescriptionAttribute("Tag")]
        Tag = 12,

        [DescriptionAttribute("Timeline")]
        Timeline = 13,

        [DescriptionAttribute("Georectification")]
        Georectification = 14
    }
}