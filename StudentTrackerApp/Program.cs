using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudentTrackerApp.Services;

var builder = WebApplication.CreateBuilder(args);
    
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CHANGES TO ALLOW APP PUBLISHING
var rootPath = builder.Environment.ContentRootPath;
var dbPath = Path.Combine(rootPath, "Data", "Data.db");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5013")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStudentRepository, DbStudentRepository>();
builder.Services.AddScoped<IAttendanceRepository, DbAttendanceRepository>();
builder.Services.AddScoped<IIdentityUserRepository, DbIdentityUserRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>{
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services
    .AddDefaultIdentity<ApplicationUser>(
        options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(); 
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

await CreateRoles(app.Services);

app.Run();

// Seed user roles into database
async Task CreateRoles(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "Teacher", "Student" };
        
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                System.Diagnostics.Debug.WriteLine($"Created Role: {roleName}");
            }
        }

    }
}