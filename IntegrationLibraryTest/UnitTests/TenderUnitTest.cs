namespace IntegrationLibraryTest.UnitTests
{
    using IntegrationLibrary.Tender;
    using IntegrationLibraryTest.MockData;
    using Shouldly;

    public class TenderUnitTest
    {
        [Fact]
        public void Make_an_offer()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.ValidOfferForTender1;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldNotBeNull();
            validOffer.Items.Count.ShouldBe(2);
        }

        [Fact]
        public void Make_an_Offer_for_extra_amount()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.ValidOfferForTender1ExtraBloodAmount;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldNotBeNull();
            validOffer.Items.Count.ShouldBe(2);
        }

        [Fact]
        public void Make_an_Offer_for_extra_blood_type()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.ValidOfferForTender1ExtraBloodTypes;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldNotBeNull();
            validOffer.Items.Count.ShouldBe(3);
        }

        [Fact]
        public void Make_an_Offer_with_missing_item()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.InvalidOfferForTender1MissingItem;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldBeNull();
        }

        [Fact]
        public void Make_an_offer_with_insufficient_amount()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.InvalidOfferForTender1InsufficientAmount;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldBeNull();
        }

        [Fact]
        public void Make_an_offer_with_wrong_blood_type()
        {
            Tender tender = TenderMockData.Tender1;
            TenderOffer offer = TenderMockData.InvalidOfferForTender1WrongBloodType;

            var validOffer = tender.MakeAnOffer(offer);

            validOffer.ShouldBeNull();
        }
    }
}
