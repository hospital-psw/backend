namespace HospitalLibrary.Core.Service.Examinations
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SymptomService : BaseService<Symptom>, ISymptomService
    {
        private ILogger<SymptomService> _logger;

        public SymptomService(IUnitOfWork unitOfWork, ILogger<SymptomService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        public Symptom GetByName(string name)
        {
            try
            {
                return _unitOfWork.SymptomRepository.GetByName(name);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in SymptomService in GetByName {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
