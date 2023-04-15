namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using Grpc.Core;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.News;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.News;
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Settings;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using System.Net;

    public class NewsIntegrationTest : BaseIntegrationTest
    {
        public NewsIntegrationTest(TestDatabaseFactory factory) : base(factory) { }

        private static NewsController SetupController(IServiceScope serviceScope)
        {
            return new NewsController(serviceScope.ServiceProvider.GetRequiredService<INewsService>(), serviceScope.ServiceProvider.GetRequiredService<IMapper>());
        }

        private IntegrationDbContext SetupContext(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<IntegrationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.News.Add(NewsMockData.PendingNews);
            context.News.Add(NewsMockData.ArchivedNews);
            context.News.Add(NewsMockData.PublishedNews);
            context.SaveChanges();
            return context;
        }


        [Theory]
        [ClassData(typeof(ManagerNewsData))]
        public void Get_news(int newsId, string expectedTitle,NewsStatus expectedStatus)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.Get(newsId)).Value as ManagerNewsDTO;

            result.ShouldNotBeNull();
            result.Title.ShouldBe(expectedTitle);
            result.Status.ShouldBe(expectedStatus);
        }

        [Fact]
        public void Get_all_news()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.GetAll()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public void Get_news_for_invalid_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((StatusCodeResult)controller.Get(10));

            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void Get_archived()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            SetupContext(scope);

            var result = ((OkObjectResult)controller.GetArchived()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void Get_published()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.GetPublished()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void Get_pending()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.GetPending()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }


        [Theory]
        [ClassData(typeof(PublishNewsData))]
        public void Publish_news(int newsId, int expectedStatusCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((StatusCodeResult)controller.Publish(newsId));

            result.StatusCode.ShouldBe(expectedStatusCode);
        }

        [Fact]
        public void Archive_news()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((StatusCodeResult)controller.Archive(3));

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [Fact]
        public void Create_News()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var news = new UserNewsDTO
            {
                DateCreated = DateTime.Now,
                Title = "Test title",
                Text = "Test body",
                Image = "Test image",
            };
            var result = (StatusCodeResult)controller.Create(news);

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        class ManagerNewsData : TheoryData<int, string,NewsStatus>
        {
            public ManagerNewsData()
            {
                Add(1, "Akcija prikupljanja krvi na stadionu Karadjordje!",NewsStatus.PENDING);
                Add(2, "Akcija prikupljanja krvi na stadionu Rajko Mitic!",NewsStatus.ARCHIVED);
                Add(3, "Akcija prikupljanja krvi na stadionu JNA!",NewsStatus.PUBLISHED);
            }
        }

        class PublishNewsData : TheoryData<int, int>
        {
            public PublishNewsData()
            {
                Add(1, StatusCodes.Status200OK);
                Add(2, StatusCodes.Status200OK);
                Add(3, StatusCodes.Status400BadRequest);
            }
        }
    }
}
