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

using Smartrac.Base;

namespace System
{
    /// <summary>
    /// implementes Copy extension method for all objects
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a datetime into a RFC3339 time string
        /// </summary>
        /// <param name="time">date time</param>
        /// <returns>RFC3339 time string</returns>
        public static string ToRfc3339DateTime(this DateTime time)
        {
            return Rfc3339DateTimeProvider.ToString(time);
        }

        /// <summary>
        /// Converts a RFC3339 time string into a date time
        /// </summary>
        /// <param name="time">RFC3339 time string</param>
        /// <returns>date time</returns>
        public static DateTime FromRfc3339DateTime(this string timeString)
        {
            return Rfc3339DateTimeProvider.Parse(timeString);
        }

        /// <summary>
        /// Converts a RFC3339 time string into a date time
        /// </summary>
        /// <param name="time">RFC3339 time string</param>
        /// <returns>date time</returns>
        public static bool FromRfc3339DateTime(this string timeString, out DateTime result)
        {
            return Rfc3339DateTimeProvider.TryParse(timeString, out result);
        }

        /// <summary>
        /// Converts a long into a unix timestamp e.g. 1412841162
        /// </summary>
        /// <param name="unixTimestamp">unix timestamp as long</param>
        /// <returns>date</returns>
        public static DateTime FromUnixTimestamp(this long unixTimestamp)
        {
            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round((double)(unixTimestamp / 1000)));
            return dtDateTime;
        }

        /// <summary>
        /// Converts a long into a java timestamp e.g. 1412841162123
        /// </summary>
        /// <param name="javaTimestamp">java timestamp as long</param>
        /// <returns>java timestamp as datetime</returns>
        public static DateTime FromJavaTimestamp(this long javaTimestamp)
        {
            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round((double)(javaTimestamp / 1000)));
            return dtDateTime;
        }

        /// <summary>
        /// Converts a date time into a unix timestamp e.g. 1412841162
        /// </summary>
        /// <param name="date">date tim</param>
        /// <returns>unix timestamp</returns>
        public static long ToUnixTimestamp(this DateTime date)
        {
            return Convert.ToInt64((date - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds);
        }
    }
}