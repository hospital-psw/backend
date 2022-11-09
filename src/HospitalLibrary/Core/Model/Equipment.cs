﻿namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Equipment : Entity
    {
        public EquipmentType EquipmentType { get; set; }
        public int Quantity { get; set; }
        public Room Room { get; set; }

        public Equipment()
        {
        }

        public Equipment(EquipmentType equipmentType, int quantity, Room room)
        {
            EquipmentType = equipmentType;
            Quantity = quantity;
            Room = room;
        }
    }
}
