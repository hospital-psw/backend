namespace IntegrationLibrary.BloodBank
{
    using DataAnnotationsExtensions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Owned]
    public class MonthlyTransfer
    {
        public DateTime DateTime { get; set; }
        [Min(0)]
        public int APlus { get; set; }
        [Min(0)]
        public int BPlus { get; set; }
        [Min(0)]
        public int AMinus { get; set; }
        [Min(0)]
        public int BMinus { get; set; }
        [Min(0)]
        public int OPlus { get; set; }
        [Min(0)]
        public int OMinus { get; set; }
        [Min(0)]
        public int ABPlus { get; set; }
        [Min(0)]
        public int ABMinus { get; set; }
    }
}
