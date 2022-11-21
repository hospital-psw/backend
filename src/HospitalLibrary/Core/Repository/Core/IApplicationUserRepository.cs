namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationUserRepository : IBaseRepository<ApplicationUser>
    {
        public IEnumerable<ApplicationUser> GetAllDoctors();
        public IEnumerable<ApplicationUser> GetAllPatients();

    }

}
