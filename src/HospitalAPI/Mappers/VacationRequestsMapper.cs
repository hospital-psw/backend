namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequest;

    public class VacationRequestsMapper
    {
        public static VacationRequestDto EntityToEntityDto(VacationRequest vacationRequest)
        {
            VacationRequestDto dto = new VacationRequestDto();

            dto.Id = vacationRequest.Id;
            dto.Doctor = DoctorMapper.EntityToEntityDto(vacationRequest.Doctor);
            dto.From = vacationRequest.From;
            dto.To = vacationRequest.To;
            dto.Status = vacationRequest.Status;
            dto.Comment = vacationRequest.Comment;
            dto.Urgent = vacationRequest.Urgent;
            dto.ManagerComment = vacationRequest.ManagerComment;

            return dto;
        }
    }
}
