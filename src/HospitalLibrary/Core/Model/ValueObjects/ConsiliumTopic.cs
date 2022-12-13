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
    public class ConsiliumTopic : ValueObject
    {
        public string Content { get; private set; }

        private ConsiliumTopic(string content) 
        {
            Content = content;
        }

        public static ConsiliumTopic Enter(string content)
        {
            return new ConsiliumTopic(content);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
