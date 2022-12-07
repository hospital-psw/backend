namespace IntegrationLibrary.Notification
{
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Notification.Enums;
    using IntegrationLibrary.Tender.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Notification : Entity
    {
        public string Message { get; set; }
        public BloodUnitStatus BloodUnitStatus { get; set; }
        public BloodType BloodType { get; set; }
    }
}
