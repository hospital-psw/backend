namespace HospitalLibrary.Core.Infrastucture
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class EventSourcedAggregate : Entity
    {
        public List<DomainEvent> Changes { get; private set; }
        public int Version { get; protected set; }

        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
            Version = 0;
        }

        public abstract void Apply(DomainEvent changes);

    }
}
