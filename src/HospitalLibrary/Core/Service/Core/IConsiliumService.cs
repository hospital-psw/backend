namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IConsiliumService
    {

        Consilium Schedule(Consilium consilium);
        Consilium Get(int consiliumId);
        IEnumerable<Consilium> GetAll();
        List<Consilium> GetAllForRoom(int roomId);

    }
}
