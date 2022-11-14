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


        public int GetAmountForSpecificBloodType(BloodType bloodType)
        {

            return _unitOfWork.BloodUnitRepository.GetAmountForSpecificBloodType(bloodType);

        }
    }
}

