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

using System.Web;

namespace System
{
    public static class HttpExtensions
    {
        /// <summary>
        /// Add query to URL string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri AddQuery(this Uri uri, string name, long? value)
        {
            if (value != null && value.HasValue)
                return AddQuery(uri, name, value.Value.ToString());
            else
                return uri;
        }

        /// <summary>
        /// Add query to URL string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            if (String.IsNullOrEmpty(name) ||
                String.IsNullOrEmpty(value))
            {
                return uri;
            }

            string query = uri.OriginalString;
            query += query.Contains("?") ? "&" : "?";
            query += name + "=" + HttpUtility.UrlEncode(value);

            return new Uri(query, uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
        }

        /// <summary>
        /// Add subfolder to URL string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="subfolder"></param>
        /// <returns></returns>
        public static Uri AddSubfolder(this Uri uri, string subfolder)
        {
            if (String.IsNullOrEmpty(subfolder))
            {
                return uri;
            }

            string query = uri.OriginalString;
            query += query.EndsWith("/") ? HttpUtility.UrlEncode(subfolder) : "/" + HttpUtility.UrlEncode(subfolder);

            return new Uri(query, uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
        }
    }
}