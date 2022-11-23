namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Blood.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitService : BaseService<BloodUnit>, IBloodUnitService
    {

        private readonly ILogger<BloodUnit> _logger;

        public BloodUnitService(ILogger<BloodUnit> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override BloodUnit Get(int id)
        {
            try
            {
                return _unitOfWork.BloodUnitRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public int GetAmountForSpecificBloodType(BloodType bloodType)
        {

            return _unitOfWork.BloodUnitRepository.GetAmountForSpecificBloodType(bloodType);

        }

        public BloodUnit GetByBloodType(BloodType bloodType)
        {
            return _unitOfWork.BloodUnitRepository.GetByBloodType(bloodType);
        }
    }
}

