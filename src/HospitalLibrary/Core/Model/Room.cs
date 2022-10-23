using HospitalLibrary.Core.DTO;
using IdentityServer4.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Core.Model
{
    public class Room : Entity
    {
        public string Number { get; set; }
        public Floor Floor { get; set; }
        public Building Building { get; set; }
        public string Purpose { get; set; }

        public Room()
        {
        }

        public Room(RoomDTO dto)
        {
            Number = dto.Number;
            Purpose = dto.Purpose;
        }

    }
}