namespace IntegrationLibrary.Core.Service.Core
{
    using IntegrationLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IBloodBankService
    {
        IEnumerable<BloodBank> GetAll();
        BloodBank Get(int id);
        BloodBank Create(BloodBank bloodBank);
        BloodBank Update(BloodBank bloodBank);
        bool Delete(int id);

        bool CheckBloodType(int id,string type);
    }
}
