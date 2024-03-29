﻿namespace HospitalLibrary.Core.Service.Examinations
{
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using IdentityServer4.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrescriptionService : BaseService<Prescription>, IPrescriptionService
    {
        ILogger<PrescriptionService> _logger;

        public PrescriptionService(IUnitOfWork unitOfWork, ILogger<PrescriptionService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        public Prescription Add(int medicamentId, string description, DateRange dateRange)
        {
            try
            {
                Medicament medicament = _unitOfWork.MedicamentRepository.Get(medicamentId);
                Prescription prescription = new Prescription(medicament, description, dateRange);
                _unitOfWork.PrescriptionRepository.Add(prescription);
                _unitOfWork.Save();
                return prescription;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PrescriptionService in Add {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public List<Prescription> AddMultiple(List<NewPrescriptionDto> dtos)
        {
            try
            {
                List<Prescription> prescriptions = new List<Prescription>();
                dtos.ForEach(x => prescriptions.Add(Add(x.MedicamentId, x.Description, new DateRange(x.From, x.To))));
                return prescriptions;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PrescriptionService in AddMultiple {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Prescription Get(int id)
        {
            try
            {
                Prescription prescription = _unitOfWork.PrescriptionRepository.Get(id);
                return prescription;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PrescriptionService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<Prescription> GetAll()
        {
            try
            {
                return _unitOfWork.PrescriptionRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PrescriptionService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Prescription> GetPrescriptionsBySearchCriteria(List<string> criteriasList)
        {
            try
            {
                List<Prescription> prescriptions = new List<Prescription>();
                foreach (string el in criteriasList)
                {
                    List<Prescription> p = _unitOfWork.PrescriptionRepository.GetPrescriptionsBySearchCriteria(el).ToList();
                    if (!p.IsNullOrEmpty())
                    {
                        prescriptions.AddRange(p);
                    }
                }
                return prescriptions.Distinct();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PrescriptionService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
