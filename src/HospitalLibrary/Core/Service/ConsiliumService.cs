﻿namespace HospitalLibrary.Core.Service
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

    public class ConsiliumService : BaseService<Consilium>, IConsiliumService
    {
        private ILogger<Consilium> _logger;

        public ConsiliumService(ILogger<Consilium> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public Consilium Schedule(Consilium consilium)
        {
            try
            {
                consilium.DoctorsSchedule.ForEach(ds => ds.Consiliums.Add(consilium));
                _unitOfWork.ConsiliumRepository.Add(consilium);
                _unitOfWork.Save();
                return consilium;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ConsiliumService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Consilium Get(int id)
        {
            try
            {
                return _unitOfWork.ConsiliumRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ConsiliumService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<Consilium> GetAll()
        {
            try
            {
                return _unitOfWork.ConsiliumRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ConsiliumService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public List<Consilium> GetAllForRoom(int roomId)
        {
            List<Consilium> futureRequests = _unitOfWork.ConsiliumRepository.GetScheduledConsiliumsForRoom(roomId);
            if (futureRequests == null)
            {
                return new List<Consilium>();
            }
            return futureRequests;
        }

        public List<Consilium> GetAllForDoctor(int doctorId)
        {
            List<Consilium> consiliums = _unitOfWork.ConsiliumRepository.GetConsiliumsByDoctorId(doctorId).ToList();
            if (consiliums == null)
            {
                consiliums = new List<Consilium>();
            }

            return consiliums;
        }
    }
}
