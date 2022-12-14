﻿namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IConsiliumRepository : IBaseRepository<Consilium>
    {
        List<Consilium> GetScheduledConsiliumsForRoom(int roomId);

        List<Consilium> GetConsiliumsByDoctorId(int doctorId);
    }
}
