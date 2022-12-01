﻿namespace HospitalLibrary.Core.Service.Examinations
{
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnamnesisService : BaseService<Anamnesis>, IAnamnesisService
    {

        private readonly ILogger<AnamnesisService> _logger;

        public AnamnesisService(IUnitOfWork unitOfWork, ILogger<AnamnesisService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override IEnumerable<Anamnesis> GetAll()
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetAll in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Anamnesis Get(int id)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Get in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Anamnesis> GetByDoctor(int doctorId)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetByDoctor(doctorId);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in GetByDoctor in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Anamnesis> GetByPatient(int patientId)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetByPatient(patientId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetByPatient in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Anamnesis> GetInDateRange(DateRange dateRange)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetInDateRange(dateRange);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetInDateRange in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
