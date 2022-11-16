namespace IntegrationLibraryTest.UnitTests
{
    using IntegrationLibrary.Core;
    using IntegrationLibrary.News;
    using IntegrationLibrary.News.Interfaces;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewsUnitTest
    {
        [Fact]
        public void Extract_PNG_extension()
        {
            var news = new News
            {
                Title = "Title",
                Text = "Body",
                Image = "png;IMAGE_DATA",
                Status = NewsStatus.PENDING
            };
            var logger = new Mock<ILogger<News>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var service = new NewsService(logger.Object, unitOfWork.Object);

            var result = service.GetImageExtension(news);

            result.ShouldBe("png");
        }

        [Fact]
        public void Extract_image_data()
        {
            var news = new News
            {
                Title = "Title",
                Text = "Body",
                Image = "png;IMAGE_DATA",
                Status = NewsStatus.PENDING
            };
            var logger = new Mock<ILogger<News>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var service = new NewsService(logger.Object, unitOfWork.Object);

            var result = service.GetImageData(news);

            result.ShouldBe("IMAGE_DATA");
        }
    }
}
