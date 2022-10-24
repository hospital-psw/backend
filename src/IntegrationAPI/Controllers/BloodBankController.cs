namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.Core.Model;
    using IntegrationLibrary.Core.Service;
    using IntegrationLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodBankController : BaseController<BloodBank>
    {
        private readonly IBloodBankService _bloodBankService; 

        public BloodBankController(IBloodBankService bloodBankService)
        {
            _bloodBankService = bloodBankService;   
        }
    }
}
