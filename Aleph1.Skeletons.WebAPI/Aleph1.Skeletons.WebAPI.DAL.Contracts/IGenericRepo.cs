using System;
using System.Linq;

using Aleph1.Skeletons.WebAPI.Models;

namespace Aleph1.Skeletons.WebAPI.DAL.Contracts
{
	/// <summary>Generic Repository pattern</summary>
	public interface IGenericRepo : IDisposable
	{
		/// <summary>Apply all the changes to the underlying database</summary>
		void SaveChanges();

		/// <summary>Get all entities</summary>
		/// <returns>Entities</returns>
		IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity;

		/// <summary>Get a single entity (tracked)</summary>
		/// <param name="keyValues">The entity's primary key(s) value(s)</param>
		/// <returns>Entity</returns>
		TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity;

		/// <summary>Insert a single entity</summary>
		/// <param name="entity">Entity to be inserted</param>
		/// <returns>Inserted entity</returns>
		TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity;

		/// <summary>Delete a single entity</summary>
		/// <param name="keyValues">The entity's primary key(s) value(s)</param>
		/// <returns>Deleted entity</returns>
		TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity;
	}
}
