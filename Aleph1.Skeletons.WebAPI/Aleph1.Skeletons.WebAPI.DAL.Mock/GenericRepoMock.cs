using System.Linq;

using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Implementation;
using Aleph1.Skeletons.WebAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal class GenericRepoMock : IGenericRepo
	{
		private readonly GenericContext context;
		public GenericRepoMock(GenericContext genericContext) => context = genericContext;
		public void Dispose()
		{
			context.Dispose();
		}

		public void SaveChanges()
		{
			context.SaveChanges();
		}

		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity
		{
			return context.Set<TEntity>().AsNoTracking();
		}

		public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			return context.Set<TEntity>().Find(keyValues);
		}

		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity
		{
			return context.Set<TEntity>().Add(entity).Entity;
		}

		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			TEntity entity = GetById<TEntity>(keyValues);
			return context.Set<TEntity>().Remove(entity).Entity;
		}
	}
}
