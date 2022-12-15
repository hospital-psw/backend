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
        public string PostalCode { get; set; }

        public Address(string city, string street, string postalCode)
        {
            City = city;
            Street = street;
            PostalCode = postalCode;
        }

        public static Address Create(string city, string street, string postalCode) 
        {
            return new Address(city, street, postalCode);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

    }
}
