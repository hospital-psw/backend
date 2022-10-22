namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        private readonly HospitalDbContext _context;

        public FeedbackRepository(HospitalDbContext context) : base(context)
        {
            _context = context; 
        }

        public IEnumerable<Feedback> GetAllPublic() 
        {
            return _context.Feedback.Where(x => x.Public && !x.Deleted);
        }

        public IEnumerable<Feedback> GetAllPrivate() 
        {
            return _context.Feedback.Where(x => !x.Public && !x.Deleted);
        }

        public IEnumerable<Feedback> GetAllAnonymous() 
        {
            return _context.Feedback.Where(x => x.Anonymous);
        }

        public IEnumerable<Feedback> GetAllIdentified() 
        {
            return _context.Feedback.Where(x => !x.Anonymous);
        }
    }
}
