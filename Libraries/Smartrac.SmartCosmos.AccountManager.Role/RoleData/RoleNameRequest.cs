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
