using AEMS_Zundel.Data;
using AEMS_Zundel.Infrastructure;
using AEMS_Zundel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("EmployeeManagmentContextConnection");
builder.Services.AddDbContext<EmployeeManagmentContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EmployeeManagmentContext>();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("MembersAndAdmins", policy =>
    {
        policy.AddRequirements(new EmployeeAuthorizationRequirement
        {
            AllowAdmins = true,
            AllowMembers = true
        });
    });
});


builder.Services.AddScoped<IUserClaimsPrincipalFactory<EmployeeManagmentUser>, EmployeeManagmentPrincipalFactory>();
builder.Services.AddTransient<IAuthorizationHandler, EmployeeAuthorizationHandler>();





builder.Services.AddControllersWithViews();

var app = builder.Build();
// Run Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.CreateAdminAccount(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
