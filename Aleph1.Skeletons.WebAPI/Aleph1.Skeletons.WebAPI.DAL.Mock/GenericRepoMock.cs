using System.Linq;

using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Implementation;
using Aleph1.Skeletons.WebAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal sealed class GenericRepoMock(GenericContext genericContext) : IGenericRepo
	{
		public void Dispose()
		{
			genericContext.Dispose();
		}

		public void SaveChanges()
		{
			_ = genericContext.SaveChanges();
		}

		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity
		{
			return genericContext.Set<TEntity>().AsNoTracking();
		}

		public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			return genericContext.Set<TEntity>().Find(keyValues);
		}

		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity
		{
			return genericContext.Set<TEntity>().Add(entity).Entity;
		}

		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			TEntity entity = GetById<TEntity>(keyValues);
			return genericContext.Set<TEntity>().Remove(entity).Entity;
		}
	}
}
