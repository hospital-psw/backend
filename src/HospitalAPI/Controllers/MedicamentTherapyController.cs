namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Service.Core;

    public class MedicamentTherapyController : BaseController<MedicamentTherapy>
    {

        private readonly IMedicamentTherapyService _medicamentTherapyService;

        public MedicamentTherapyController(IMedicamentTherapyService medicamentTherapyService)
        {
            _medicamentTherapyService = medicamentTherapyService;
        }
    }
}
