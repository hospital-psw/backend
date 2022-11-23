namespace HospitalLibrary.Core.DTO.Feedback
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewFeedbackDTO
    {
        [Required(ErrorMessage = "Required")]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool Anonymous { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool Public { get; set; }

    }
}
