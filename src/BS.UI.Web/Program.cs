

var builder = WebApplication.CreateBuilder(args);

//var applicationSettings = builder.Configuration
//    .GetSection("ApplicationSettings").Get<ApplicationSettings>();
var appName = "billsoft"; //applicationSettings.AppName;

//set up connection strings
var connectionString
    = builder.Configuration.GetConnectionString("BillsoftDB");

builder.Services.AddDbContext<BillsoftDBContext>(
    options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = $"BILLSOFT.{appName}";
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    config.Cookie.HttpOnly = true;
});

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = $"BILLSOFT.SESSION.{appName}";
    options.Cookie.IsEssential = true;
    options.IOTimeout = Timeout.InfiniteTimeSpan;
});
builder.Services.AddMemoryCache();

builder.Services.AddLazyCache();
IAppCache cache = new CachingService()
{
    DefaultCachePolicy = new CacheDefaults()
    {
        DefaultCacheDurationSeconds = 60 * 5    //5 MINUTES
    },
};

RegisterServices(builder);
RegisterRepositories(builder);


builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped(x => new ProfileCache(x.GetRequiredService<IAppCache>(),
//    x.GetRequiredService<ISyProfileService>(),
//    x.GetRequiredService<ISyNoticeService>(),
//    x.GetRequiredService<ISyUserProfileRoleService>(),
//    x.GetRequiredService<ISyLogService>()));

//builder.Services.AddScoped(x => new UserOTPCache(x.GetRequiredService<IAppCache>(), x.GetRequiredService<IConfiguration>()));
//builder.Services.AddScoped(x => new UserProfileRoleMenuCache(x.GetRequiredService<IAppCache>(), x.GetRequiredService<ISyMenuRepository>()));
//builder.Services.AddScoped(x => new ResourceTextCache(x.GetRequiredService<ISyLogRepository>(), x.GetRequiredService<IAppCache>(), x.GetRequiredService<ISyResourceTextRepository>()));

builder.Services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

builder.WebHost.UseWebRoot("wwwroot").UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "files"));
var requestPath = "/files";
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.UseNotyf();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMvc(routes =>
{
    ////////////routes.MapRoute(
    ////////////    name: "SY",
    ////////////    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    ////////////routes.MapRoute(
    ////////////    name: "default",
    ////////////    template: "{controller=Login}/{action=Index}/{id?}");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<BillsoftDBContext, BillsoftDBContext>();

    //
    builder.Services.AddScoped<IUserLoginService, UserLoginService>();
    
    //sys services
    builder.Services.AddScoped<ISysLogService, SysLogService>();
    
    //ar services
    builder.Services.AddScoped<IARCustomerService, ARCustomerService>();
    builder.Services.AddScoped<IARInvoiceService, ARInvoiceService>();
    builder.Services.AddScoped<IARChallanService, ARChallanService>();

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddSingleton<ResourceManager>();
}

static void RegisterRepositories(WebApplicationBuilder builder)
{
    //sys repos
    builder.Services.AddScoped<ISysResourceTextRepository, SysResourceTextRepository>();
    builder.Services.AddScoped<ISysLogRepository, SysLogRepository>();

    //ar repos
    builder.Services.AddScoped<IARCustomerRepository, ARCustomerRepository>();
    builder.Services.AddScoped<IARCustomerAddressRepository, ARCustomerAddressRepository>();
    builder.Services.AddScoped<IARChallanRepository, ARChallanRepository>();
    builder.Services.AddScoped<IARChallanDetailRepository, ARChallanDetailRepository>();
    builder.Services.AddScoped<IARDBRepository, ARDBRepository>();
    builder.Services.AddScoped<IARInvoiceRepository, ARInvoiceRepository>();
    builder.Services.AddScoped<IARInvoiceDetailRepository, ARInvoiceDetailRepository>();
}
