namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class WorkingHoursMapper
    {
        public static WorkingHoursDto EntityToEntityDto(WorkingHours workingHours)
        {
            WorkingHoursDto dto = new WorkingHoursDto();

            if (workingHours != null)
            {
                dto.Start = workingHours.Start.Hour.ToString() + ":" + workingHours.Start.Minute.ToString();
                dto.End = workingHours.End.Hour.ToString() + ":" + workingHours.End.Minute.ToString();
            }
            else
            {
                dto.Start = "";
                dto.End = "";
            }
            return dto;
        }
    }
}
