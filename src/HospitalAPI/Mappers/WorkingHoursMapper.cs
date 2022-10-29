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
                dto.Start = workingHours.Start;
                dto.End = workingHours.End;
            }
            else
            {
                dto.Start = new DateTime();
                dto.End = new DateTime();
            }
            return dto;
        }
    }
}
