namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;

    public interface IRenovationService
    {
        RenovationRequest Create(RenovationRequest entity);
    }
}
