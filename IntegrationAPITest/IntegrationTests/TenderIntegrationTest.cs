namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.Tender;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using IntegrationLibrary.Tender.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using System;
    using System.Net;

    public class TenderIntegrationTest : BaseIntegrationTest
    {
        public TenderIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static TenderController SetupController(IServiceScope serviceScope)
        {
            return new TenderController(serviceScope.ServiceProvider.GetRequiredService<ITenderService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IMapper>());
        }

        private IntegrationDbContext SetupContext(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<IntegrationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Tenders.Add(TenderMockData.Tender1);
            context.Tenders.Add(TenderMockData.TenderClosed);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Get_1_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as GetTenderDTO;

            result.ShouldNotBeNull();
            result.Status.ShouldBe(TenderStatus.OPEN);
            result.DueDate.ShouldBe(DateTime.Now);
        }

        [Fact]
        public void MakeAnOffer_ShouldReturn200OK()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);
            MakeTenderOfferDTO offer = new MakeTenderOfferDTO()
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

            var result = ((OkObjectResult)controller.MakeAnOffer(1, offer)).Value as ViewTenderOfferDTO;

            result.ShouldNotBe(null);
            result.Items.ShouldNotBe(null);
            result.Items.Count.ShouldBe(2);
            result.Items[0].Quantity.ShouldBe(5);
            result.Items[0].Money.Amount.ShouldBe(5);
            result.Items[0].Money.Currency.ShouldBe(Currency.EUR);

        }

        [Fact]
        public void MakeAnOffer_PastExpirationDate_ShouldReturn400BadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((StatusCodeResult)controller.MakeAnOffer(2, TenderMockData.TenderOfferWithTwoItems));

            result.ShouldNotBe(null);
            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void MakeAnOffer_TenderNonExistant_ShouldReturn400BadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((StatusCodeResult)controller.MakeAnOffer(3, TenderMockData.TenderOfferWithTwoItems));

            result.ShouldNotBe(null);
            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
    }
}
