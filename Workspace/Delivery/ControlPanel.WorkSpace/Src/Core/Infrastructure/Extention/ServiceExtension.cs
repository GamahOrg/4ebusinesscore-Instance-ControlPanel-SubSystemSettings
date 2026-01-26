

using _4EPlatForm.Infrastrucure.Contracts;
using _4EPlatForm.Infrastrucure.Repostories;

namespace _4EPlatForm.Infrastrucure.Extention
{
    public static class ServiceExtension
    {
        public static void AddUnitOfWorkRepositry(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubsytemSettingsRepository, SubsytemSettingsRepository>();
            services.AddScoped<ILK_NoteTypesRepository, LK_NoteTypesRepository>();
            services.AddScoped<ISubsytemSettingsMainClassificationRepository, SubsytemSettingsMainClassificationRepository>();
            services.AddScoped<ISubsytemMainLookupRepository, SubsytemMainLookupRepository>();
            services.AddScoped<ISubsystemSettingsMainClassificationLinkRepo, SubsystemSettingsMainClassificationLinkRepo>();
            services.AddScoped<ISSOSecuritySettingRepositry, SSOSecuritySettingRepositry>();


        }
    }
}
