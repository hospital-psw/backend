namespace IntegrationAPITest.MockData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using Microsoft.VisualBasic;

    public class TenderMockData
    {
        public static Tender Tender1
        {
            get
            {
                return new Tender()
                {
                    Status = TenderStatus.OPEN,
                    DueDate = DateTime.Now
                };
            }
        }
    }
}
