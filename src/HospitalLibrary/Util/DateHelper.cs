namespace HospitalLibrary.Util
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateHelper
    {
        public static string DateToString(DateTime date, string format = "dd/M/yyyy")
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string DateTimeToString(DateTime date, string format = "dd/M/yyyy HH:mm")
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
