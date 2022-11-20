namespace IntegrationLibrary.News.Interfaces
{
    using IntegrationLibrary.Core;
    using System.Collections.Generic;

    public interface INewsService : IService<News>
    {
        IEnumerable<News> GetArchived();
        IEnumerable<News> GetPublished();
        IEnumerable<News> GetPending();
        bool Publish(int id);
        bool Archive(int id);
        string GetImageExtension(News entity);
        string GetImageData(News entity);
        void SaveImageToDisk(News entity);
    }
}
