namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers.AppUsers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;

    public class VacationRequestsMapper
    {
        public static VacationRequestDto EntityToEntityDto(VacationRequest vacationRequest)
        {
            VacationRequestDto dto = new VacationRequestDto();

            dto.Id = vacationRequest.Id;
            dto.From = vacationRequest.From;
            dto.To = vacationRequest.To;
            dto.Status = vacationRequest.Status;
            dto.Comment = vacationRequest.Comment.Comment;
            dto.Urgent = vacationRequest.Urgent;
            dto.ManagerComment = vacationRequest.ManagerComment;
            dto.Doctor = ApplicationDoctorMapper.EntityToEntityDTO(vacationRequest.Doctor);

            return dto;
        }
    }
}
