namespace HospitalLibrary.Core.Model
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
        public int ReservedQuantity { get; set; }
        public Equipment()
        {
        }

        public Equipment(EquipmentType equipmentType, int quantity, Room room)
        {
            EquipmentType = equipmentType;
            Quantity = quantity;
            Room = room;
        }

        public Equipment AddReservedQuantity(Equipment equipment, int quantity)
        {
            equipment.ReservedQuantity += quantity;
            return equipment;
        }

        public static void Create(EquipmentType equipmentType, int quantity, Room room)
        {
            Equipment equipment = new Equipment(equipmentType, quantity, room);
        }

        public Equipment AddQuantity(Equipment equipment, int quantity)
        {
            equipment.Quantity += quantity;
            return equipment;
        }

        public Equipment SubstractQuantity(Equipment equipment, int quantity)
        {
            equipment.Quantity -= quantity;
            return equipment;
        }

        public bool CheckForDelete(Equipment equipment)
        {
            if (equipment.Quantity <= 0)
                return true;
            return false;
        }
    }
}
