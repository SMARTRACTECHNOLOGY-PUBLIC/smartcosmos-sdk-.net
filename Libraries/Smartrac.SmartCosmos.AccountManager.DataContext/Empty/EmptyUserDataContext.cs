using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.AccountManager.DataContext
{
    public class EmptyUserDataContext : BaseUserDataContext
    {
        public string GivenName { get; set; }

        public override string GetGivenName()
        {
            return GivenName;
        }

        public bool Enabled { get; set; }

        public override bool GetEnabled()
        {
            return Enabled;
        }

        public string Surname { get; set; }

        public override string GetSurname()
        {
            return Surname;
        }

        public Email Mail { get; set; }

        public override Email GetEmail()
        {
            return Mail;
        }

        public Urn RoleUrn { get; set; }

        public override Urn GetRoleUrn()
        {
            return RoleUrn;
        }
    }
}
