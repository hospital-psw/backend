namespace IntegrationLibrary.Util.Interfaces
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.UrgentBloodTransfer;

    public interface IBBConnections
    {
        public bool SendHttpRequestToBank(BloodBank bloodBank, string type);
        public void SendBloodUnitToHospital(BloodUnit unit);
    }

}
