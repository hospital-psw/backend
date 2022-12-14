namespace IntegrationLibrary.Util.Interfaces
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.UrgentBloodTransfer.Model;

    public interface IBBConnections
    {
        public bool SendHttpRequestToBank(BloodBank bloodBank, string type);
        public bool SendHttpRequestAmountToBank(BloodBank bloodBank, string type, double amount);
        public void SendBloodUnitToHospital(BloodUnit unit);


    }

}
