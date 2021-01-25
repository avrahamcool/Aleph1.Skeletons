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
		private readonly AuthenticationInfo CurrentUser;
		public GenericRepo(GenericContext genericContext, AuthenticationInfo currentUser)
		{
			GenericContext = genericContext;
			CurrentUser = currentUser;
		}
		public void Dispose()
		{
			GenericContext.Dispose();
		}

		[Logged]
		public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity
		{
			return GenericContext.Set<TEntity>().AsNoTracking();
		}

		[Logged]
		public TEntity GetByID<TEntity>(params object[] keyValues) where TEntity : class, IEntity
		{
			return GenericContext.Set<TEntity>().Find(keyValues);
		}

		[Logged]
		public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity
		{
			return GenericContext.Set<TEntity>().Add(entity);
		}

		[Logged]
		public TEntity Delete<TEntity>(params object[] keyValues) where TEntity : class, IEntity
		{
			TEntity entity = GetByID<TEntity>(keyValues);
			return GenericContext.Set<TEntity>().Remove(entity);
		}

		[Logged]
		public void SaveChanges()
		{
			GenericContext.SaveChanges(CurrentUser?.Username);
		}
	}
}
