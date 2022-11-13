namespace IntegrationAPITest.IntegrationTests
{
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.News;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.News;
    using IntegrationLibrary.News.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;

    public class NewsIntegrationTest : BaseIntegrationTest
    {
        public NewsIntegrationTest(TestDatabaseFactory factory) : base(factory) { }

        private static NewsController SetupController(IServiceScope serviceScope)
        {
            return new NewsController(serviceScope.ServiceProvider.GetRequiredService<INewsService>());
        }

        [Fact]
        public void Get_1_ShouldReturnPending()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as ManagerNewsDTO;

            result.ShouldNotBeNull();
            result.Title.ShouldBe("Akcija prikupljanja krvi na stadionu Karadjordje!");
            result.Status.ShouldBe(NewsStatus.PENDING);
        }

        [Fact]
        public void Get_2_ShouldReturnArchived()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(2)).Value as ManagerNewsDTO;

            result.ShouldNotBeNull();
            result.Title.ShouldBe("Akcija prikupljanja krvi na stadionu Rajko Mitic!");
            result.Status.ShouldBe(NewsStatus.ARCHIVED);
        }

        [Fact]
        public void Get_3_ShouldReturnPublished()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(3)).Value as ManagerNewsDTO;

            result.ShouldNotBeNull();
            result.Title.ShouldBe("Akcija prikupljanja krvi na stadionu JNA!");
            result.Status.ShouldBe(NewsStatus.PUBLISHED);
        }

        [Fact]
        public void GetAll_ShouldReturnCount3()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public void Get_10_ShouldReturnNotFound()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((NotFoundObjectResult)controller.Get(10));
        }

        [Fact]
        public void GetArchived_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetArchived()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void GetPublished_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetPublished()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void GetPending_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetPending()).Value as List<ManagerNewsDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void Publish_1_ShouldReturnOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Publish(1)).Value as ManagerNewsDTO;

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Publish_2_ShouldReturnOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Publish(2)).Value as ManagerNewsDTO;

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Publish_3_ShouldReturnBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((BadRequestObjectResult)controller.Publish(3));
        }

        [Fact]
        public void Archive_3_ShouldReturnOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Publish(3));
        }


    }
}
