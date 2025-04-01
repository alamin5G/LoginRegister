using LoginRegister.Data;
using LoginRegister.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add services to the container to controll the database
// Start Here Database Config.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

builder.Services.AddControllersWithViews();


// End Here Database Config.

var app = builder.Build();


// Seed roles here
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Ensure database is created if not exists
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();


    // Seed default admin if not present
    await DbInitializer.SeedAsync(services);

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

    string[] roles = new[] { "JobSeeker", "Employer", "Admin" };

    foreach (var role in roles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }

    
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Add authentication to the request pipeline
app.UseAuthentication();

app.UseAuthorization();

// Add the endpoints to the request pipeline
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
