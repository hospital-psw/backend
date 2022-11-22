namespace IntegrationLibrary.News
{
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class NewsRepository : INewsRepository
    {
        private IntegrationDbContext _context;
        public NewsRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Add(News entity)
        {
            entity.Status = NewsStatus.PENDING;
            _context.Set<News>().Add(entity);
        }

        public News Get(int id)
        {
            return _context.Set<News>().Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
        }

        public IEnumerable<News> GetAll()
        {
            return _context.Set<News>().Where(x => !x.Deleted).ToList();
        }

        public void Update(News entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
        }
    }
}
