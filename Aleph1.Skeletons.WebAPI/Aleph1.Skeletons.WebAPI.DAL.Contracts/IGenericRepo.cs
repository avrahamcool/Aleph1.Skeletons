using System;
using System.Linq;

using Aleph1.Skeletons.WebAPI.Models;

namespace Aleph1.Skeletons.WebAPI.DAL.Contracts
{
	/// <summary>Generic Repository pattern</summary>
	public interface IGenericRepo : IDisposable
	{
		/// <summary>apply all the changes to the underlying DB</summary>
		void SaveChanges();

		/// <summary>returns all entities</summary>
		/// <returns>entities</returns>
		IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;

		/// <summary>returns a single entity (tracked)</summary>
		/// <param name="keyValues">The values of the primary key for the entity to be found</param>
		/// <returns>entity</returns>
		TEntity GetByID<TEntity>(params object[] keyValues) where TEntity : class, IEntity;

		/// <summary>insert a single entity</summary>
		/// <param name="entity">the entity to insert</param>
		/// <returns>entity</returns>
		TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity;

		/// <summary>delete a single entity</summary>
		/// <param name="keyValues">The values of the primary key for the entity to be deleted</param>
		/// <returns>entity</returns>
		TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IEntity;
	}
}
