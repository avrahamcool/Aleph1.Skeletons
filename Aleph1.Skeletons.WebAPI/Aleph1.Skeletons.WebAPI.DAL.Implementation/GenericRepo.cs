using System.Linq;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	internal class GenericRepo : IGenericRepo
	{
		private readonly GenericContext context;
		public GenericRepo(GenericContext genericContext) => context = genericContext;
		public void Dispose()
		{
			context.Dispose();
		}

		[Logged]
		public void SaveChanges()
		{
			context.SaveChanges();
		}

		[Logged]
		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity
		{
			return context.Set<TEntity>().AsNoTracking();
		}

		[Logged]
		public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			return context.Set<TEntity>().Find(keyValues);
		}

		[Logged]
		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity
		{
			return context.Set<TEntity>().Add(entity).Entity;
		}

		[Logged]
		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			TEntity entity = GetById<TEntity>(keyValues);
			return context.Set<TEntity>().Remove(entity).Entity;
		}
	}
}
