using System.Linq;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	internal sealed class GenericRepo(GenericContext genericContext) : IGenericRepo
	{
		public void Dispose()
		{
			genericContext.Dispose();
		}

		[Logged]
		public void SaveChanges()
		{
			_ = genericContext.SaveChanges();
		}

		[Logged]
		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity
		{
			return genericContext.Set<TEntity>().AsNoTracking();
		}

		[Logged]
		public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			return genericContext.Set<TEntity>().Find(keyValues);
		}

		[Logged]
		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity
		{
			return genericContext.Set<TEntity>().Add(entity).Entity;
		}

		[Logged]
		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			TEntity entity = GetById<TEntity>(keyValues);
			return genericContext.Set<TEntity>().Remove(entity).Entity;
		}
	}
}
