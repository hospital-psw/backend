namespace HospitalLibrary.EventSourcingInfrastructure
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class EventSourcedAggregateRoot : Entity
    {
        public List<DomainEvent> Events { get; set; }
        public int Version { get; set; }  //sustinski brojac
        public EventSourcedAggregateRoot()
        {
            Events = new List<DomainEvent>();
        }
        public abstract void Apply(DomainEvent change);
    }
}
