namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using System;

    public class WorkingHoursMapper
    {
        public static WorkingHoursDto EntityToEntityDto(WorkingHours workingHours)
        {
            WorkingHoursDto dto = new WorkingHoursDto();

            if (workingHours != null)
            {
                dto.Id = workingHours.Id;
                dto.Start = workingHours.Start;
                dto.End = workingHours.End;
            }
            else
            {
                dto.Id = -1; // da li ovako?
                dto.Start = new DateTime();
                dto.End = new DateTime();
            }
            return dto;
        }

        public static WorkingHours EntityDtoToEntity(WorkingHoursDto dto)
        {
            WorkingHours workingHours = new WorkingHours();

            if (dto.Id != -1)
            {
                workingHours.Id = dto.Id;
                workingHours.Start = dto.Start;
                workingHours.End = dto.End;
            }
            else
            {
                workingHours = null;
            }
            return workingHours;
        }
    }
}
