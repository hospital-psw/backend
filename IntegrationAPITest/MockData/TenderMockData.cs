namespace IntegrationAPITest.MockData
{
    using IntegrationAPI.DTO.Tender;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TenderMockData
    {
        public static Tender Tender1
        {
            get
            {
                return new Tender()
                {
                    Status = TenderStatus.OPEN,
                    DueDate = new DateTime(2022, 1, 1),
                    Items = new List<TenderItem>()
                    {
                        new TenderItem()
                        {
                            BloodType = BloodType.A_POSITIVE,
                            Quantity = 5,
                        },
                        new TenderItem()
                        {
                            BloodType = BloodType.B_POSITIVE,
                            Quantity = 5,
                        },
                    }
                };
            }
        }

        public static Tender TenderClosed
        {
            get
            {
                return new Tender()
                {
                    Status = TenderStatus.CLOSED,
                    DueDate = new DateTime(2000, 1, 1)
                };
            }
        }

        public static MakeTenderOfferDTO TenderOfferWithTwoItems
        {
            get
            {
                return new MakeTenderOfferDTO()
                {
                    Items = new List<TenderItem>()
                {
                    new TenderItem()
                    {
                        BloodType = BloodType.A_POSITIVE,
                        Quantity = 5,
                        Money = new Money()
                        {
                            Amount = 5,
                            Currency = Currency.EUR
                        }
                    },
                    new TenderItem()
                    {
                        BloodType = BloodType.B_POSITIVE,
                        Quantity = 5,
                        Money = new Money()
                        {
                            Amount = 6,
                            Currency = Currency.EUR
                        }
                    }
                }
                };
            }
        }
    }
}
