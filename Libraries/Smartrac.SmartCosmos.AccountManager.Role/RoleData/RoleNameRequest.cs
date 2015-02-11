﻿#region License

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartrac.SmartCosmos.AccountManager.Role
{
    public class RoleNameRequest
    {
        private string name_;
        
        public string Name
        {
            get
            {
                return name_;
            }
        }

        public RoleNameRequest(string name)
            : base()
        {
            this.name_ = name;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(Name);
        }

        public RoleNameRequest()
        {
        }
    }
}
