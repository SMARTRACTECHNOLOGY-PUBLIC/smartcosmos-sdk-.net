﻿#region License
// SMART COSMOS Profiles SDK
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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.Logging
{
    public enum LogType
    {

        Debug, //     Debug log level.        
        Error, //     Error log level.        
        Fatal, //     Fatal log level.
        Info,  //     Info log level.
        Warning //    Warning log level.
    }

    public interface IMessageLogger
    {
        void AddLog(string aMessage, LogType aLogType = LogType.Debug);
    }
}
