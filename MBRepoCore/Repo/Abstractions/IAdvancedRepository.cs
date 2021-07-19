using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MBRepoCore.Interfaces;


namespace MBRepoCore.Repo.Abstractions
{
    /// <summary>
    /// Represent the advanced Generic repository
    /// </summary>
    public interface IAdvancedRepository
    {

        #region Get

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records, and load related selected entities records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="relatedEntitiesToBeLoaded">Entities to be load data from (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        List<TEntity> Get<TEntity>(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records that respects a set of conditions provided in the filter expression
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="filterExpression">One or set of conditions to be respected</param>
        List<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records that respects a set of conditions provided in the filter expression and load related selected entities records
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="filterExpression">One or set of conditions to be respected</param>
        /// <param name="relatedEntitiesToBeLoaded">Entities to be load data from (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        List<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        /// <summary>
        /// Get one <b><see cref="TEntity"/></b> record and load related selected entities
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="pkValue">Primary key value</param>
        /// <param name="relatedEntitiesToBeLoaded">Entities to be loaded (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        TEntity GetById<TEntity>(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// Update many <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <typeparam name="TEntity">Entity to update in</typeparam>
        /// <param name="filterExpression">The filter expression</param>
        /// <param name="updateAction">The update action</param>
        void Update<TEntity>(Expression<Func<TEntity, bool>> filterExpression, Action<TEntity> updateAction) where TEntity : class;

        /// <summary>
        /// Update one <b><see cref="TEntity"/></b> record, except selected properties
        /// </summary>
        /// <typeparam name="TEntity">Entity to update in</typeparam>
        /// <param name="record"><b><see cref="TEntity"/></b> record to be updated</param>
        /// <param name="propertiesToBeSkipped"><b><see cref="record"/></b>'s properties to be excluded/skipped from update</param>
        void UpdateExcept<TEntity>(TEntity record, Expression<Func<TEntity, object>> propertiesToBeSkipped) where TEntity : class;

        /// <summary>
        /// Update one <b><see cref="TEntity"/></b> record, except selected properties from an <see cref="ISkippable{T}"/> implementation
        /// </summary>
        /// <typeparam name="TSkippable">An <see cref="ISkippable{T}"/> implementation</typeparam>
        /// <typeparam name="TEntity">Entity to update in</typeparam>
        /// <param name="record"><b><see cref="TEntity"/></b> record to be updated</param>
        void UpdateExcept<TEntity, TSkippable>(TEntity record) where TSkippable : ISkippable<TEntity>, new() where TEntity : class;

        /// <summary>
        /// Update one <b><see cref="TEntity"/></b> record, except selected properties from an <see cref="ISkippable{T}"/> implementation and other selected properties from the parameters
        /// </summary>
        /// <typeparam name="TEntity">Entity to update in</typeparam>
        /// <typeparam name="TSkippable">An <see cref="ISkippable{T}"/> implementation</typeparam>
        /// <param name="record"><b><see cref="TEntity"/></b> record to be updated</param>
        /// <param name="otherPropertiesToBeSkipped">Additional <b><see cref="record"/></b>'s properties to be excluded from update</param>
        void UpdateExcept<TEntity, TSkippable>(TEntity record, Expression<Func<TEntity, object>> otherPropertiesToBeSkipped) where TSkippable : ISkippable<TEntity>, new() where TEntity : class;

        #endregion

        #region Filter

        /// <summary>
        /// Filter <b><see cref="TEntity"/></b> objects by a custom expression
        /// </summary>
        /// <typeparam name="TEntity">The entity to be filtered</typeparam>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <returns></returns>
        List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        /// <summary>
        /// Filter and order <b><see cref="TEntity"/></b> objects by custom feltering and ordering expressions
        /// </summary>
        /// <typeparam name="TEntity">The entity to be filtered and ordered</typeparam>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable"/></b> ordering expression</param>
        /// <returns></returns>
        List<TEntity> FilterAndOrder<TEntity>(Expression<Func<TEntity, bool>> filterExpression,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc)
            where TEntity : class;

        #endregion

        #region Exist

        /// <summary>
        /// Check if a <b><see cref="TEntity"/></b> object with the <b><see cref="pkValue"/></b> is exist
        /// </summary>
        /// <typeparam name="TEntity">The entity to be checked</typeparam>
        /// <param name="pkValue">The primary key value</param>
        /// <returns><see cref="bool"/></returns>
        bool IsExist<TEntity>(object pkValue) where TEntity : class;

        /// <summary>
        /// Check if a <b><see cref="TEntity"/></b> object matched with the <see cref="Expression{TDelegate}"/> expression is exist
        /// </summary>
        /// <typeparam name="TEntity">The entity to be checked</typeparam>
        /// <param name="selectExpression">Selection expression</param>
        /// <returns><see cref="bool"/></returns>
        bool IsExist<TEntity>(Expression<Func<TEntity, bool>> selectExpression) where TEntity : class;

        #endregion

        #region Contains

        /// <summary>
        /// Check if <b><see cref="TEntity"/></b> contains an object using a custom <b><see cref="IEqualityComparer{TEntity}"/></b>
        /// </summary>
        /// <typeparam name="TEntity">Entity to be look in</typeparam>
        /// <typeparam name="TEntityComparer">Custom <see cref="IEqualityComparer{TEntity}"/> comparer</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns><see cref="bool"/></returns>
        bool Contains<TEntity, TEntityComparer>(TEntity obj) where TEntity : class where TEntityComparer : IEqualityComparer<TEntity>, new();

        #endregion

        #region Get Partial

        /// <summary>
        /// Get a list of <b><typeparamref name="TProperty"/></b> from a <b><typeparamref name="TEntity"/></b>
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <typeparam name="TProperty">Type of <paramref name="propertyToBeSelected"/></typeparam>
        /// <param name="propertyToBeSelected">The <typeparamref name="TEntity"/> property to be selected</param>
        List<TProperty> GetPartial<TEntity, TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected) where TEntity : class;

        /// <summary>
        /// Get a list of <b><typeparamref name="TProperty"/></b> from a <b><typeparamref name="TEntity"/></b> with filtering
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <typeparam name="TProperty">Type of <paramref name="propertyToBeSelected"/></typeparam>
        /// <param name="propertyToBeSelected">The <typeparamref name="TEntity"/> property to be selected</param>
        /// <param name="filterExpression">One or set of conditions to filter by</param>
        List<TProperty> GetPartial<TEntity, TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;


        /// <summary>
        /// Get a list of <b><see cref="object"/>s</b> with a custom properties from a <b><typeparamref name="TEntity"/></b>
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <returns><see cref="IEnumerable{object}"/></returns>
        List<object> GetPartial<TEntity>(Func<TEntity, object> propertiesToBeSelected) where TEntity : class;

        /// <summary>
        /// Get a list of <b><see cref="object"/>s</b> with a custom properties from a <b><typeparamref name="TEntity"/></b> with filtering
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <param name="filterExpression">One or set of conditions to filter by</param>
        /// <returns><see cref="IEnumerable{object}"/></returns>
        List<object> GetPartial<TEntity>(Func<TEntity, object> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;


        /// <summary>
        /// Get a list of <b><see cref="TEntity"/></b> objects with a custom properties
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        List<TEntity> GetPartial<TEntity>(Func<TEntity, TEntity> propertiesToBeSelected) where TEntity : class;

        /// <summary>
        /// Get a list of <b><see cref="TEntity"/></b> objects with a custom properties and filtering
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <param name="filterExpression">One or set of conditions to filter by</param>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        List<TEntity> GetPartial<TEntity>(Func<TEntity, TEntity> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        #endregion

    }

    /// <summary>
    /// Represent the advanced specific repository
    /// </summary>
    /// <typeparam name="TEntity">Entity to create repository for</typeparam>
    public interface IAdvancedRepository<TEntity> where TEntity : class
    {

        #region Get

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records, and load related selected entities records
        /// </summary>
        /// <param name="relatedEntitiesToBeLoaded">Entities to be load data from (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        List<TEntity> Get(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records that respects a set of conditions provided in the filter expression
        /// </summary>
        /// <typeparam name="TEntity">The entity to select from</typeparam>
        /// <param name="filterExpression">One or set of conditions to be respected</param>
        List<TEntity> Get(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Get All <b><see cref="TEntity"/></b> records that respects a set of conditions provided in the filter expression and load related selected entities records
        /// </summary>
        /// <param name="filterExpression">One or set of conditions to be respected</param>
        /// <param name="relatedEntitiesToBeLoaded">Entities to be loaded (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        List<TEntity> Get(Expression<Func<TEntity, bool>>            filterExpression,
                              params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        /// <summary>
        /// Get one <b><see cref="TEntity"/></b> record and load related selected entities
        /// </summary>
        /// <param name="pkValue">Primary key value</param>
        /// <param name="relatedEntitiesToBeLoaded">Entities to be loaded (Should be the <see cref="TEntity"/>'s navigation properties)</param>
        TEntity GetById(object pkValue,params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        #endregion

        #region Update

        /// <summary>
        /// Update many <b><see cref="TEntity"/></b> records
        /// </summary>
        /// <param name="filterExpression">The filter expression</param>
        /// <param name="updateAction">The update action</param>
        void Update( Expression<Func<TEntity , bool>> filterExpression , Action<TEntity> updateAction );

        /// <summary>
        /// Update one <b><see cref="TEntity"/></b> record, except selected properties
        /// </summary>
        /// <param name="record"><b><see cref="TEntity"/></b> record to be updated</param>
        /// <param name="propertiesToBeSkipped"><b><see cref="record"/></b>'s properties to be excluded from update</param>
        void UpdateExcept( TEntity record , Expression<Func<TEntity , object>> propertiesToBeSkipped );

        /// <summary>
        /// Update one <b><see cref="TEntity"/></b> record, except selected properties from an <see cref="ISkippable{T}"/> implementation
        /// </summary>
        /// <typeparam name="TSkippable">An <see cref="ISkippable{T}"/> implementation</typeparam>
        /// <param name="record"><b><see cref="TEntity"/></b> record to be updated</param>
        void UpdateExcept<TSkippable>( TEntity record ) where TSkippable : ISkippable<TEntity> , new();

        /// <summary>
        /// Update one <b><see cref="TEntity"/></b> record, except selected properties from an <see cref="ISkippable{T}"/> implementation and other selected properties from the parameters
        /// </summary>
        /// <typeparam name="TSkippable">An <see cref="ISkippable{T}"/> implementation</typeparam>
        /// <param name="record"><b><see cref="TEntity"/></b> record to be updated</param>
        /// <param name="otherPropertiesToBeSkipped">Additional <b><see cref="record"/></b>'s properties to be excluded/skipped from update</param>
        void UpdateExcept<TSkippable>( TEntity record , Expression<Func<TEntity , object>> otherPropertiesToBeSkipped ) where TSkippable : ISkippable<TEntity> , new();

        #endregion

        #region Filter

        /// <summary>
        /// Filter <b><see cref="TEntity"/></b> objects by a custom expression
        /// </summary>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        List<TEntity> Filter(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Filter and order <b><see cref="TEntity"/></b> objects by custom feltering and ordering expressions
        /// </summary>
        /// <param name="filterExpression">The <b><see cref="Expression"/></b> filter</param>
        /// <param name="orderingFunc">The <b><see cref="IOrderedQueryable"/></b> ordering expression</param>
        List<TEntity> FilterAndOrder(Expression<Func<TEntity, bool>>                       filterExpression,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc);

        #endregion

        #region Exist

        /// <summary>
        /// Check if a <see cref="TEntity"/> object with the <see cref="pkValue"/> is exist
        /// </summary>
        /// <param name="pkValue">The primary key value</param>
        /// <returns><see cref="bool"/></returns>
        bool IsExist( object pkValue );

        /// <summary>
        /// Check if a <b><see cref="TEntity"/></b> object matched with the <see cref="Expression{TDelegate}"/> expression is exist
        /// </summary>
        /// <param name="selectExpression">Selection expression</param>
        /// <returns><see cref="bool"/></returns>
        bool IsExist( Expression<Func<TEntity , bool>> selectExpression );

        #endregion

        #region Contains

        /// <summary>
        /// Check if <b><see cref="TEntity"/></b> contains an object using a custom <b><see cref="IEqualityComparer{TEntity}"/></b>
        /// </summary>
        /// <typeparam name="TEntityComparer">Custom <see cref="IEqualityComparer{TEntity}"/> comparer</typeparam>
        /// <param name="obj">The object to be looking for</param>
        /// <returns><see cref="bool"/></returns>
        bool Contains<TEntityComparer>( TEntity obj ) where TEntityComparer : IEqualityComparer<TEntity> , new();

        #endregion

        #region Get Partial

        /// <summary>
        /// Get a list of <b><typeparamref name="TProperty"/></b> from a <b><typeparamref name="TEntity"/></b>
        /// </summary>
        /// <typeparam name="TProperty">Type of <paramref name="propertyToBeSelected"/></typeparam>
        /// <param name="propertyToBeSelected">The <typeparamref name="TEntity"/> property to be selected</param>
        List<TProperty> GetPartial<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected);

        /// <summary>
        /// Get a list of <b><typeparamref name="TProperty"/></b> from a <b><typeparamref name="TEntity"/></b> with filtering
        /// </summary>
        /// <typeparam name="TProperty">Type of <paramref name="propertyToBeSelected"/></typeparam>
        /// <param name="propertyToBeSelected">The <typeparamref name="TEntity"/> property to be selected</param>
        /// <param name="filterExpression">One or set of conditions to filter by</param>
        List<TProperty> GetPartial<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected, Expression<Func<TEntity, bool>> filterExpression);


        /// <summary>
        /// Get a list of <b><see cref="object"/>s</b> with a custom properties from a <b><typeparamref name="TEntity"/></b>
        /// </summary>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <returns><see cref="IEnumerable{object}"/></returns>
        List<object> GetPartial( Func<TEntity, object> propertiesToBeSelected);

        /// <summary>
        /// Get a list of <b><see cref="object"/>s</b> with a custom properties from a <b><typeparamref name="TEntity"/></b> with filtering
        /// </summary>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <param name="filterExpression">One or set of conditions to filter by</param>
        /// <returns><see cref="IEnumerable{object}"/></returns>
        List<object> GetPartial( Func<TEntity, object> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression);


        /// <summary>
        /// Get a list of <b><see cref="TEntity"/></b> objects with a custom properties
        /// </summary>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        List<TEntity> GetPartial( Func<TEntity, TEntity> propertiesToBeSelected);

        /// <summary>
        /// Get a list of <b><see cref="TEntity"/></b> objects with a custom properties and filtering
        /// </summary>
        /// <param name="propertiesToBeSelected">The <typeparamref name="TEntity"/> properties to be selected</param>
        /// <param name="filterExpression">One or set of conditions to filter by</param>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        List<TEntity> GetPartial( Func<TEntity, TEntity> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression);

        #endregion

        #region Get Where Not In

        /// <summary>
        ///  Get <b><see cref="TEntity"/></b> records whose not in <b><typeparamref name="TNotIn"/></b>
        /// </summary>
        /// <typeparam name="TNotIn">Entity that <b><see cref="TEntity"/></b> records not in</typeparam>
        /// <returns><see cref="List{T}"/></returns>
        List<TEntity> GetWhereNotIn<TNotIn>(Func<TEntity, object> PropertyToBeChecked, Func<TNotIn, object> propertyToCheckIn) where TNotIn : class;

        #endregion

    }
}