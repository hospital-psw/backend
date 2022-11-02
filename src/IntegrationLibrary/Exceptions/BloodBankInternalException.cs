namespace IntegrationLibrary.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodBankInternalException : Exception
    {
        public BloodBankInternalException(String message) : base(message)
        {

        }
    }
}
