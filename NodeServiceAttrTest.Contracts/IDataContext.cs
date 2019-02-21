using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NodeServiceAttrTest.Contracts.Repositories;

namespace NodeServiceAttrTest.Contracts
{
    public interface IDataContext : IDisposable
    {
        void Commit();
        IServiceRepository Services { get; }
        INodeRepository Nodes { get; }
        IAttributeRepository Attrs { get; }
        IServiceNodesRepository ServiceNodes { get; }
        ICustomDataSetRepository CustomDataSet { get; }
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}
