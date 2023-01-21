namespace HospitalAPI.Mappers.Blood
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using System.Collections.Generic;

    public class BloodAcquisitionMapper
    {
        public static CreateAcquisitionDTO EntityToEntityDto(BloodAcquisition bloodAcquisition)
        {
            CreateAcquisitionDTO dto = new CreateAcquisitionDTO();

            dto.DoctorId = bloodAcquisition.Doctor.Id;
            dto.Date = bloodAcquisition.Date;
            dto.BloodType = bloodAcquisition.BloodType;
            dto.Amount = bloodAcquisition.Amount;
            dto.Reason = bloodAcquisition.Reason;

            return dto;
        }
    }
}
