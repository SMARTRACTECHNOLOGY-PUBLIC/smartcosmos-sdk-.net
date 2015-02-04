using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;


namespace Smartrac.SmartCosmos.AccountManager.DataContext
{
    public interface IRoleDataContext : IBaseDataContext
    {
        string GetName();
    }
}
