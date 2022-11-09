namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Service.Core;

    public class BloodUnitTherapyController : BaseController<BloodUnitTherapy>
    {

        private readonly IBloodUnitTherapyService _bloodUnitTherapyService;

        public BloodUnitTherapyController(IBloodUnitTherapyService bloodUnitTherapyService)
        {
            _bloodUnitTherapyService = bloodUnitTherapyService;
        }
    }
}
