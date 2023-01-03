namespace HospitalLibrary.Core.Service.Examinations.Core
{
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPrescriptionService
    {
        Prescription Get(int id);

        Prescription Add(int medicamentId, string description, DateRange dateRange);

        IEnumerable<Prescription> GetAll();

        List<Prescription> AddMultiple(List<NewPrescriptionDto> dtos);
        IEnumerable<Prescription> GetPrescriptionsBySearchCriteria(List<string> criteriasList);

    }
}
