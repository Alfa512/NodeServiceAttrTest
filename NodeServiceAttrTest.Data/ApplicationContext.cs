using System;
using Microsoft.EntityFrameworkCore;
using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Data.Repositories;
using NodeServiceAttrTest.Models;
using Attribute = NodeServiceAttrTest.Models.Attribute;

namespace NodeServiceAttrTest.Data
{
    public class ApplicationContext : DbContext, IDataContext
    {
        private IConfigurationService _configurationService;
        IServiceNodesRepository IDataContext.ServiceNodes => new ServiceNodesRepository(this);
        IServiceRepository IDataContext.Services => new ServiceRepository(this);
        INodeRepository IDataContext.Nodes => new NodeRepository(this);
        IAttributeRepository IDataContext.Attrs => new AttributeRepository(this);

        ICustomDataSetRepository IDataContext.CustomDataSet => new CustomDataSetRepository(this);

        public DbSet<CustomDataSet> CustomData { get; set; }

        public ApplicationContext(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configurationService.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ServiceNodes>()
                .HasKey(bc => new { bc.NodeId, bc.ServiceId});

            #region ManyToMany

            builder.Entity<ServiceNodes>()
                .HasOne(bc => bc.Node)
                .WithMany(b => b.ServiceNodes)
                .HasForeignKey(bc => bc.NodeId);
            builder.Entity<ServiceNodes>()
                .HasOne(bc => bc.Service)
                .WithMany(c => c.ServiceNodes)
                .HasForeignKey(bc => bc.ServiceId);

            builder.Entity<Attribute>()
                .HasOne(r => r.Service)
                .WithMany(s => s.Attributes)
                .HasForeignKey(t => t.ServiceId);

            #endregion
        }

        void IDataContext.Commit()
        {
            SaveChanges();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}
