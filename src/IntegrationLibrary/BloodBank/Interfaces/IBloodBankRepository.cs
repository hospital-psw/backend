using IntegrationLibrary.Core;

namespace IntegrationLibrary.BloodBank.Interfaces
{
    public interface IBloodBankRepository : IRepository<BloodBank>
    {
        BloodBank GetByEmail(string email);
    }
}
