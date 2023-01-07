namespace HospitalLibrary.Core.Repository.Examinations
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using HospitalLibrary.Core.Model.Events;

    public class ExaminationEventRepository : BaseRepository<DomainEvent>, IExaminationEventRepository
    {
        public ExaminationEventRepository(HospitalDbContext context) : base(context)
        {
            
        }

        public DomainEvent AddEvent(DomainEvent @event)
        {
            When((dynamic)@event);
            return @event;
        }

        private void When(ExaminationStarted examStarted)
        {
            HospitalDbContext.ExaminationStartedEvents.Add(examStarted);
        }

        private void When(ExaminationFinished examFinished)
        {
            HospitalDbContext.ExaminationFinishedEvents.Add(examFinished);
        }

        private void When(PrescriptionCreated prescCreated)
        {
            HospitalDbContext.PrescriptionCreatedEvents.Add(prescCreated);
        }

        private void When(PrescriptionRemoved prescRemoved)
        {
            HospitalDbContext.PrescriptionRemovedEvents.Add(prescRemoved);
        }

        private void When(DescriptionCreated descCreated)
        {
            HospitalDbContext.DescriptionCreatedEvents.Add(descCreated);
        }

        private void When(SymptomsChanged symptomsChanged)
        {
            HospitalDbContext.SymptomsChangedEvents.Add(symptomsChanged);
        }

        private void When(ExaminationEvent examEvent)
        {
            HospitalDbContext.ExaminationEvents.Add(examEvent);
        }
    }
}
