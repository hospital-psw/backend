using IntegrationLibrary;

namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Tender.Enums;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TenderItem : Entity
    {
        public BloodType BloodType { get; set; }
        public double Money { get; set; }
        public double Quantity { get; set; }
    }
}
