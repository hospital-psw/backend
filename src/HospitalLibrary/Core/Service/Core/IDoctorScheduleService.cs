﻿namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDoctorScheduleService
    {
        DoctorSchedule Get(int doctorScheduleId);
        IEnumerable<DoctorSchedule> GetAll();
    }
}
