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
    public class Address : ValueObject
    {
        public string City { get; private set; }
        public string Street { get; private set; }

        public Address(string city, string street)
        {
            City = city;
            Street = street;
        }

        public static Address Create(string city, string street) 
        {
            return new Address(city, street);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

    }
}
