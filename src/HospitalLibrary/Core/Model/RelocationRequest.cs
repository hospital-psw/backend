using HospitalLibrary.Core.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class RelocationRequest : Entity
    {
        public Room FromRoom { get; private set; }
        public Room ToRoom { get; private set; }
        public Equipment Equipment { get; private set; }
        public RelocationQuantity Quantity { get; private set; }
        public DateTime StartTime { get; private set; }
        public RelocationDuration Duration { get; private set; }

        private RelocationRequest() { }

        private RelocationRequest(Room fromRoom, Room toRoom, Equipment equipment, RelocationQuantity quantity, DateTime startTime, RelocationDuration duration)
        {
            FromRoom = fromRoom;
            ToRoom = toRoom;
            Equipment = equipment;
            Quantity = quantity;
            StartTime = startTime;
            Duration = duration;
        }

        public static RelocationRequest Create(Room fromRoom, Room toRoom, Equipment equipment, int quantity, DateTime startTime, int duration)
        {
            if (fromRoom == null) throw new Exception("FromRoom cannot be null");
            if (toRoom == null) throw new Exception("ToRoom cannot be null");
            if (equipment == null) throw new Exception("Equipment cannot be null");
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0");
            if (duration <= 0) throw new Exception("Duration must be greater than 0");
            return new RelocationRequest(fromRoom, toRoom, equipment, RelocationQuantity.Create(quantity), startTime, RelocationDuration.Create(duration));
        }


        public void DeleteRelocation()
        {

            Equipment.SubstractReservedQuantity(this.Quantity.Quantity);
            this.Deleted = true;
        }
    }
}
