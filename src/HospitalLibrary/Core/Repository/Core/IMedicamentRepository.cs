﻿namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMedicamentRepository : IBaseRepository<Medicament>
    {

        IEnumerable<Medicament> GetAcceptableMedicaments(ApplicationPatient patient);
    }
}
