﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MBRepoCore.Interfaces;

namespace MBRepoCore.Repo.Abstractions
{
    /// <summary>
    /// Represent the Asynchronous advanced Generic repository
    /// </summary>
    public interface IAdvancedRepositoryAsync
    {
        #region Get

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.Get{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.Get{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        Task<List<TEntity>> GetAsync<TEntity>(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.Get{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.Get{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.Get{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}},System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.Get{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}},System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetById{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetById{TEntity}"/>
        Task<TEntity> GetByIdAsync<TEntity>(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.Update{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.Update{TEntity}"/>
        Task UpdateAsync<TEntity>( Expression<Func<TEntity , bool>> filterExpression , Action<TEntity> updateAction ) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.UpdateExcept{TEntity}(TEntity,System.Linq.Expressions.Expression{System.Func{TEntity,object}})"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        ///  <inheritdoc cref="IAdvancedRepository.UpdateExcept{TEntity}(TEntity,System.Linq.Expressions.Expression{System.Func{TEntity,object}})"/>
        Task UpdateExceptAsync<TEntity>( TEntity record , Expression<Func<TEntity , object>> propertiesToBeSkipped ) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.UpdateExcept{TEntity,TSkippable}(TEntity)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        ///  <inheritdoc cref="IAdvancedRepository.UpdateExcept{TEntity,TSkippable}(TEntity)"/>
        Task UpdateExceptAsync<TEntity,TSkippable>( TEntity record ) where TSkippable : ISkippable<TEntity> , new() where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.UpdateExcept{TEntity,TSkippable}(TEntity,Expression{Func{TEntity , object}} )"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        ///  <inheritdoc cref="IAdvancedRepository.UpdateExcept{TEntity,TSkippable}(TEntity,Expression{Func{TEntity , object}} )"/>
        Task UpdateExceptAsync<TEntity,TSkippable>( TEntity record , Expression<Func<TEntity , object>> otherPropertiesToBeSkipped ) where TSkippable : ISkippable<TEntity> , new() where TEntity : class;


        #endregion

        #region Filter

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.Filter{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.Filter{TEntity}"/>
        Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.FilterAndOrder{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.FilterAndOrder{TEntity}"/>
        Task<List<TEntity>> FilterAndOrderAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderingFunc) where TEntity : class;

        #endregion

        #region Exist

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.IsExist{TEntity}(object)"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.IsExist{TEntity}(object)"/>
        Task<bool> IsExistAsync<TEntity>(object pkValue) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.IsExist{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.IsExist{TEntity}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        Task<bool> IsExistAsync<TEntity>(Expression<Func<TEntity, bool>> selectExpression) where TEntity : class;

        #endregion

        #region Contains

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.Contains{TEntity,TEntityComparer}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.Contains{TEntity,TEntityComparer}"/>
        Task<bool> ContainsAsync<TEntity, TEntityComparer>(TEntity obj) where TEntity : class where TEntityComparer : IEqualityComparer<TEntity>, new();

        #endregion

        #region Get Partial

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity, TProperty}(Expression{Func{TEntity, object}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity, TProperty}(Expression{Func{TEntity, object}})"/>
        Task<List<TProperty>> GetPartialAsync<TEntity, TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity, TProperty}(Expression{Func{TEntity, object}}, Expression{Func{TEntity, bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity, TProperty}(Expression{Func{TEntity, object}}, Expression{Func{TEntity, bool}})"/>
        Task<List<TProperty>> GetPartialAsync<TEntity, TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, object})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, object})"/>
        Task<List<object>> GetPartialAsync<TEntity>(Func<TEntity, object> propertiesToBeSelected) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, object}, Expression{Func{TEntity, bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, object}, Expression{Func{TEntity, bool}})"/>
        Task<List<object>> GetPartialAsync<TEntity>(Func<TEntity, object> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, TEntity})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, TEntity})"/>
        Task<List<TEntity>> GetPartialAsync<TEntity>(Func<TEntity, TEntity> propertiesToBeSelected) where TEntity : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, TEntity}, Expression{Func{TEntity, bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository.GetPartial{TEntity}(Func{TEntity, TEntity}, Expression{Func{TEntity, bool}})"/>
        Task<List<TEntity>> GetPartialAsync<TEntity>(Func<TEntity, TEntity> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class;

        #endregion

        #region Get Where Not In

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TEntity,TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn) where TEntity : class where TNotIn : class;


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TEntity, TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn, Func<TEntity, bool> tEntityFilter) where TEntity : class where TNotIn : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TNotIn, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TNotIn, bool})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TEntity, TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn, Func<TNotIn, bool> tNotInFilter) where TEntity : class where TNotIn : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool}, Func{TNotIn, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool}, Func{TNotIn, bool})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TEntity, TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn, Func<TEntity, bool> tEntityFilter, Func<TNotIn, bool> tNotInFilter) where TEntity : class where TNotIn : class;

        #endregion

    }

    /// <summary>
    /// Represent the Asynchronous advanced speific Generic repository
    /// </summary>
    public interface IAdvancedRepositoryAsync<TEntity> where TEntity : class
    {

        #region Get

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.Get(System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.Get(System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        Task<List<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.Get(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.Get(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.Get(System.Linq.Expressions.Expression{System.Func{TEntity,bool}},System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.Get(System.Linq.Expressions.Expression{System.Func{TEntity,bool}},System.Linq.Expressions.Expression{System.Func{TEntity,object}}[])"/>
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetById"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetById"/>
        Task<TEntity> GetByIdAsync(object pkValue, params Expression<Func<TEntity, object>>[] relatedEntitiesToBeLoaded);

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.Update"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.Update"/>
        Task UpdateAsync(Expression<Func<TEntity, bool>> filterExpression, Action<TEntity> updateAction);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.UpdateExcept(TEntity,System.Linq.Expressions.Expression{System.Func{TEntity,object}})"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        ///  <inheritdoc cref="IAdvancedRepository{TEntity}.UpdateExcept(TEntity,System.Linq.Expressions.Expression{System.Func{TEntity,object}})"/>
        Task UpdateExceptAsync( TEntity record ,Expression<Func<TEntity , object>> propertiesToBeSkipped );

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.UpdateExcept{TSkippable}(TEntity)"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        ///  <inheritdoc cref="IAdvancedRepository{TEntity}.UpdateExcept{TSkippable}(TEntity)"/>
        Task UpdateExceptAsync<TSkippable>( TEntity record ) where TSkippable : ISkippable<TEntity> , new();

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.UpdateExcept{TSkippable}(TEntity,Expression{Func{TEntity , object}} )"/>
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        ///  <inheritdoc cref="IAdvancedRepository{TEntity}.UpdateExcept{TSkippable}(TEntity,Expression{Func{TEntity , object}} )"/>
        Task UpdateExceptAsync<TSkippable>( TEntity record , Expression<Func<TEntity , object>> otherPropertiesToBeSkipped ) where TSkippable : ISkippable<TEntity> , new();

        #endregion

        #region Filter

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.Filter(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.Filter(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.FilterAndOrder"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.FilterAndOrder"/>
        Task<List<TEntity>> FilterAndOrderAsync( Expression<Func<TEntity , bool>> filterExpression , Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>> orderingFunc );

        #endregion

        #region Exist

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.IsExist(object)"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.IsExist(object)"/>
        Task<bool> IsExistAsync(object pkValue);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.IsExist(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.IsExist(System.Linq.Expressions.Expression{System.Func{TEntity,bool}})"/>
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selectExpression);

        #endregion

        #region Contains

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.Contains{TEntityComparer}"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.Contains{TEntityComparer}"/>
        Task<bool> ContainsAsync<TEntityComparer>(TEntity obj) where TEntityComparer : IEqualityComparer<TEntity>, new();

        #endregion

        #region Get Partial

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial{TProperty}(Expression{Func{TEntity, object}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial{TProperty}(Expression{Func{TEntity, object}})"/>
        Task<List<TProperty>> GetPartialAsync<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial{TProperty}(Expression{Func{TEntity, object}}, Expression{Func{TEntity, bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial{TProperty}(Expression{Func{TEntity, object}}, Expression{Func{TEntity, bool}})"/>
        Task<List<TProperty>> GetPartialAsync<TProperty>(Expression<Func<TEntity, object>> propertyToBeSelected, Expression<Func<TEntity, bool>> filterExpression);


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, object})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, object})"/>
        Task<List<object>> GetPartialAsync(Func<TEntity, object> propertiesToBeSelected); 
        
        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, object}, Expression{Func{TEntity, bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, object}, Expression{Func{TEntity, bool}})"/>
        Task<List<object>> GetPartialAsync(Func<TEntity, object> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression);


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, TEntity})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, TEntity})"/>
        Task<List<TEntity>> GetPartialAsync(Func<TEntity, TEntity> propertiesToBeSelected);

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, TEntity}, Expression{Func{TEntity, bool}})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetPartial(Func{TEntity, TEntity}, Expression{Func{TEntity, bool}})"/>
        Task<List<TEntity>> GetPartialAsync(Func<TEntity, TEntity> propertiesToBeSelected, Expression<Func<TEntity, bool>> filterExpression);

        #endregion

        #region Get Where Not In

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn) where TNotIn : class;


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn, Func<TEntity, bool> tEntityFilter) where TNotIn : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TNotIn, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TNotIn, bool})"/>
        Task<List<TEntity>> GetWhereNotInAsync<TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn, Func<TNotIn, bool> tNotInFilter) where TNotIn : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool}, Func{TNotIn, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereNotIn{TNotIn}(Expression{Func{TEntity, object}}, Func{TNotIn, object}, Func{TEntity, bool}, Func{TNotIn, bool})"/>
        Task<List<TEntity>> GetWhereNotInAsync <TNotIn>(Expression<Func<TEntity, object>> PropertyToBeChecked, Func<TNotIn, object> propertyToLookIn, Func<TEntity, bool> tEntityFilter, Func<TNotIn, bool> tNotInFilter) where TNotIn : class;

        #endregion

        #region Get Where In

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object})"/>
        Task<List<TEntity>> GetWhereInAsync<Tin> (Expression<Func<TEntity, object>> propertyToBeChecked, Func<Tin, object> propertyToLookIn) where Tin : class;


        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object}, Func{TEntity, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object}, Func{TEntity, bool})"/>
        Task<List<TEntity>> GetWhereInAsync<Tin> (Expression<Func<TEntity, object>> propertyToBeChecked, Func<Tin, object> propertyToLookIn, Func<TEntity, bool> tEntityFilter) where Tin : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object}, Func{Tin, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object}, Func{Tin, bool})"/>
        Task<List<TEntity>> GetWhereInAsync<Tin> (Expression<Func<TEntity, object>> propertyToBeChecked, Func<Tin, object> propertyToLookIn, Func<Tin, bool> tInFilter) where Tin : class;

        /// <summary>
        /// Asynchronously, <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object}, Func{TEntity, bool}, Func{Tin, bool})"/>
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        /// <inheritdoc cref="IAdvancedRepository{TEntity}.GetWhereIn{Tin}(Expression{Func{TEntity, object}}, Func{Tin, object}, Func{TEntity, bool}, Func{Tin, bool})"/>
        Task<List<TEntity>> GetWhereInAsync<Tin> (Expression<Func<TEntity, object>> propertyToBeChecked, Func<Tin, object> propertyToLookIn, Func<TEntity, bool> tEntityFilter, Func<Tin, bool> tInFilter) where Tin : class;

        #endregion

    }
}