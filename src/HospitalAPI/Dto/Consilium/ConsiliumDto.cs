﻿namespace HospitalAPI.Dto.Consilium
{
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;

    public class ConsiliumDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public string Topic { get; set; }
        public RoomDto Room { get; set; }
        public List<ApplicationDoctorDTO> Doctors { get; set; } = new List<ApplicationDoctorDTO>();
    }
}
