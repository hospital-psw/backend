﻿namespace IntegrationServices.ReportService.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateRangeDTO
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateRangeDTO() { }
        public DateRangeDTO(DateTime from, DateTime to) {
            From = from; 
            To = to;
        }
    }
}
