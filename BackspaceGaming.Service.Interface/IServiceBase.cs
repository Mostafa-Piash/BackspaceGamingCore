using BackspaceGaming.Repository;
using BackspaceGaming.Repostiroy.Interface;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Services;

namespace BackspaceGaming.Service.Interface
{
    public interface IServiceBase<TEntity> : IService<TEntity>, IRepositoryBase<TEntity> where TEntity : class, ITrackable
    {
        Task<System.Collections.Generic.List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken);
        TEntity Find(object[] keyValues, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}