namespace IntegrationLibrary.Core
{
    using System.Collections.Generic;

    public interface IService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);
    }
}
