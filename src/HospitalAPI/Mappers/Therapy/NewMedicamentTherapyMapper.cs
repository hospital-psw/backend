namespace HospitalAPI.Mappers.Therapy
{
    using HospitalAPI.Dto.Therapy;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Model.Therapy;
    using System;

    public class NewMedicamentTherapyMapper
    {
        public static MedicamentTherapy EntityDtoToEntity(NewMedicamentTherapyDto dto, Medicament medicament)
        {
            return new MedicamentTherapy
            {
                Start = DateTime.Now,
                End = default(DateTime),
                AmountOfMedicament = dto.Amount,
                Medicament = medicament,
                About = dto.About,
                Type = TherapyType.MEDICAMENT
            };
        }
    }
}
