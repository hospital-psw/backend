using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank
{
    public class BloodBankRepository : BaseRepository<BloodBank>, IBloodBankRepository
    {
        public BloodBankRepository(IntegrationDbContext context) : base(context)
        {
        }
    }
}
