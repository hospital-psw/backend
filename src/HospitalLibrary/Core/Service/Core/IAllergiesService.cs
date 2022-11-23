namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAllergiesService
    {
        Allergies Add(Allergies allergy);
        IEnumerable<Allergies> GetAll();
        List<Allergies> GetAllergiesFromDTO(List<int> ids);
    }
}
