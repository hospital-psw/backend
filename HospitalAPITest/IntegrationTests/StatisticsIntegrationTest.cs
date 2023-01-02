namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.Statistics;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Util;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatisticsIntegrationTest : BaseIntegrationTest
    {
        public StatisticsIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static StatisticalController SetupController(IServiceScope serviceScope)
        {
            return new StatisticalController(serviceScope.ServiceProvider.GetRequiredService<IStatisticsService>());
        }

        [Fact]
        public void Gets_Correct_Statistics()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetStats()).Value as StatisticsDTO;

            StatisticsDTO expected = getExpectedResults();

            Assert.Equal(expected.Chart1, result.Chart1);
            Assert.Equal(expected.Chart2Names, result.Chart2Names);
            Assert.NotNull(result.Chart2Values);
            Assert.Equal(expected.Chart3Male, result.Chart3Male);
            Assert.Equal(expected.Chart3Female, result.Chart3Female);
            Assert.Equal(expected.Chart4, result.Chart4);
        }

        public StatisticsDTO getExpectedResults()
        {
            StatisticsDTO expected = new StatisticsDTO();
            expected.Chart1 = ListFactory.CreateList(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0);
            expected.Chart2Names = ListFactory.CreateList("Galina Gavanski", "Lik Beson", "Djankarlo Rapacoti");
            expected.Chart3Male = ListFactory.CreateList(0, 2, 0, 0, 0, 2);
            expected.Chart3Female = ListFactory.CreateList(0, 0, 0, 0, 0, 1);
            expected.Chart4 = ListFactory.CreateList(5, 1, 1, 1);

            return expected;
        }

        [Fact]
        public void Gets_Correct_Vacation_Statistics()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetVacationStatistics(6)).Value as List<int>;

            List<int> expected = ListFactory.CreateList(10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Gets_Correct_Doctor_Yearly_Booking_Statistics()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetYearlyDoctorAppointmentsStatistics(4, 2022)).Value as List<int>;

            List<int> expected = ListFactory.CreateList(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Gets_Correct_Doctor_Monthly_Booking_Statistics()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetMonthlyDoctorAppointmentsStatistics(7, 12, 2022)).Value as List<int>;

            List<int> expected = new() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Gets_average_scheduling_duration()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetAverageSchedulingDuration()).Value as List<double>;
            List<double> expected = new() { 25 };

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Gets_average_scheduling_duration_by_groups()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetAverageSchedulingDurationByGroups()).Value as List<double>;
            List<double> expected = new() { 1, 0, 0, 0, 0 };

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Gets_Correct_Number_Of_Views_For_Each_Renovation_Step()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetNumberOfViewsForEachStep()).Value as List<double>;

            List<double> expected = new() { 1, 1, 0, 0, 0, 0 };

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Gets_Correct_Number_Of_Steps_According_To_Renovation_Type()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetNumberOfStepsAccordingToRenovationType()).Value as List<double>;

            List<double> expected = new() { 0, 0 };

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Gets_Correct_Number_Of_Average_Steps_Renovation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetAverageNumberOfRenovationSteps()).Value as List<double>;

            List<double> expected = new() { 0, 0 };

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
