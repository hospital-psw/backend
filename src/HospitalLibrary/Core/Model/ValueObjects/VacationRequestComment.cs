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
    public class VacationRequestComment : ValueObject
    {
        public string Comment { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        private VacationRequestComment(string comment)
        {
            Comment = comment;
        }

        public static VacationRequestComment Create(string comment)
        {
            return new VacationRequestComment(comment);
        }
    }
}
