namespace IntegrationLibrary.News
{
    using IntegrationLibrary.News.Interfaces;
    using System.Collections.Generic;
    using System;

    public class NewsService : INewsService
    {
        public News Create(News entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public News Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<News> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public News Update(News entity)
        {
            throw new System.NotImplementedException();
        }

        string INewsService.GetImageExtension(News entity)
        {
            throw new NotImplementedException();
        }

        string INewsService.GetImageData(News entity)
        {
            throw new NotImplementedException();
        }

        void INewsService.SaveImageToDisk(News entity)
        {
            throw new NotImplementedException();
        }
    }
}
