namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class VacationRequestsController
    {
        private IVacationRequestsService _vacationRequestsService;

        public VacationRequestsController(IVacationRequestsService vacationRequestsService)
        {
            _vacationRequestsService = vacationRequestsService;
        }

        public ObjectResult GetAllPending()
        {
            throw new NotImplementedException();
        }
    }
}
