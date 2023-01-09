namespace IntegrationLibraryTest.UnitTests
{
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using IntegrationLibrary.UrgentBloodTransfer;
    using IntegrationLibrary.Util.Interfaces;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationLibrary.Tender.Interfaces;
    using Shouldly;
    using IntegrationLibrary.Util;

    public class TenderBloodStatisticsTest
    {
        private TenderBloodTransferStatisticsService SetupService()
        {
            var service = new Mock<ITenderService>();
            service.Setup(x => x.GetAll()).Returns(
                new List<Tender>()
                {
                   
                }
            );
            return new TenderBloodTransferStatisticsService(service.Object, new Mock<IHTMLReportService>().Object);

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
        public void GetBloodBankShare_ShouldReturn_TwoBloodBanks()
        {
            var service = SetupService();

            var result = service.GetBloodBankShare(new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));

            result.Count.ShouldBe(2);
            result["Blood bank 1"].ShouldBe(0.6);
            result["Blood bank 2"].ShouldBe(0.4);
        }

        [Fact]
        public void Test()
        {
            var service = new HTMLReportService();

        }
    }
}
