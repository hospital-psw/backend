﻿namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Consilium : Entity
    {

        public DateTime DateTime { get; set; }
        public string Topic { get; set; }
        public int Duration { get; set; }
        public List<DoctorSchedule> DoctorsSchedule { get; set; }
        public Room Room { get; set; }

        public Consilium()
        {
            DoctorsSchedule = new List<DoctorSchedule>();
        }

        public Consilium(DateTime dateTime, string topic, int duration, List<DoctorSchedule> doctorsSchedule, Room room)
        {
            DateTime = dateTime;
            Topic = topic;
            Duration = duration;
            DoctorsSchedule = doctorsSchedule;
            Room = room;
        }
    }
}
