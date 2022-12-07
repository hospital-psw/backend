namespace IntegrationAPITest.MockData
{
    using IntegrationLibrary.Notification;
    using IntegrationLibrary.Notification.Enums;
    using IntegrationLibrary.Tender.Enums;

    public class NotificationMockData
    {
        public static Notification Notification1
        {
            get
            {
                return new Notification()
                {
                    BloodUnitStatus = BloodUnitStatus.IN_STOCK,
                    Message = "porukaa",
                    BloodType = BloodType.A_NEGATIVE
                };
            }
        }
    }
}
