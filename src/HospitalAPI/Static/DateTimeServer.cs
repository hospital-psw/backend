namespace HospitalAPI.Static
{
    using System;

    public static class DateTimeServer
    {
        private static Func<DateTime> _func;
        public static DateTime Now => _func();

        public static void Init(Func<DateTime> func)
        { 
            _func = func;
        }
    }
}
