namespace IntegrationAPITest.MockData
{
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TenderMockData
    {
        public static Tender Tender1
        {
            get
            {
                return new Tender()
                {
                    Status = TenderStatus.OPEN,
                    DueDate = new DateTime(2022, 12, 01)
                };
            }
        }
    }
}
