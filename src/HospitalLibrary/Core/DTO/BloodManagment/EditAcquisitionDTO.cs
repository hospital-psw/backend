namespace HospitalLibrary.Core.DTO.BloodManagment
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditAcquisitionDTO
    {
        public int Id { get; set; }
        public BloodRequestStatus Status { get; set; }
        public string ManagerComment { get; set; }


        public EditAcquisitionDTO(int id, BloodRequestStatus status, string managerComment)
        {
            Id = id;
            ManagerComment = managerComment;
            Status = status;
        }
    }
}
