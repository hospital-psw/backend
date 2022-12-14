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
    public class FeedbackMessage : ValueObject
    {
        public string Message { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
        private FeedbackMessage(string message)
        {
            Message = message;
        }

        public static FeedbackMessage Create(string message)
        {
            return new FeedbackMessage(message);
        }
    }
}
