namespace HospitalLibrary.Core.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        TEntity Add(TEntity entity);

        bool Delete(int id);

        TEntity Update(TEntity entity);
    }
}
