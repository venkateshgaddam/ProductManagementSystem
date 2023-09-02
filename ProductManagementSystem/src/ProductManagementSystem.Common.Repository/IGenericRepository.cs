﻿using Im.Common.Database.Interface;
using System;

namespace IM.Common.Repository.Sql
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="Carrier.CCP.Common.Database.Interface.IRepository{TEntity}" />
    public interface IGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

    }
}