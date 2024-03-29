﻿namespace HospitalAPI.Mappers.Examinations
{
    using HospitalAPI.Dto.Examinations;
    using HospitalAPI.Mappers.Medicament;
    using HospitalLibrary.Core.Model.Examinations;
    using System.Collections.Generic;

    public class PrescriptionMapper
    {

        public static PrescriptionDto EntityToEntityDto(Prescription prescription)
        {
            if (prescription == null) return null;
            return new PrescriptionDto(prescription.Id, MedicamentMapper.EntityToEntityDto(prescription.Medicament), prescription.Description, prescription.DateRange);
        }

        public static List<PrescriptionDto> EntityListToEntityDtoList(List<Prescription> prescriptions)
        {
            if (prescriptions == null) return null;

            List<PrescriptionDto> retList = new List<PrescriptionDto>();
            prescriptions.ForEach(x => retList.Add(EntityToEntityDto(x)));
            return retList;
        }
    }
}
