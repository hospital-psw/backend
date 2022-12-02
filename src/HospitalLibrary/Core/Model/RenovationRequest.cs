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
        public RenovationType RenovationType { get; set; }
        public List<Room> Rooms { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public List<RenovationDetails> RenovationDetails { get; set; }

        public RenovationRequest() { }
    }
}
