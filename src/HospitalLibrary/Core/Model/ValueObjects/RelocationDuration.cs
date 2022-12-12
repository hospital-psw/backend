namespace HospitalLibrary.Core.Model.ValueObjects
{
    using CSharpFunctionalExtensions;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Owned]
    public class RelocationDuration:ValueObject
    {
        public int Duration { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        private RelocationDuration(int duration)
        {
            Duration = duration;
        }
        public static RelocationDuration Create(int duration)
        {
            return new RelocationDuration(duration);
        }
    }
}
