﻿namespace IntegrationLibrary.UrgentBloodTransfer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HTTPUrgentBloodTransferRequest
    {
        public BloodType BloodType { get; set; }
        public uint Amount { get; set; }
    }
}
