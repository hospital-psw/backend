namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class ManagerFeedbackDto
    {
        public int FeedbackId { get; set; }
        public string Creator { get; set; }
        public string Message { get; set; }
        public bool Anonymous { get; set; }
        public bool Public { get; set; }
        public FeedbackStatus Status { get; set; }
    }
}
