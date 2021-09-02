using System.Linq;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.Models.Security;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	internal class GenericRepo : IGenericRepo
	{
		private readonly GenericContext GenericContext;
		private readonly Identity Identity;

		public GenericRepo(GenericContext genericContext, Identity identity)
		{
			GenericContext = genericContext;
			Identity = identity;
		}

		public void Dispose() => GenericContext.Dispose();

		[Logged]
		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IReadableEntity => GenericContext.Set<TEntity>().AsNoTracking();

		[Logged]
		public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity => GenericContext.Set<TEntity>().Find(keyValues);

		[Logged]
		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IWritableEntity => GenericContext.Set<TEntity>().Add(entity);

		[Logged]
		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IWritableEntity
		{
			TEntity entity = GetById<TEntity>(keyValues);
			return GenericContext.Set<TEntity>().Remove(entity);
		}

		[Logged]
		public void SaveChanges() => GenericContext.SaveChanges(Identity?.Username);
	}
}
