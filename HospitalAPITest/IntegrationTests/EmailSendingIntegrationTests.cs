﻿namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Controllers.AppUsers;
    using HospitalAPI.Dto;
    using HospitalAPI.EmailServices;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmailSendingIntegrationTests : BaseIntegrationTest
    {
        public EmailSendingIntegrationTests(TestDatabaseFactory factory) : base(factory)
        {
        }
    }
}
