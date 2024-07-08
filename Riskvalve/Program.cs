using DataAccessLayer;
using BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using SharedLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(options => 
{
    options.HeaderName = "__RequestVerificationToken";
});

// Add services to the container.
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.Secure = CookieSecurePolicy.Always;
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
// Area Platfrom Asset
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IValveTypeRepository, ValveTypeRepository>();
builder.Services.AddScoped<IManualOverrideRepository, ManualOverrideRepository>();
builder.Services.AddScoped<IFluidPhaseRepository, FluidPhaseRepository>();
builder.Services.AddScoped<IToxicOrFlamableFluidRepository, ToxicOrFlamableFluidRepository>();
// Maintenance
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IIsValveRepairedRepository, IsValveRepairedRepository>();
builder.Services.AddScoped<IInspectionFileRepository, InspectionFileRepository>();
// Inspecion
builder.Services.AddScoped<IInspectionService, InspectionService>();
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();
builder.Services.AddScoped<IInspectionMethodRepository, InspectionMethodRepository>();
builder.Services.AddScoped<ICurrentConditionLimitStateRepository, CurrentConditionLimitStateRepository>();
builder.Services.AddScoped<IInspectionEffectivenessRepository, InspectionEffectivenessRepository>();
// Assessment
builder.Services.AddScoped<IAssessmentService, AssessmentService>();
builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
builder.Services.AddScoped<IAssessmentInspectionRepository, AssessmentInspectionRepository>();
builder.Services.AddScoped<IAssessmentMaintenanceRepository, AssessmentMaintenanceRepository>();
builder.Services.AddScoped<IHSSEDefinisionRepository, HSSEDefinisionRepository>();
builder.Services.AddScoped<IImpactEffectRepository, ImpactEffectRepository>();
builder.Services.AddScoped<IRecomendationActionRepository, RecomendationActionRepository>();
builder.Services.AddScoped<IRepairedRepository, RepairedRepository>();
builder.Services.AddScoped<ITimeToLimitStateRepository, TimeToLimitStateRepository>();
builder.Services.AddScoped<IUsedWithinOEMSpecificationRepository, UsedWithinOEMSpecificationRepository>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromDays(365); // 1 year
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseMiddleware<SuppressHeadersMiddleware>();

app.UsePathBase(SharedEnvironment.app_path);
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseCookiePolicy(); // Use the cookie policy

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSession();

app.Run();
