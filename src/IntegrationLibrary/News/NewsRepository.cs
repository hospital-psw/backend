namespace IntegrationLibrary.News
{
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Settings;
    using System;
    using System.Collections.Generic;

    public class NewsRepository : INewsRepository
    {
        private IntegrationDbContext _context;
        public NewsRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Add(News entity)
        {
            throw new NotImplementedException();
        }

        public News Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(News entity)
        {
            throw new NotImplementedException();
        }
    }
}
