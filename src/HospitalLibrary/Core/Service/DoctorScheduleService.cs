namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorScheduleService : BaseService<DoctorSchedule>, IDoctorScheduleService
    {
        private ILogger<DoctorSchedule> _logger;

        public DoctorScheduleService(ILogger<DoctorSchedule> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override DoctorSchedule Get(int id)
        {
            try
            {
                return _unitOfWork.DoctorScheduleRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorScheduleService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<DoctorSchedule> GetAll()
        {
            try
            {
                return _unitOfWork.DoctorScheduleRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorScheduleService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
