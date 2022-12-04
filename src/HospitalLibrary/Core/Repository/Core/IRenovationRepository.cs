﻿namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRenovationRepository : IBaseRepository<RenovationRequest>
    {
        RenovationRequest Create(RenovationRequest request);
        public List<RenovationRequest> GetAll();
        public int Save();
    }
}
