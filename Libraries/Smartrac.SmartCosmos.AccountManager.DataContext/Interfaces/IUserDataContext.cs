using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;


namespace Smartrac.SmartCosmos.AccountManager.DataContext
{
    public interface IUserDataContext : IBaseDataContext
    {
        Urn GetRoleUrn();

        Email GetEmail();

        string GetSurname();

        string GetGivenName();
            
        bool GetEnabled();
    }
}
