using EventsCEC.Domain.Repositories;
using EventsCEC.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

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

SeedUserRoles(app);

app.UseAuthentication(); // Habilita a autenticação
app.UseAuthorization();  // Habilita a autorização

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();

static void SeedUserRoles(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.CreateScope();
    var seed = serviceScope.ServiceProvider
                           .GetService<ISeedUserRoleInitialRepository>();
    seed?.SeedRoles();
    seed?.SeedUsers();
}