namespace HospitalLibrary.EventSourcingInfrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DomainEvent
    {
        public Guid Id { get; private set; }

        public DomainEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }

    }
}
