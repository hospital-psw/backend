namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
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

        public BloodExpenditureService(ILogger<BloodExpenditure> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }


        public override BloodExpenditure Get(int id)
        {
            try
            {

                return _unitOfWork.BloodExpenditureRepository.Get(id);
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

                return _unitOfWork.BloodExpenditureRepository.GetAll();
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

                Doctor doctor = _unitOfWork.DoctorRepository.Get(expendituredto.DoctorId);
                BloodType bloodType = expendituredto.BloodType;
                int amount = expendituredto.Amount;
                string reason = expendituredto.Reason;
                DateTime date = expendituredto.Date;
                BloodExpenditure bloodExpenditure = new BloodExpenditure(doctor, bloodType, amount, reason, date);
                _unitOfWork.BloodExpenditureRepository.Add(bloodExpenditure);
                _unitOfWork.Save();

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

                _unitOfWork.BloodExpenditureRepository.Update(bloodExpenditure);
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

        public CalculateDTO CalculateExpenditure(DateTime from,DateTime to)
        {
            IEnumerable<BloodExpenditure> bloodExpenditureList = GetAll();
            CalculateDTO retVal = new CalculateDTO();

            foreach(BloodExpenditure b in bloodExpenditureList)
            {
                if(b.Date>from && b.Date < to)
                {
                    retVal.totalSum += b.Amount;
                    if(b.BloodType == BloodType.A_PLUS)
                        retVal.APlusAmount +=b.Amount;
                    else if(b.BloodType == BloodType.A_MINUS)
                        retVal.AMinusAmount += b.Amount;
                    else if (b.BloodType == BloodType.B_PLUS)
                        retVal.BPlusAmount += b.Amount;
                    else if (b.BloodType == BloodType.B_MINUS)
                        retVal.BMinusAmount += b.Amount;
                    else if (b.BloodType == BloodType.AB_PLUS)
                        retVal.ABPlusAmount += b.Amount;
                    else if (b.BloodType == BloodType.AB_MINUS)
                        retVal.ABMinusAmount += b.Amount;
                    else if (b.BloodType == BloodType.O_PLUS)
                        retVal.OPlusAmount += b.Amount;
                    else if (b.BloodType == BloodType.O_MINUS)
                        retVal.OMinusAmount += b.Amount;
                }
            }
            return retVal;
        }
    }
}
