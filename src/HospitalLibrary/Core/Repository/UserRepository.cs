namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(HospitalDbContext context) : base(context) { }
    }
}
