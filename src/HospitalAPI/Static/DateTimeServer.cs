namespace HospitalAPI.Static
{
    using System;

    public class DateTimeServer :IDateTimeServer
    {
        public DateTime Now => DateTime.Now;
    }
}
