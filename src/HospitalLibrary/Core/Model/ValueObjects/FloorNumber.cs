namespace HospitalLibrary.Core.Model.ValueObjects
{
    using CSharpFunctionalExtensions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Owned]
    public class FloorNumber : ValueObject
    {
        public int Number { get; private set; }

        private FloorNumber(int number)
        {
            Number = number;
        }

        public static FloorNumber Create(int number)
        {
            return new FloorNumber(number);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
