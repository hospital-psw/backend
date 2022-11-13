namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodExpenditureService : BaseService<BloodExpenditure>, IBloodExpenditureService
    {

        private readonly ILogger<BloodExpenditure> _logger;

        public BloodExpenditureService(ILogger<BloodExpenditure> logger) : base()
        {
            _logger = logger;
        }


        public override BloodExpenditure Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.BloodExpenditureRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<BloodExpenditure> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.BloodExpenditureRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }


        public void Create(CreateExpenditureDTO expendituredto)
        {
            try 
            { 
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Doctor doctor = unitOfWork.DoctorRepository.Get(expendituredto.DoctorId);
                BloodType bloodType = expendituredto.BloodType;
                int amount = expendituredto.Amount;
                string reason = expendituredto.Reason;
                DateTime date = expendituredto.Date;
                BloodExpenditure bloodExpenditure = new BloodExpenditure(doctor,bloodType,amount,reason,date);
                unitOfWork.BloodExpenditureRepository.Add(bloodExpenditure);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
            }

        }

        public BloodExpenditure Update(BloodExpenditure bloodExpenditure)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                 unitOfWork.BloodExpenditureRepository.Update(bloodExpenditure);
                return bloodExpenditure;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Update(CreateExpenditureDTO expendituredto)
        {
            throw new NotImplementedException();
        }

        BloodExpenditure IBloodExpenditureService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
