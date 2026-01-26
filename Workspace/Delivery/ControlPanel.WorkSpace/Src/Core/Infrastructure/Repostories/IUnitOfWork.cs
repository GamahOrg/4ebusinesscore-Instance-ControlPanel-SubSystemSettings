using _4EPlatForm.Infrastrucure.Contracts;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Guid _loggedInUserId { get; }
        ISubsytemSettingsRepository WorkSpace { get; }
        ILK_NoteTypesRepository LK_NoteTypes { get; }
        ISubsytemSettingsMainClassificationRepository SubsytemSettingsMainClassification { get; }
        ISubsytemMainLookupRepository SubsytemMainLookup { get; }
        ISubsystemSettingsMainClassificationLinkRepo SettingsMainClassificationLinkRepo { get; }
        ISSOSecuritySettingRepositry SSOSecuritySettingRepositry { get; }
        #region Transaction
        void BeginTransaction();
        void Commit();
        void Rollback();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        #endregion
        #region SaveChanges
        Task SaveChangesAsync();
        void SaveChanges();
        #endregion
    }
}
