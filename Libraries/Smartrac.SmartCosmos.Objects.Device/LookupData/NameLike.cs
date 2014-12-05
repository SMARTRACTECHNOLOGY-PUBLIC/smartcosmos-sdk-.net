using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.Objects.Device{
    
    /// <summary>
    /// Class for nameLike property for Lookup
    /// </summary>
    public class Name
    {
        private string nameLike;

        /// <summary>
        /// Any API call that requires a {nameLike} parameter must provide string value
        /// </summary>
        public string NameLike
        {
            get
            {
                return nameLike;
            }
        }

        public Name(string nameLike)
            : base()
        {
            this.nameLike = nameLike;
        }

        public virtual bool IsValid()
        {
            return !String.IsNullOrEmpty(nameLike) &&
                nameLike.Length <= 255;
        }

        public Name()
        {
        }
    }
}
