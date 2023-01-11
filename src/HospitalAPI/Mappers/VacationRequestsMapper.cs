namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers.AppUsers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
    using System.Collections.Generic;

    public class VacationRequestsMapper
    {
        public static VacationRequestDto EntityToEntityDto(VacationRequest vacationRequest)
        {
            VacationRequestDto dto = new VacationRequestDto();
            if (vacationRequest == null) return null;

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

        public static List<VacationRequestDto> EntityListToDtoList(List<VacationRequest> vacationRequests)
        {
            List<VacationRequestDto> dtoList = new List<VacationRequestDto>();
            if (vacationRequests == null) return dtoList;
            vacationRequests.ForEach(x => dtoList.Add(EntityToEntityDto(x)));
            return dtoList;
        }
    }
}
