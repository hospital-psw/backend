namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRelocationRepository : IBaseRepository<RelocationRequest>
    {
        RelocationRequest Create(RelocationRequest request);
        List<RelocationRequest> GetScheduledRelocationsForRoom(int roomId);

        List<RelocationRequest> GetFinishedRelocations();

        int Save();

    }
}
