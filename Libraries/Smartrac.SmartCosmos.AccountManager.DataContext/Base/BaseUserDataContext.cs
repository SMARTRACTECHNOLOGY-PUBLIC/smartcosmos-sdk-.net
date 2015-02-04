using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.AccountManager.DataContext
{
    public class BaseUserDataContext : BaseDataContext, IUserDataContext
    {
        public virtual Email GetEmail()
        {
            return new Email();
        }

        public virtual Urn GetRoleUrn()
        {
            return new Urn();
        }

        public virtual string GetSurname()
        {
            return "";
        }

        public virtual bool GetEnabled()
        {
            return true;
        }

        public virtual string GetGivenName()
        {
            return "";
        }
    }
}
