namespace IntegrationLibraryTest.UnitTests
{
    using IntegrationLibrary.UrgentBloodTransfer;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Moq;
    using OpenQA.Selenium.DevTools.V105.Page;
    using Shouldly;

    public class UrgentBloodStatisticsTest
    {
        private UrgentBloodTransferStatisticsService SetupService() 
        {
            var service = new Mock<IUrgentBloodTransferService>();
            service.Setup(x => x.GetAll()).Returns(
                new List<UrgentBloodTransfer>()
                {
                    new UrgentBloodTransfer(grpcServices.BloodType.Abplus, 5, true)
                    {
                        DateCreated = new DateTime(2022, 11, 5),
                        Sender = new IntegrationLibrary.BloodBank.BloodBank() { Name = "Blood bank 1"},
                    },
                    new UrgentBloodTransfer(grpcServices.BloodType.Bplus, 10, true)
                    {
                        DateCreated = new DateTime(2022, 11, 5),
                        Sender = new IntegrationLibrary.BloodBank.BloodBank() { Name = "Blood bank 1"},
                    },
                    new UrgentBloodTransfer(grpcServices.BloodType.Oplus, 3, true)
                    {
                        DateCreated = new DateTime(2022, 11, 6),
                        Sender = new IntegrationLibrary.BloodBank.BloodBank() { Name = "Blood bank 2"},
                    },
                    new UrgentBloodTransfer(grpcServices.BloodType.Ominus, 7, true)
                    {
                        DateCreated = new DateTime(2022, 11, 6),
                        Sender = new IntegrationLibrary.BloodBank.BloodBank() { Name = "Blood bank 2"},
                    }
                }
            );
            return new UrgentBloodTransferStatisticsService(service.Object, new Mock<IHTMLReportService>().Object);
        }

        [Fact]
        public void GetAmountByBloodBankByBloodGroup_ShouldReturn_TwoBloodBanks()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodBankByBloodGroup(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.Count.ShouldBe(2);
            var expectedResult_BloodBank1 = new Dictionary<string, double>();
            var expectedResult_BloodBank2 = new Dictionary<string, double>();
            expectedResult_BloodBank1.Add("Abplus", 5);
            expectedResult_BloodBank1.Add("Bplus", 10);
            expectedResult_BloodBank2.Add("Oplus", 3);
            expectedResult_BloodBank2.Add("Ominus", 7);
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
            result["Abplus"].ShouldBe(5);
            result["Bplus"].ShouldBe(10);
            result["Oplus"].ShouldBe(3);
            result["Ominus"].ShouldBe(7);
        }

        [Fact]
        public void GetAmountByBloodUnit_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodUnit(new DateTime(2022, 11, 20), new DateTime(2022, 11, 1));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetAmountByBloodUnit_InvalidDateRange_ShouldReturn_EmptyDictionary()
        {
            var service = SetupService();

            var result = service.GetAmountByBloodUnit(new DateTime(2022, 11, 20), new DateTime(2022, 11, 1));

            result.ShouldNotBe(null);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetBloodBankShare_ShouldReturn_TwoBloodBanks()
        {
            var service = SetupService();

            var result = service.GetBloodBankShare(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.Count.ShouldBe(2);
            result["Blood bank 1"].ShouldBe(0.6);
            result["Blood bank 2"].ShouldBe(0.4);
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

        [Fact]
        public void Test()
        {
            var service = new HTMLReportService();

            //service.AddBarChart();
        }
    }
}
