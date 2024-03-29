﻿using System.Collections.Generic;

namespace MBRepoCore.Repo.Abstractions
{
    /// <summary>
    /// Represent a base Generic repository
    /// </summary>
    public interface IBasicRepository
    {
        #region Get

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <returns></returns>
        List<TEntity> Get<TEntity>() where TEntity : class;

        /// <summary>
        /// Get One <b><see cref="TEntity"/></b> record, based on the primary key value
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="pkValue">The primary key value</param>
        /// <returns></returns>
        TEntity GetById<TEntity>(object pkValue) where TEntity : class;

        #endregion

        #region Add

        /// <summary>
        /// Add one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to add into</typeparam>
        /// <param name="record">The record to be added</param>
        void Add<TEntity>(TEntity record) where TEntity : class;

        /// <summary>
        /// Add a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">Entity to insert into</typeparam>
        /// <param name="records">Records to be inserted</param>
        void Add<TEntity>(List<TEntity> records) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// update one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to add into</typeparam>
        /// <param name="record">The record to be updated</param>
        void Update<TEntity>(TEntity record) where TEntity : class;

        #endregion

        #region Contains

        /// <summary>
        /// Check if <b><see cref="TEntity"/></b> contains an object
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns></returns>
        bool Contains<TEntity>(TEntity obj) where TEntity : class;

        #endregion

        #region Remove

        /// <summary>
        /// Remove One <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <typeparam name="TEntity">Entity to remove from</typeparam>
        /// <param name="record">The record to be removed</param>
        void Remove<TEntity>(TEntity record) where TEntity : class;

        /// <summary>
        /// Remove a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">Entity to delete from</typeparam>
        /// <param name="records">Records to be deleted</param>
        void Remove<TEntity>(List<TEntity> records) where TEntity : class;

        #endregion
    }

    /// <summary>
    /// Represent a base specific repository
    /// </summary>
    /// <typeparam name="TEntity">Entity to create repository for</typeparam>
    public interface IBasicRepository<TEntity> where TEntity : class
    {
        #region Get

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records
        /// </summary>
        List<TEntity> Get();

        /// <summary>
        /// Get One <b><see cref="TEntity"/></b> record, based on the primary key value
        /// </summary>
        /// <param name="pkValue">The primary key value</param>
        TEntity GetById(object pkValue);

        #endregion

        #region Add

        /// <summary>
        /// Add one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <param name="record">The record to be added</param>
        void Add(TEntity record);

        /// <summary>
        /// Add a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <param name="records">Records to be inserted</param>
        void Add(List<TEntity> records);

        #endregion

        #region Update

        /// <summary>
        /// update one <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <param name="record">The record to be updated</param>
        void Update(TEntity record);

        #endregion

        #region Contains

        /// <summary>
        /// Check if <b><see cref="TEntity"/></b> contains an object
        /// </summary>
        /// <param name="obj">The object to be looking for</param>
        bool Contains(TEntity obj);

        #endregion

        #region Remove

        /// <summary>
        /// Remove One <b><see cref="TEntity"/></b> record
        /// </summary>
        /// <param name="record">The record to be removed</param>
        void Remove(TEntity record);

        /// <summary>
        /// Remove a range of <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <param name="records">Records to be deleted</param>
        void Remove(List<TEntity> records);

        #endregion
    }
}