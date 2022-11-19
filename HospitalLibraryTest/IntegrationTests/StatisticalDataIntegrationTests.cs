namespace HospitalLibraryTest.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /*
     Idea:
    -FE sends request getStatistic
    -Full response object should look like: 
    data = {
        chart1: {
            Array<int> size=12 numberOfAppointments
        }
        chart2: {
            Array<string> size=len(doctors) namesOfDoctors
            Array<int> size=len(doctors) numberOfPatientsPerDoctor
        }
        chart3m: {
            '60+', '45-60', '35-45', '25-35', '15-25', "0-15"
            Array<int> size=6 numberOfPatientsThisAge
        }
        chart3f: {
            '60+', '45-60', '35-45', '25-35', '15-25', "0-15"
            Array<int> size=6 numberOfPatientsThisAge
        }
        chart4: {
            'Patient', 'Manager', 'General doctor', 'Cardiologist', 'Neurologist'
            Array<int> size=numberOfRoles usersPerRole
        }
    }
     */


    internal class StatisticalDataIntegrationTests
    {
    }
}
