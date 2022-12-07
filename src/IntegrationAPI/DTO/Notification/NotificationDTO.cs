namespace IntegrationAPI.DTO.Notification
{
    using IntegrationLibrary.Notification;
    using IntegrationLibrary.Notification.Enums;
    using IntegrationLibrary.Tender.Enums;
    using System;

    public class NotificationDTO
    {
        public BloodUnitStatus BloodUnitStatus { get; set; }
        public string Message { get; set; }
        public BloodType BloodType { get; set; }
    }
}
