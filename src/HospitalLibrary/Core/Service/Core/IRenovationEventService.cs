﻿namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRenovationEventService : IBaseService<RenovationEvent>
    {
        DomainEvent Execute(RenovationEvent evt);
    }
}
