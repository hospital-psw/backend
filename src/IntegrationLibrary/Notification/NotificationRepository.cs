namespace IntegrationLibrary.Notification
{
    using IntegrationLibrary.Notification.Interfaces;
    using IntegrationLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NotificationRepository : INotificationRepository
    {

        private readonly DbContext _context;

        public NotificationRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public IntegrationDbContext IntegrationDbContext
        {
            get { return _context as IntegrationDbContext; }
        }

        public void Add(Notification entity)
        {
            _context.Set<Notification>().Add(entity);
        }

        public Notification Get(int id)
        {
            return _context.Set<Notification>().Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
        }

        public IEnumerable<Notification> GetAll()
        {
            return _context.Set<Notification>().Where(x => !x.Deleted).ToList();
        }

        public void Update(Notification entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
        }
    }
}
