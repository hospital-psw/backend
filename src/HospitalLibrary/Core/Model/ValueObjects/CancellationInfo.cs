namespace HospitalLibrary.Core.Model.ValueObjects
{
    using CSharpFunctionalExtensions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    [Owned]
    public class CancellationInfo : ValueObject
    {
        public DateTime Date { get; private set; }
        public int CanceledBy { get; private set; }

        public CancellationInfo(DateTime date, int canceledBy)
        {
            Date = date;
            CanceledBy = canceledBy;    
        }

        public static CancellationInfo Create(DateTime date, int canceledBy) 
        {
            return new CancellationInfo(date, canceledBy);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
