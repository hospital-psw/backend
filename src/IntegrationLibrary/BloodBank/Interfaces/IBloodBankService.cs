namespace IntegrationLibrary.BloodBank.Interfaces
{
    using IntegrationLibrary.Core;

    public interface IBloodBankService : IService<BloodBank>
    {
        BloodBank Register(BloodBank entity);
        bool CheckBloodType(int id, string type);
    }
}
