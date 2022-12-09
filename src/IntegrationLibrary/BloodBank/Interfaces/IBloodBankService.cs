namespace IntegrationLibrary.BloodBank.Interfaces
{
    using IntegrationLibrary.Core;
    using System;

    public interface IBloodBankService : IService<BloodBank>
    {
        BloodBank Register(BloodBank entity);
        bool CheckBloodType(int id, string type);
        BloodBank GetByEmail(string email);
        bool CheckBloodAmount(int id, string type, double amount);
        BloodBank SaveConfiguration(int id, int frequntly, DateTime reportFrom, DateTime reportTo);
        BloodBank SaveMonthlyTransferConfiguration(int id, MonthlyTransfer mt);
    }
}
