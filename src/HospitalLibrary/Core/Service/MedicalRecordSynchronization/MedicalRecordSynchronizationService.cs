namespace HospitalLibrary.Core.Service.MedicalRecordSynchronization
{
    using OpenQA.Selenium.Html5;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalRecordSynchronizationService
    {

        private readonly HttpClient _httpClient;
        public MedicalRecordSynchronizationService()
        {
            _httpClient = new HttpClient();
        }


        public virtual async Task<string> GetPreviousMedicalRecord(int patientId)
        {

            string url = $"https://ZdravoBolnica/record/{patientId}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string medicalRecord = await response.Content.ReadAsStringAsync();
                return medicalRecord;
            }
            return "Failed to retrieve medical record";
        }
        

    }
}
