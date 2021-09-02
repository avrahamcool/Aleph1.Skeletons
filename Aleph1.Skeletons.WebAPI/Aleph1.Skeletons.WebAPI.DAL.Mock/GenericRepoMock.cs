using System.Linq;

using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal class GenericRepoMock : IGenericRepo
	{
		private readonly GenericContextMock GenericContext;
		public GenericRepoMock(GenericContextMock genericContext) => GenericContext = genericContext;
		public void Dispose() => GenericContext.Dispose();

		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity => GenericContext.Set<TEntity>().AsNoTracking();

		public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity => GenericContext.Set<TEntity>().Find(keyValues);

		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity => GenericContext.Set<TEntity>().Add(entity).Entity;

		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			TEntity entity = GetById<TEntity>(keyValues);
			return GenericContext.Set<TEntity>().Remove(entity).Entity;
		}

		public void SaveChanges() => GenericContext.SaveChanges();
	}
}
