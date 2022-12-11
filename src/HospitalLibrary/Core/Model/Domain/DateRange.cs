namespace HospitalLibrary.Core.Model.Domain
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Owned]
    public class DateRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateRange() { }

        public DateRange(DateTime from, DateTime to)
        {
            //if (From >= To) { throw new Exception("Invalid Date Range Exception, Start date should be before End"); }
            From = from;
            To = to;
        }

        public bool InRange(DateTime date)
        {
            return From <= date && To <= date;
        }

        public bool Overlaps(DateRange date)
        {
            return !(To <= date.From || From >= date.To);
        }

        public override bool Equals(object obj)
        {
            DateRange dateRange = obj as DateRange;
            return From == dateRange.From && To == dateRange.To;
        }

        public static bool operator ==(DateRange one, DateRange two)
        {
            return one.Equals(two);
        }

        public static bool operator !=(DateRange one, DateRange two)
        {
            return !one.Equals(two);
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() ^ To.GetHashCode();
        }
    }
}
