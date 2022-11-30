namespace HospitalLibrary.Core.Model.VacationRequests
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequest : Entity
    {
        public ApplicationDoctor Doctor{ get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public VacationRequestStatus Status { get; set; }
        public string Comment { get; set; }
        public bool Urgent { get; set; }
        public string ManagerComment { get; set; }

        public VacationRequest() { }

        public VacationRequest(ApplicationDoctor doctor, DateTime from, DateTime to, VacationRequestStatus status, string comment, bool urgent, string managerComment)
        {
            Doctor = doctor;
            From = from;
            To = to;
            Status = status;
            Comment = comment;
            Urgent = urgent;
            ManagerComment = managerComment;
        }
    }
}
