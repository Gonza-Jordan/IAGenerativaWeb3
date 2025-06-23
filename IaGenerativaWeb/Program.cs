
using IAGenerativa.Data.EF;
using IAGenerativa.Data.Repository;
using IAGenerativa.Data.UnitOfWork;
using IAGenerativa.Logica.Servicios.Interfaces;
using IAGenerativaDemo.Business.Servicios;
using IAGenerativaWeb.Models.Configuration.MLModelConfiguration;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IagenerativaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();




builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddOptions().Configure<MLModelConfiguration>(builder.Configuration.GetSection("IAGenerativeModel"))
    .AddScoped<IClasificacionTextoService, ClasificacionTextoService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped<ClasificacionTextoService>();
builder.Services.AddTransient<IStartupService, StartupService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var startupService = scope.ServiceProvider.GetRequiredService<IStartupService>();
    await startupService.InitializeAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
