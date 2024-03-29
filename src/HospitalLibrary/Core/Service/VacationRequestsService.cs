﻿namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.VacationRequest;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using IdentityServer4.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequestsService : BaseService<VacationRequest>, IVacationRequestsService
    {
        private readonly ILogger<VacationRequest> _logger;
        private new readonly IUnitOfWork _unitOfWork;

        public VacationRequestsService(ILogger<VacationRequest> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<VacationRequest> GetAllPending()
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.GetAllPending();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public void HandleVacationRequest(VacationRequestStatus status, int id, string managerComment)
        {
            VacationRequest request = _unitOfWork.VacationRequestsRepository.Get(id);
            request.Status = status;
            request.ManagerComment = managerComment;
            _unitOfWork.VacationRequestsRepository.Update(request);
            _unitOfWork.VacationRequestsRepository.Save();
        }

        public VacationRequest Create(NewVacationRequestDto dto)
        {
            try
            {
                ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(dto.DoctorId);
                List<Appointment> scheduledAppointments = _unitOfWork.AppointmentRepository.GetAppointmentsInDateRangeDoctor(dto.DoctorId, dto.From, dto.To).ToList();
                VacationRequest request = null;

                if (scheduledAppointments.IsNullOrEmpty())
                {
                    request = new VacationRequest(doctor, dto.From, dto.To, dto.Status, dto.Comment, dto.Urgent, "");
                    _unitOfWork.VacationRequestsRepository.Add(request);
                    _unitOfWork.Save();
                    return request;
                }

                if (!dto.Urgent)
                {
                    return null;
                }

                request = CreateEmergencyRequest(scheduledAppointments, doctor, dto);

                if (request == null) return null;

                _unitOfWork.VacationRequestsRepository.Add(request);
                _unitOfWork.Save();

                return request;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public VacationRequest CreateEmergencyRequest(List<Appointment> appointments, ApplicationDoctor doctor, NewVacationRequestDto dto)
        {

            if (!SubstituteDoctors(appointments))
            {
                return null;
            }

            return new VacationRequest(doctor, dto.From, dto.To, dto.Status, dto.Comment, dto.Urgent, "");
        }

        public bool SubstituteDoctors(List<Appointment> appointments)
        {
            foreach (Appointment a in appointments)
            {
                var substitution = GetAvailableDoctorOfSameSpecialization(a);
                if (substitution == null) return false;
                a.Doctor = substitution;
                _unitOfWork.AppointmentRepository.Update(a);
            }

            return true;
        }

        public ApplicationDoctor GetAvailableDoctorOfSameSpecialization(Appointment appointment)
        {
            var sameSpecializationDoctors = _unitOfWork.ApplicationDoctorRepository.GetOtherSpecializationDoctors(appointment.Doctor.Specialization, appointment.Doctor.Id).ToList();
            var availableDoctors = sameSpecializationDoctors.Where(x => _unitOfWork.AppointmentRepository.IsDoctorAvailable(x.Id, appointment.Date)).ToList();
            return availableDoctors.FirstOrDefault();
        }

        public IEnumerable<VacationRequest> GetAllRequestsByDoctorId(int doctorId)
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.GetAllRequestsByDoctorsId(doctorId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<VacationRequest> GetAllWaitingByDoctorId(int doctorId)
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.GetAllWaitingByDoctorId(doctorId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<VacationRequest> getAllApprovedByDoctorId(int doctorId)
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.GetAllApprovedByDoctorId(doctorId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<VacationRequest> GetAllRejectedByDoctorId(int doctorId)
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.GetAllRejectedByDoctorId(doctorId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public VacationRequest GetById(int vacationRequestId)
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.Get(vacationRequestId);
            }
            catch
            {
                return null;
            }
        }

        public void Delete(VacationRequest vacationRequest)
        {
            try
            {
                vacationRequest.Deleted = true;
                _unitOfWork.VacationRequestsRepository.Update(vacationRequest);
                _unitOfWork.Save();

            }
            catch (Exception) { }
        }
    }

}
