namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class VacationRequestDto
    {
        public int Id { get; set; }
        public DoctorDto Doctor { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public VacationRequestStatus Status { get; set; }
        public string Comment { get; set; }
        public bool Urgent { get; set; }
        public string ManagerComment { get; set; }

        public VacationRequestDto() { }

        public VacationRequestDto(int id, DoctorDto doctor, DateTime from, DateTime to, VacationRequestStatus status, string comment, bool urgent, string managerComment)
        {
            Id = id;
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
