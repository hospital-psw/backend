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
    public class AnamnesisDescription : ValueObject
    {

        public string DescriptionContent { get; private set; }

        public AnamnesisDescription(string descriptionContent)
        {
            DescriptionContent = descriptionContent;
        }
        public static AnamnesisDescription Create(string descriptionContent)
        {
            return new AnamnesisDescription(descriptionContent);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
