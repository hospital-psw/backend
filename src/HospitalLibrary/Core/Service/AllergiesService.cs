namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AllergiesService : BaseService<Allergies>, IAllergiesService
    {
        private readonly ILogger<Allergies> _logger;
        public AllergiesService(ILogger<Allergies> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }
        public Allergies Add(Allergies allergy)
        {
            try
            {
                _unitOfWork.AllergiesRepository.Add(allergy);
                _unitOfWork.Save();

                return allergy;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AllergiesService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }



        public override IEnumerable<Allergies> GetAll()
        {
            try
            {
                return _unitOfWork.AllergiesRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AllergiesService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

    }
}
