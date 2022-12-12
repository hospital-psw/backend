using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class RenovationDetails : Entity
    {
        public string NewRoomName { get; private set; }
        public string NewRoomPurpose { get; private set; }
        public int NewCapacity { get; private set; }

        private RenovationDetails() { }

        private RenovationDetails(string newRoomName, string newRoomPurpose, int newCapacity)
        {
            NewRoomName = newRoomName;
            NewRoomPurpose = newRoomPurpose;
            NewCapacity = newCapacity;
        }

        public static RenovationDetails Create(string newRoomName, string newRoomPurpose, int newCapacity)
        {
            return new RenovationDetails(newRoomName, newRoomPurpose, newCapacity);
        }
    }
}
