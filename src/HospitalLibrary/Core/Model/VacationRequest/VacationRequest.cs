namespace HospitalLibrary.Core.Model.VacationRequest
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequest
    {
        public Doctor Doctor { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public VacationRequestStatus Status { get; set; }

        public String Reason { get; set; }

        public String RejectionReason { get; set; }

        public int Id { get; set; }

        public VacationRequest() { }

        public VacationRequest(Doctor doctor, DateTime from, DateTime to, VacationRequestStatus status, string reason, string rejectionReason)
        {
            Doctor = doctor;
            From = from;
            To = to;
            Status = status;
            Reason = reason;
            RejectionReason = rejectionReason;
        }
    }
}
