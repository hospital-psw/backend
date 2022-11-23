namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEquipmentRepository : IBaseRepository<Equipment>
    {
        IEnumerable<Equipment> GetEquipments();
        Equipment GetEquipment(EquipmentType type, Room room);
        Equipment Create(Equipment equipment);
        int Save();
    }
}
