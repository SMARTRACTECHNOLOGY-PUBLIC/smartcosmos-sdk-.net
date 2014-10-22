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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ArrayExtensions;
using System.Text;

namespace System
{
    /// <summary>
    /// implementes Copy extension method for all objects
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// FromUnixTimestamp
        /// </summary>
        /// <param name="unixTimestamp">e.g. 1412841162</param>
        /// <returns>date</returns>
        public static DateTime FromUnixTimestamp(long unixTimestamp)
        {
            /*
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            DateTime dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
             */

            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round( (double)(unixTimestamp / 1000) ));
            return dtDateTime;
        }

        public static DateTime FromJavaTimestamp(long javaTimestamp)
        {
            /*
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            DateTime dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
             */

            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round((double)(javaTimestamp / 1000)));
            return dtDateTime;
        }

        public static long ToUnixTimestamp(DateTime date)
        {
            return Convert.ToInt64( (date - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds);
            /*
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
             */
        }
    }
}
