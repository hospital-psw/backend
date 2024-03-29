﻿namespace HospitalAPI.Mappers.Medicament
{
    using HospitalAPI.Dto.Medicament;
    using HospitalLibrary.Core.Model.Medicament;
    using System.Collections.Generic;

    public class MedicamentMapper
    {
        public static MedicamentDto EntityToEntityDto(Medicament medicament)
        {
            if (medicament == null) return null;
            MedicamentDto dto = new MedicamentDto();

            dto.Id = medicament.Id;
            dto.Name = medicament.Name;
            dto.Quantity = medicament.Quantity;
            dto.Description = medicament.Description;
            medicament.Allergens.ForEach(al => dto.Allergens.Add(AllergiesMapper.EntityToEntityDto(al)));

            return dto;
        }

        public static List<MedicamentDto> EntityToEntityDtoList(List<Medicament> medicaments)
        {
            List<MedicamentDto> list = new List<MedicamentDto>();
            medicaments.ForEach(m => list.Add(EntityToEntityDto(m)));
            return list;
        }
    }
}
