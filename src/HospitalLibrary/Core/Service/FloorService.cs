namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FloorService : BaseService<Floor>, IFloorService
    {
        public FloorService() : base() { }

        public FloorDetailsDTO GetDetails(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                FloorDetailsDTO floor = new FloorDetailsDTO(unitOfWork.FloorRepository.Get(id));
                return floor;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
