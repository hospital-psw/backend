namespace HospitalLibrary.Core.Repository.Examinations.Core
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IExaminationEventRepository : IBaseRepository<DomainEvent>
    {
        DomainEvent AddEvent(DomainEvent @event);
    }
}
