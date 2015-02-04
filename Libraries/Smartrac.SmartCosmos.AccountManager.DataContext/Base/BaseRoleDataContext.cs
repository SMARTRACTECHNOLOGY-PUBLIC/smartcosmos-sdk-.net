using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.AccountManager.DataContext
{
    public class BaseRoleDataContext : BaseDataContext, IRoleDataContext
    {
        public virtual string GetName()
        {
            return "";
        }
    }
}
