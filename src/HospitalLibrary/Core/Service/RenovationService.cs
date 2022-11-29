namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationService : BaseService<RenovationRequest>, IRenovationService
    {
        public RenovationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public RenovationRequest Create(RenovationRequest entity)
        {
            try
            {
                return _unitOfWork.RenovationRepository.Create(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
