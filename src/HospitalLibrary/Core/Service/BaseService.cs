namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {

        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual TEntity Get(int id)
        {
            try
            {
                return _unitOfWork.GetRepository<TEntity>().Get(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _unitOfWork.GetRepository<TEntity>().GetAll();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                _unitOfWork.GetRepository<TEntity>().Add(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public virtual bool Delete(int id)
        {
            try
            {
                TEntity entity = _unitOfWork.GetRepository<TEntity>().Get(id);

                (entity as Entity).Deleted = true;
                _unitOfWork.GetRepository<TEntity>().Update(entity);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                _unitOfWork.GetRepository<TEntity>().Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
