namespace IntegrationLibraryTest.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationLibrary.News;
    using Shouldly;
    using Moq;
    using IntegrationLibrary.News.Interfaces;

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
            var service = new NewsService();

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
            var service = new NewsService();

            var result = service.GetImageData(news);

            result.ShouldBe("IMAGE_DATA");
        }
    }
}
