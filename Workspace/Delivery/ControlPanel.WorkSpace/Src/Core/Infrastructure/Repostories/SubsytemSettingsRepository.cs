namespace Infrastructure.Repostories
{
    public class SubsytemSettingsRepository : GRepository<SubsytemSettings>, ISubsytemSettingsRepository
    {
        public SubsytemSettingsRepository(DBContext context) : base(context)
        {

        }
    }
}
