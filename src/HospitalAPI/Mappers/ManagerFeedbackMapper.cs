namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class ManagerFeedbackMapper
    {
        public static ManagerFeedbackDto EntityToEntityDto(Feedback feedback)
        {
            ManagerFeedbackDto dto = new ManagerFeedbackDto();

            dto.Creator = feedback.Creator.FirstName + " " + feedback.Creator.LastName;
            dto.FeedbackId = feedback.Id;
            dto.Message = feedback.Message.Message;
            dto.Status = feedback.Status;
            dto.Anonymous = feedback.Anonymous;
            dto.Public = feedback.Public;

            return dto;
        }
    }
}
