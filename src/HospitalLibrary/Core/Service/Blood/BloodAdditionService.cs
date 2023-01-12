namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Blood.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAdditionService : BaseService<BloodAddition>, IBloodAdditionService
    {

        private readonly ILogger<BloodAddition> _logger;

        public BloodAdditionService(ILogger<BloodAddition> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public BloodAddition Add(BloodType bloodType, int amount)
        {
            DateTime date = DateTime.Now;
            BloodAddition bloodAddition = new BloodAddition(date, bloodType, amount);

            _unitOfWork.BloodAdditionRepository.Add(bloodAddition);
            return bloodAddition;
        }


        public IEnumerable<BloodAddition> GetByBloodType(BloodType bloodType)
        {
            return _unitOfWork.BloodAdditionRepository.GetByBloodType(bloodType);
        }

        BloodAddition IBloodAdditionService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
