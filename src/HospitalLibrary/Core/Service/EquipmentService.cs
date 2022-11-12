namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EquipmentService : BaseService<Equipment>, IEquipmentService
    {
        private readonly ILogger<Equipment> _logger;

        public EquipmentService( ILogger<Equipment> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public List<Equipment> GetForRoom(int roomId)
        {
            try
            {
                IEnumerable<Equipment> all =  _unitOfWork.EquipmentRepository.GetEquipments();
                List<Equipment> equipment = new List<Equipment>();
                foreach(Equipment eq in all)
                {
                    if(eq.Room.Id == roomId)  equipment.Add(eq);
                }
                return equipment;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in EquipmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

    }
}
