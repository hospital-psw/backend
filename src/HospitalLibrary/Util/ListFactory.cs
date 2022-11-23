namespace HospitalLibrary.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ListFactory
    {
        public static List<T> CreateList<T>(params T[] values)
        {
            return new List<T>(values);
        }
    }
}
