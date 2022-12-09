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
        public EquipmentType EquipmentType { get; private set; }
        public int Quantity { get; private set; }
        public Room Room { get; private set; }
        public int ReservedQuantity { get; private set; }
        public Equipment()
        {
        }

        public Equipment(EquipmentType equipmentType, int quantity, Room room)
        {
            EquipmentType = equipmentType;
            Quantity = quantity;
            Room = room;
        }

        public Equipment(EquipmentType equipmentType, int quantity, Room room, int reservedQuantity)
        {
            EquipmentType = equipmentType;
            Quantity = quantity;
            Room = room;
            ReservedQuantity = reservedQuantity;
        }

        public Equipment(EquipmentType equipmentType, int quantity, Room room, int id, int reservedQuantity)
        {
            Id = id;
            EquipmentType = equipmentType;
            Quantity = quantity;
            Room = room;
            ReservedQuantity = reservedQuantity;
        }

        public Equipment AddReservedQuantity(int quantity)
        {
            this.ReservedQuantity += quantity;
            return this;
        }
        public Equipment SubstractReservedQuantity(int quantity)
        {
            this.ReservedQuantity -= quantity;
            return this;
        }

        public static Equipment Create(EquipmentType equipmentType, int quantity, Room room)
        {
            Equipment equipment = new Equipment(equipmentType, quantity, room);
            return equipment;
        }

        public static Equipment CreateWithReservedQuantity(EquipmentType equipmentType, int quantity, Room room, int reservedQuantity)
        {
            Equipment equipment = new Equipment(equipmentType, quantity, room, reservedQuantity);
            return equipment;
        }

        public Equipment AddQuantity(int quantity)
        {
            this.Quantity += quantity;
            return this;
        }

        public Equipment SubstractQuantity(int quantity)
        {
            this.Quantity -= quantity;
            this.CheckForDelete();
            return this;
        }

        public void CheckForDelete()
        {
            if (this.Quantity <= 0)
                this.Deleted = true;
        }
    }
}
