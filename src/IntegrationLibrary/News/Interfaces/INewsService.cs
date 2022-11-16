namespace IntegrationLibrary.News.Interfaces
{
    using IntegrationLibrary.Core;
    using System;

    public interface INewsService : IService<News>
    {
        String GetImageExtension(News entity);
        String GetImageData(News entity);
        void SaveImageToDisk(News entity);
    }
}
