﻿namespace HospitalLibrary.Core.Service.Examinations.Core
{
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.DTO.PDF;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAnamnesisService : IBaseService<Anamnesis>
    {
        IEnumerable<Anamnesis> GetByDoctor(int doctorId);

        IEnumerable<Anamnesis> GetByPatient(int patientId);

        IEnumerable<Anamnesis> GetInDateRange(DateRange dateRange);

        Anamnesis Add(NewAnamnesisDto dto);

        Anamnesis AddPrescriptions(int anamnesisId, List<Prescription> prescriptions);

        Anamnesis GetByAppointment(int id);

        void GeneratePdf(AnamnesisPdfDTO dto);

        IEnumerable<Anamnesis> GetAnamnesesBySearchCriteria(List<string> criteriasList);
    }
}
