using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.AccountManager.DataContext
{
    public class EmptyRoleDataContext : BaseRoleDataContext
    {
        
        public string Name { get; set; }

        public override string GetName()
        {
            return Name;
        }
    }
}