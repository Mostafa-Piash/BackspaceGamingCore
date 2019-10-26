using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;

namespace BackspaceGaming.Repostiroy.Interface
{
    public interface IRepositoryBase<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
        // Example: adding synchronous Find, scope: application wide for all repositories
        TEntity Find(object[] keyValues, CancellationToken cancellationToken);
        Task<System.Collections.Generic.List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}