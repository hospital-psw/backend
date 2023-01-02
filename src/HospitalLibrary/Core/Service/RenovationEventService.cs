namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Routing.Constraints;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationEventService : BaseService<RenovationEvent>, IRenovationEventService
    {
        public RenovationEventService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public DomainEvent Execute(RenovationEvent evt)
        {
            RenovationRequest request = null;
            if (evt.EventName.Equals("RENOVATION_TYPE_EVENT"))
            {
                request = _unitOfWork.RenovationRepository.Create(RenovationRequest.Create(evt.Type, null, DateTime.Now, 0));
                evt = new RenovationEvent(request.Id, DateTime.Now, evt.EventName, evt.Type);
                request.Delete();
            }
            else
            {
                request = _unitOfWork.RenovationRepository.Get(evt.AggregateId);
                if (evt.EventName.Equals("SCHEDULE_EVENT")) {
                    request.Undelete();
                }
                evt = new RenovationEvent(evt.AggregateId, DateTime.Now, evt.EventName, evt.Type);
              
            }
            request.SetType(evt);
            _unitOfWork.RenovationRepository.Update(request);
            _unitOfWork.RenovationEventRepository.Add(evt);
            _unitOfWork.Save();
            return evt;
        }

     
    }
}
