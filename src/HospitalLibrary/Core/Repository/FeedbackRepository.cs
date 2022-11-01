namespace HospitalLibrary.Core.Repository
{

    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
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

        public override IEnumerable<Feedback> GetAll()
        {
            return _context.Feedback.Include(x => x.Creator)
                                    .Where(x => !x.Deleted)
                                    .ToList();
        }

        public override Feedback Get(int id)
        {
            return _context.Feedback.Include(x => x.Creator)
                                    .Where(x => !x.Deleted && x.Id == id)
                                    .FirstOrDefault();
        }

        public IEnumerable<Feedback> GetAllPublic()
        {
            return GetAll().Where(x => x.Public).ToList();
        }

        public IEnumerable<Feedback> GetAllPrivate()
        {
            return GetAll().Where(x => !x.Public).ToList();
        }

        public IEnumerable<Feedback> GetAllAnonymous()
        {
            return GetAll().Where(x => x.Anonymous).ToList();
        }
        public IEnumerable<Feedback> GetAllIdentified()
        {
            return GetAll().Where(x => !x.Anonymous).ToList();
        }

       
        public IEnumerable<Feedback> GetAllDenied()
        {
            return GetAll().Where(x => x.Status == FeedbackStatus.DENIED).ToList();
        }

        public IEnumerable<Feedback> GetAllApproved()
        {
            return GetAll().Where(x => x.Status == FeedbackStatus.APPROVED).ToList();
        }


        public IEnumerable<Feedback> GetAllPending()
        {
            return GetAll().Where(x => x.Status == FeedbackStatus.PENDING).ToList();
        }
    }
}
