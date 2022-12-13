using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class RenovationRequest : Entity
    {
        public RenovationType RenovationType { get; private set; }
        public List<Room> Rooms { get; private set; }
        public DateTime StartTime { get; private set; }
        public int Duration { get; private set; }
        public List<RenovationDetails> RenovationDetails { get; private set; }

        private RenovationRequest() { }
        private RenovationRequest(RenovationType renovationType, List<Room> rooms, DateTime startTime, int duration, List<RenovationDetails> renovationDetails)
        {
            RenovationType = renovationType;
            Rooms = rooms;
            StartTime = startTime;
            Duration = duration;
            RenovationDetails = renovationDetails;
        }

        public static RenovationRequest Create(RenovationType type, List<Room> rooms, DateTime startTime, int duration, List<RenovationDetails> details)
        {
            //if (startTime < DateTime.Now) throw new Exception("Start time cannot be in the past");
            return new RenovationRequest(type, rooms, startTime, duration, details);
        }

        public void Delete()
        {
            Deleted = true;
        }
    }
}
