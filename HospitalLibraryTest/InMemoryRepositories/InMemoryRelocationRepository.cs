namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryRelocationRepository : IRelocationRepository
    {
        public void Add(RelocationRequest entity)
        {
            throw new NotImplementedException();
        }

        public RelocationRequest Create(RelocationRequest request)
        {
            throw new NotImplementedException();
        }

        public RelocationRequest Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RelocationRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<RelocationRequest> GetFinishedRelocations()
        {
            throw new NotImplementedException();
        }

        public List<RelocationRequest> GetScheduledRelocationsForRoom(int roomId)
        {
            List<RelocationRequest> relocations = new List<RelocationRequest>();
            return relocations;
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public void Update(RelocationRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
