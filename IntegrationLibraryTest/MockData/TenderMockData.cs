namespace IntegrationLibraryTest.MockData
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using System;
    using System.Collections.Generic;

    public static class TenderMockData
    {
        public static Tender Tender1
        {
            get
            {
                return new Tender()
                {
                    Status = TenderStatus.OPEN,
                    DueDate = DateTime.Now.AddHours(1),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 5
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 6
                    }
                }
                };
            }
        }

        public static TenderOffer ValidOfferForTender1
        {
            get
            {
                return new TenderOffer()
                {
                    Offeror = new BloodBank(),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 5
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 6
                    }
                }
                };
            }
        }

        public static TenderOffer ValidOfferForTender1ExtraBloodAmount
        {
            get
            {
                return new TenderOffer()
                {
                    Offeror = new BloodBank(),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 20
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 60
                    }
                }
                };
            }
        }

        public static TenderOffer ValidOfferForTender1ExtraBloodTypes
        {
            get
            {
                return new TenderOffer()
                {
                    Offeror = new BloodBank(),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 5
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 6
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.O_POSITIVE,
                        Quantity = 3
                    }
                }
                };
            }
        }

        public static TenderOffer InvalidOfferForTender1MissingItem
        {
            get
            {
                return new TenderOffer()
                {
                    Offeror = new BloodBank(),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 5
                    },
                }
                };
            }
        }

        public static TenderOffer InvalidOfferForTender1InsufficientAmount
        {
            get
            {
                return new TenderOffer()
                {
                    Offeror = new BloodBank(),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 5
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 5
                    }
                }
                };
            }
        }

        public static TenderOffer InvalidOfferForTender1WrongBloodType
        {
            get
            {
                return new TenderOffer()
                {
                    Offeror = new BloodBank(),
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.O_POSITIVE,
                        Quantity = 5
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 6
                    }
                }
                };
            }
        }

    }
}
