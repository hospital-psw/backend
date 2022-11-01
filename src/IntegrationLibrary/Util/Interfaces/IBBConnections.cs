namespace IntegrationLibrary.Util.Interfaces
{
    using IntegrationLibrary.BloodBank;

    public interface IBBConnections
    {
        public bool SendHttpRequestToBank(BloodBank bloodBank, string type);
    }

}
