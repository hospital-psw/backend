namespace IntegrationLibrary.BloodBank
{
    using System.Collections.Generic;

    public interface IBloodBankService
    {
        IEnumerable<BloodBank> GetAll();
        BloodBank Get(int id);
        BloodBank Create(BloodBank bloodBank);
        BloodBank Update(BloodBank bloodBank);
        bool Delete(int id);
    }
}
