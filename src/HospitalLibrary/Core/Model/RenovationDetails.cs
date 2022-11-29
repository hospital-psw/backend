using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class RenovationDetails : Entity
    { 
        public string NewRoomName { get; set; }
        public string NewRoomPurpose { get; set; }

        public RenovationDetails() { }
    }
}
