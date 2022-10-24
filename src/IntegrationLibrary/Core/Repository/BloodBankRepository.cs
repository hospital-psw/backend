using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.Core;
using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository
{
    public class BloodBankRepository : BaseRepository<BloodBank>, IBloodBankRepository
    {
        public BloodBankRepository(IntegrationDbContext context) : base(context)
        {
        }
    }
}