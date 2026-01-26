

using _4EPlatForm.Infrastrucure.Contracts;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dbContext;
        private IDbContextTransaction _transaction;
        IServiceProvider _serviceProvider;
        //public Guid _loggedInUserId;
        public UnitOfWork(DBContext dbContext, IServiceProvider serviceProvider, IHttpContextAccessor _httpContextAccessor)
        {

            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }
        public Guid _loggedInUserId { get; }
        ISubsytemSettingsRepository workSpace = null;
        ILK_NoteTypesRepository lK_NoteTypes = null;
        ISubsytemSettingsMainClassificationRepository subsytemSettingsMainClassification = null;
        ISubsytemMainLookupRepository subsytemMainLookup = null;
        ISubsystemSettingsMainClassificationLinkRepo SettingsMainClassificationLinkRepos = null;
        ISSOSecuritySettingRepositry sSOSecuritySettingRepositry =null;



        public ISSOSecuritySettingRepositry SSOSecuritySettingRepositry
        {
            get
            {
                return sSOSecuritySettingRepositry ?? _serviceProvider.GetService<ISSOSecuritySettingRepositry>();
            }
        }
        public ISubsystemSettingsMainClassificationLinkRepo SettingsMainClassificationLinkRepo
        {
            get
            {
                return SettingsMainClassificationLinkRepos ?? _serviceProvider.GetService<ISubsystemSettingsMainClassificationLinkRepo>();
            }
        }

        public ISubsytemSettingsRepository WorkSpace
        {
            get
            {
                return workSpace ?? _serviceProvider.GetService<ISubsytemSettingsRepository>();
            }
        }
        public ILK_NoteTypesRepository LK_NoteTypes
        {
            get
            {
                return lK_NoteTypes ?? _serviceProvider.GetService<ILK_NoteTypesRepository>();
            }
        }
        public ISubsytemSettingsMainClassificationRepository SubsytemSettingsMainClassification
        {
            get
            {
                return subsytemSettingsMainClassification ?? _serviceProvider.GetService<ISubsytemSettingsMainClassificationRepository>();
            }
        }
        public ISubsytemMainLookupRepository SubsytemMainLookup
        {
            get
            {
                return subsytemMainLookup ?? _serviceProvider.GetService<ISubsytemMainLookupRepository>();
            }
        }
        #region Transaction
        public void BeginTransaction() => _transaction = _dbContext.Database.BeginTransaction();
        public async Task BeginTransactionAsync() => _transaction = await _dbContext.Database.BeginTransactionAsync();
        public void Commit() => _transaction.Commit();
        public async Task CommitAsync() => await _transaction.CommitAsync();
        public void Rollback() => _transaction?.Rollback();
        public async Task RollbackAsync() => await _transaction.RollbackAsync();

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
        public async Task DisposeAsync()
        {
            await _transaction.DisposeAsync();
            await _dbContext.DisposeAsync();
        }
        #endregion


        #region SaveChanges
        public void SaveChanges()
        {
            try
            {
                var entries = _dbContext.ChangeTracker.Entries()
.Where(e => (e.Entity is BaseEntity<Guid>) && (
     e.State == EntityState.Added
     || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedBy = _loggedInUserId;
                        entityEntry.Property("CreatedOn").IsModified = false;
                        entityEntry.Property("CreatedBy").IsModified = false;


                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedOn = DateTime.Now;
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedBy = _loggedInUserId;
                    }

                }
                if (_dbContext.Database.CurrentTransaction == null)
                    BeginTransaction();
                _dbContext.SaveChanges();
                //Commit();
            }
            catch (Exception)
            {
                Rollback();

            }

        }

        public async Task SaveChangesAsync()
        {
            try
            {

                var entries = _dbContext.ChangeTracker.Entries()
.Where(e => (e.Entity is BaseEntity<Guid>) && (
     e.State == EntityState.Added
     || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedBy = _loggedInUserId;
                        entityEntry.Property("CreatedOn").IsModified = false;
                        entityEntry.Property("CreatedBy").IsModified = false;


                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedOn = DateTime.Now;
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedBy = _loggedInUserId;
                    }
                }
                if (_dbContext.Database.CurrentTransaction == null)
                    await BeginTransactionAsync();
                await _dbContext.SaveChangesAsync();
                //Commit();
            }
            catch (Exception)
            {
                await RollbackAsync();

            }

        }
        #endregion


    }
}
