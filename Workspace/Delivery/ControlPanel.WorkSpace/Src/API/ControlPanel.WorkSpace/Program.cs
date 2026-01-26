

using _4EPlatform.PermissionsGroupingChecker;

var builder = WebApplication.CreateBuilder(args);
try
{
    var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, 7110, o => o.Protocols = HttpProtocols.Http2);

        // ADDED THIS LINE to fix the problem
        options.Listen(IPAddress.Any, 5186, o => o.Protocols = HttpProtocols.Http1);
    });

    builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);
    //builder.Services.AddScoped<PermissionsCheckerMiddleware, PermissionsCheckerMiddleware>();
    builder.Services.AddSingleton<ITokenService, TokenService>();
    builder.Services.AddHostedService<AddPermissionsMiddleware>();
    builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();


    // Add services to the container.
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));




    // solve the problem of cycle

    builder.Services.AddControllersWithViews()
     .AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
 );

    builder.Services.AddDbContext<DBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

        options.EnableSensitiveDataLogging(true);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

    builder.Services.AddUnitOfWorkRepositry();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    //builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("4EPlatform.Application"));
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(CoreModule).Assembly);
        //cfg.RegisterServicesFromAssemblies(typeof(InfrastructureModule).Assembly);
    });

    //builder.Services.AddAutoMapper(typeof(Program), typeof(CoreModule), typeof(InfrastructureModule));
    builder.Services.AddAutoMapper(typeof(Program), typeof(CoreModule));

    builder.Services.AddControllers().AddOData(options =>
                options.Select().Filter().OrderBy().Expand());
    builder.Services.AddHttpContextAccessor();    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddTransient<IAESConfiguration, AESConfiguration>();
    builder.Services.AddSingleton<IAESConfiguration>(builder.Configuration.GetSection("EncryptionConfiguration").Get<AESConfiguration>());

    builder.Services.AddSingleton<IEmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
    builder.Services.AddScoped<IEmailService, EmailService>();

    #region Localization
    builder.Services.AddSingleton<LocService>();
    #endregion

    //Identity Server
    builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    // use identity user in database 
    .AddDeveloperSigningCredential();



    builder.Services.AddApiVersioning(config =>
    {
        // Specify the default API Version as 1.0
        config.DefaultApiVersion = new ApiVersion(1, 0);
        // If the client hasn't specified the API version in the request, use the default API version number 
        config.AssumeDefaultVersionWhenUnspecified = true;
        // Advertise the API versions supported for the particular endpoint
        config.ReportApiVersions = true;
    });
    #region Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "WorkSpace APIs Reference",
        });

        c.AddSecurityDefinition(
        "token",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
            );
        c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
        }
            );
    });
    #endregion
    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(10);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
    });


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowCors",
            builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000")
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
            });
    });




    //builder.Services.AddScoped<AuthorizedPermissions>();
    //builder.Services.AddAuthorization();

    //builder.Services.AddDbContext<UserManagementContext>(options =>
    //{
    //    if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
    //    {
    //        options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings"));
    //    }
    //    else
    //    {
    //        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //    }
    //    options.EnableSensitiveDataLogging(true);
    //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //});

    builder.Services.AddApiVersioning(config =>
    {
        // Specify the default API Version as 1.0
        config.DefaultApiVersion = new ApiVersion(1, 0);
        // If the client hasn't specified the API version in the request, use the default API version number
        config.AssumeDefaultVersionWhenUnspecified = true;
        // Advertise the API versions supported for the particular endpoint
        config.ReportApiVersions = true;
    });
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    builder.Services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        // .AddDataAnnotationsLocalization();
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                return factory.Create("SharedResource", assemblyName.Name);
            };
        });

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[]
        {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("es")
                };

        options.DefaultRequestCulture = new RequestCulture(culture: "ar", uiCulture: "ar");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    //     builder.Services.AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //    }).AddJwtBearer(options =>
    //    {
    //        options.Authority = identityUrl;
    //        options.RequireHttpsMetadata = false;
    //        options.Audience = "basicdata";
    //    });
    //     builder.Services.AddAuthorization(options =>
    //     {
    //         options.AddPolicy("ApiScope", policy =>
    //         {
    //             policy.RequireAuthenticatedUser();
    //             policy.RequireClaim("scope", "basicdata");
    //         });
    //     });
    // Log.Logger = new LoggerConfiguration()
    //     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
    //     {
    //         AutoRegisterTemplate = true,
    //         AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
    //     }).CreateLogger();
    //builder.Host.UseSerilog();

    builder.Services.AddAuthentication("Bearer")
      .AddJwtBearer("Bearer", options =>
      {
          // the name of your api resources   
          options.Audience = "UserManagementServer";
          /// identity server url                    
          options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
          options.RequireHttpsMetadata = false;
      });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ResourceOwnerPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
        });

        options.AddPolicy("ClientCredentialsPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    using (var serviceScope = app.Services.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
        var serviceProvider = serviceScope.ServiceProvider;
        if (!serviceScope.ServiceProvider.GetService<DBContext>().AllMigrationsApplied())
        {
            serviceScope.ServiceProvider.GetService<DBContext>().Migrate();
        }
        ContextSeedData.Seed(dbContext, serviceProvider);
    }

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        RequestPath = "/wwwroot"
    });
    app.MapGrpcService<WorkSpaceServices>();
    app.MapGrpcService<WorkspaceControllerAndActionNameService>();
    app.MapGrpcService<SubsytemSettingsMainClassificationService>();
    app.MapGrpcService<SubsytemMainLookupService>();
    app.MapGrpcService<SSOSystemSettingService>();

    app.MapGrpcReflectionService();
    app.UseCors("AllowCors");
    //app.UseCors();
    app.UseApiVersioning();
    app.MapControllers();
    //app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseIdentityServer();

    //app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    //app.UseMiddleware<PermissionsCheckerMiddleware>();
    app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

    app.Run();

}
catch (Exception ex) when (

// https://github.com/dotnet/runtime/issues/60600
ex.GetType().Name is not "StopTheHostException"
    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
    // need to do it this way until we target .NET 8
    && ex.GetType().Name is not "HostAbortedException")
{
    Console.WriteLine(ex.Message);
    Log.Fatal(ex, "Unhandled exception");
}
