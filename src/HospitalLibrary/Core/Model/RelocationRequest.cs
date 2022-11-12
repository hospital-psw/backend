using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class RelocationRequest : Entity
    {
        public Room FromRoom { get; set; }
        public Room ToRoom { get; set; }
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public RelocationRequest() { }
    }
}
