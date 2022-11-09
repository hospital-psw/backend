namespace HospitalLibrary.Core.DTO.Therapy
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewTherapyDto
    {
        public string About { get; set; }

        public TherapyType Type { get; set; }

    }
}
