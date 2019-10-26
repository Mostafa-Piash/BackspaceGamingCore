using BackspaceGaming.Repository;
using BackspaceGaming.Repostiroy.Interface;
using BackspaceGaming.Service.Interface;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Services;
using URF.Core.Services;

namespace BackspaceGaming.Service
{
   
    public class ServiceBase<TEntity> : Service<TEntity>, IServiceBase<TEntity> where TEntity : class, ITrackable
    {
        private readonly IRepositoryBase<TEntity> repository;

        protected ServiceBase(IRepositoryBase<TEntity> repository) : base(repository)
        {
            this.repository = repository;
        }

        public TEntity Find(object[] keyValues, CancellationToken cancellationToken)
        {
            return this.repository.Find(keyValues, cancellationToken);
        }
        public async System.Threading.Tasks.Task<System.Collections.Generic.List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken)
        {
            return await this.repository.CreateQuery<T>(sql, cancellationToken);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await this.repository.SaveChangesAsync();
        }
    }
}
