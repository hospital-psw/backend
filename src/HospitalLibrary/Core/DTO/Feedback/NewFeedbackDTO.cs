namespace HospitalLibrary.Core.DTO.Feedback
{
    using HospitalLibrary.Core.Model.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewFeedbackDTO
    {
        private int num;
        private string mess;
        private bool b;
        private bool t;

        public NewFeedbackDTO(int num, string mess, bool b, bool t)
        {
            this.num = num;
            this.mess = mess;
            this.b = b;
            this.t = t;
        }

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
