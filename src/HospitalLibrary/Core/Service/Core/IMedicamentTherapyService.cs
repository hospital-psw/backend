namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.Therapy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMedicamentTherapyService
    {
        MedicamentTherapy Get(int id);

        MedicamentTherapy Update(MedicamentTherapy medicamentTherapy);

        IEnumerable<MedicamentTherapy> GetAll();

        MedicamentTherapy Add(MedicamentTherapy medicamentTherapy);

        void Delete(MedicamentTherapy medicamentTherapy);
    }
}
