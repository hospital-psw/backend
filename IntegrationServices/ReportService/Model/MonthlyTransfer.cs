namespace IntegrationServices.ReportService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MonthlyTransfer
    {
        public DateTime DateTime { get; set; }
        public int APlus { get; set; }
        public int BPlus { get; set; }
        public int AMinus { get; set; }
        public int BMinus { get; set; }
        public int OPlus { get; set; }
        public int OMinus { get; set; }
        public int ABPlus { get; set; }
        public int ABMinus { get; set; }
    }
}
