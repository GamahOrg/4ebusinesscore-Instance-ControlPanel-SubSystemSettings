
using Module = Autofac.Module;


namespace _4EPlatForm.Infrastructure;

public class InfrastructureModule : Module
{
    private readonly bool _isDevelopment = false;
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public InfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        _isDevelopment = isDevelopment;
        var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureModule));

        if (infrastructureAssembly != null)
        {
            _assemblies.Add(infrastructureAssembly);
        }
        if (callingAssembly != null)
        {
            _assemblies.Add(callingAssembly);
        }
    }

    protected override void Load(ContainerBuilder builder)
    {
        if (_isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(builder);
        }
        else
        {
            RegisterProductionOnlyDependencies(builder);
        }
        RegisterCommonDependencies(builder);
    }

    private void RegisterCommonDependencies(ContainerBuilder builder)
    {

        // Queries registration


        // Repository register
        builder.RegisterGeneric(typeof(GRepository<>))
          .As(typeof(IGRepository<>))
          .InstancePerLifetimeScope();

        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        //builder.Register<ServiceFactory>(context =>
        //{
        //  var c = context.Resolve<IComponentContext>();
        //  return t => c.Resolve();
        //});

        var mediatrOpenTypes = new[]
        {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
            .RegisterAssemblyTypes(_assemblies.ToArray())
            .AsClosedTypesOf(mediatrOpenType)
            .AsImplementedInterfaces();
        }

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
            .InstancePerLifetimeScope();
        builder.RegisterType<SubsytemSettingsRepository>().As<ISubsytemSettingsRepository>()
    .InstancePerLifetimeScope();

    }

    private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
    {
        // TODO: Add development only services
    }

    private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
    {
        // TODO: Add production only services
    }


}
