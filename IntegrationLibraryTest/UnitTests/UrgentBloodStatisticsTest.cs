namespace IntegrationLibraryTest.UnitTests
{
    using IntegrationLibrary.UrgentBloodTransfer;
    using Shouldly;

    public class UrgentBloodStatisticsTest
    {
        private UrgentBloodTransferStatisticsService SetupService() 
        {
            return new UrgentBloodTransferStatisticsService();
        }

        [Fact]
        public void GetAmountByBloodBankByBloodGroup_ShouldReturn_TwoBloodBanks()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodBankByBloodGroup(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.Count.ShouldBe(2);
            var expectedResult_BloodBank1 = new Dictionary<string, double>();
            var expectedResult_BloodBank2 = new Dictionary<string, double>();
            expectedResult_BloodBank1.Add("AB_POSITIVE", 5);
            expectedResult_BloodBank1.Add("B_POSITIVE", 10);
            expectedResult_BloodBank2.Add("O_POSITIVE", 3);
            expectedResult_BloodBank2.Add("O_NEGATIVE", 7);
            result["Blood bank 1"].ShouldBe(expectedResult_BloodBank1);
            result["Blood bank 2"].ShouldBe(expectedResult_BloodBank2);
        }        

        [Fact]
        public void GetAmountByBloodBankByBloodGroup_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodBankByBloodGroup(new DateTime(2000, 1, 1), new DateTime(2000, 10, 1));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetAmountByBloodBankByBloodGroup_InvalidDateRange_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodBankByBloodGroup(new DateTime(2022, 10, 1), new DateTime(2000, 1, 1));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetAmountByBloodUnit_ShouldReturn_FourBloodUnits()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodUnit(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.Count.ShouldBe(4);
            result["AB_POSITIVE"].ShouldBe(5);
            result["B_POSITIVE"].ShouldBe(10);
            result["O_POSITIVE"].ShouldBe(3);
            result["O_NEGATIVE"].ShouldBe(7);
        }

        [Fact]
        public void GetAmountByBloodUnit_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodUnit(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetAmountByBloodUnit_InvalidDateRange_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodUnit(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetBloodBankShare_ShouldReturn_TwoBloodBanks()
        {
            var service = SetupService();

            var result = service.GetBloodBankShare(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.Count.ShouldBe(4);
            result["Blood bank 1"].ShouldBe(15);
            result["Blood bank 2"].ShouldBe(10);
        }

        [Fact]
        public void GetBloodBankShare_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetBloodBankShare(new DateTime(2000, 11, 1), new DateTime(2000, 11, 30));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetBloodBankShare_InvalidDateRange_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetBloodBankShare(new DateTime(2022, 10, 1), new DateTime(2022, 1, 1));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }
    }
}
