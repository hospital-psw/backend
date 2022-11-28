namespace HospitalAPITest.IntegrationTests
{
    using AutoMapper;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Auth;
    using HospitalAPI.EmailServices;
    using HospitalAPI.TokenServices;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ActivationIntegrationTest : BaseIntegrationTest
    {
        public ActivationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static AuthController SetupController(IServiceScope scope)
        {
            return new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),
                scope.ServiceProvider.GetRequiredService<IMapper>(), scope.ServiceProvider.GetRequiredService<ITokenService>(),
                scope.ServiceProvider.GetRequiredService<IEmailService>());
        }
        [Fact]
        public void Send_activation_email_after_registration()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            RegisterDTO rDto = new RegisterDTO
            {
                FirstName = "Tamara",
                LastName = "Krgovic",
                Email = "nekiemail@gmail.com",
                DateOfBirth = new DateTime(),
                Male = false,
                Password = "123",
                ConfirmPassword = "123"
            };

            var result = controller.Register(rDto);





        }
    }
}
