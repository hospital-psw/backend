﻿namespace HospitalLibrary.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
