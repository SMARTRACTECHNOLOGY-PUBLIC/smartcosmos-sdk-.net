using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.Objects.Device
{
    /// <summary>
    /// Class for identification property for Lookup
    /// </summary>
    public class Identification
    {
        private string identification;

        /// <summary>
        /// Any API call that requires a {identification} parameter must provide string value
        /// </summary>
        public string Value
        {
            get
            {
                return identification;
            }
        }

        public Identification(string identification)
            : base()
        {
            this.identification = identification;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(identification) &&
                this.identification.Length <= 255;
        }

        public Identification()
        {
        }
    }
}
