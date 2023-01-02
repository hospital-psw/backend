namespace HospitalLibrary.Core.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITenderService
    {
        public List<double> GetMoneyPerMonth(int year);
    }
}
