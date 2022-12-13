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
    public class RelocationQuantity : ValueObject
    {
        public int Quantity { get; private set; }

        private RelocationQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public static RelocationQuantity Create(int quantity)
        {
            return new RelocationQuantity(quantity);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
