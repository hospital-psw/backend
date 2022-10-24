namespace IntegrationLibrary.Core.Service
{
    using IntegrationLibrary.Core.Model;
    using IntegrationLibrary.Core.Repository;
    using IntegrationLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseService<TEntity> where TEntity : class
    {

        public BaseService()
        {
        }

        public virtual TEntity Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                return unitOfWork.GetRepository<TEntity>().Get(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                return unitOfWork.GetRepository<TEntity>().GetAll();

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                unitOfWork.GetRepository<TEntity>().Add(entity);
                unitOfWork.Save();

                return entity;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public virtual bool Delete(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                TEntity entity = unitOfWork.GetRepository<TEntity>().Get(id);

                (entity as Entity).Deleted = true;
                unitOfWork.GetRepository<TEntity>().Update(entity);
                unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                unitOfWork.GetRepository<TEntity>().Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}
