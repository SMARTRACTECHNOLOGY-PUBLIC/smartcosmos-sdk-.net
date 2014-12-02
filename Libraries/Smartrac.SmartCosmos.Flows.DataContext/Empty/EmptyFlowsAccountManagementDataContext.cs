using Smartrac.SmartCosmos.Flows.Base;

namespace Smartrac.SmartCosmos.Flows.DataContext
{
    public class EmptyFlowsAccountManagementDataContext : BaseFlowsAccountManagementDataContext
    {
        public string eMailAddress { get; set; }        

        public override string GeteMailAddress()
        {
            return eMailAddress;
        }
    }
}