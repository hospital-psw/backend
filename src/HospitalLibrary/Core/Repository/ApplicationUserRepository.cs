﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly HospitalDbContext _context;
        public ApplicationUserRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllDoctors()
        {
            return _context.ApplicationDoctors.ToList();
        }
        public IEnumerable<ApplicationUser> GetAllPatients()
        {
            return _context.ApplicationPatients.ToList();
        }
    }
}
