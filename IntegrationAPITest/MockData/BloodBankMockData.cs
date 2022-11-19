namespace IntegrationAPITest.MockData
{
    using IntegrationLibrary.BloodBank;

    public static class BloodBankMockData
    {
        public static BloodBank BloodBank1
        {
            get
            {
                return new BloodBank()
                {
                    Name = "Bank 1",
                    ApiUrl = "localhost:8081",
                    ApiKey = "blah",
                    AdminPassword = "4321",
                    Email = "zika@hotmail.com",
                    IsDummyPassword = true,
                    GetBloodTypeAvailability = "api/checkblood/!BLOOD_TYPE",
                    GetBloodTypeAndAmountAvailability = "api/checkbloodamount/!BLOOD_TYPE/!AMOUNT"
                };
            }
        }

    }
}
