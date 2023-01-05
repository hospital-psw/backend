namespace IntegrationLibraryTest.UnitTests
{
    using IntegrationLibrary.Tender;
    using IntegrationLibraryTest.MockData;
    using Shouldly;

    public class TenderUnitTest
    {
        [Fact]
        public void MakeAnOffer_ValidOffer_ShouldReturnValidOffer()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.ValidOfferForTender1;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldNotBeNull();
            validOffer.Items.Count.ShouldBe(2);
        }

        [Fact]
        public void MakeAnOffer_ValidOfferExtraAmount_ShouldReturnValidOffer()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.ValidOfferForTender1ExtraBloodAmount;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldNotBeNull();
            validOffer.Items.Count.ShouldBe(2);
        }

        [Fact]
        public void MakeAnOffer_ValidOfferExtraBloodType_ShouldReturnValidOffer()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.ValidOfferForTender1ExtraBloodTypes;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldNotBeNull();
            validOffer.Items.Count.ShouldBe(3);
        }

        [Fact]
        public void MakeAnOffer_MissingItemInOffer_ShouldReturnNull()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.InvalidOfferForTender1MissingItem;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldBeNull();
        }

        [Fact]
        public void MakeAnOffer_InsufficientAmount_ShouldReturnNull()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.InvalidOfferForTender1InsufficientAmount;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldBeNull();
        }

        [Fact]
        public void MakeAnOffer_WrongBloodType_ShouldReturnNull()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.InvalidOfferForTender1WrongBloodType;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldBeNull();
        }


    }
}
