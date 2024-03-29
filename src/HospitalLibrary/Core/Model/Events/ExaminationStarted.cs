﻿namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationStarted : DomainEvent
    {

        public int UserId { get; set; }
        public int AppointmentId { get; set; }

        public ExaminationStarted(int aggregateId, DateTime timeStamp, string eventName, int appointmentId, int userId) : base(aggregateId, timeStamp, eventName)
        {
            AppointmentId = appointmentId;
            UserId = userId;
        }
    }
}
