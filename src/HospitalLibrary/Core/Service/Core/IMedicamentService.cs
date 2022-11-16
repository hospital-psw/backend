namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMedicamentService
    {
        Medicament Get(int id);

        Medicament Update(Medicament medicament);

        IEnumerable<Medicament> GetAll();

        Medicament Add(Medicament mediacament);

        void Delete(Medicament medicament);
    }
}
