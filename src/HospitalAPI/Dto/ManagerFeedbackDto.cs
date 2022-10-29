namespace HospitalAPI.Dto
{
    public class ManagerFeedbackDto
    {
        public string FeedbackId { get; set; }
        public string Creator { get; set; }
        public string Message { get; set; }
        public bool Anonymous { get; set; }
        public bool Public { get; set; }
    }
}
