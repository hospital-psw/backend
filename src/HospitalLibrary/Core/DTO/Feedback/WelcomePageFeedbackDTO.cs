namespace HospitalLibrary.Core.DTO.Feedback
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WelcomePageFeedbackDTO
    {
        public string Creator { get; set; }
        public string Message { get; set; }

        public WelcomePageFeedbackDTO(string creator, string message)
        {
            Creator = creator;
            Message = message;
        }
    }
}