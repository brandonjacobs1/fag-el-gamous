using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Data;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.SimpleSystemsManagement;
//using fag_el_gamous.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//SQLLite db connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Postgres db Connection
string postgresConnectionString;
var request = new GetParameterRequest()
{
    Name = "connectionString"
};
using (var client = new AmazonSimpleSystemsManagementClient(Amazon.RegionEndpoint.GetBySystemName("us-east-1")))
{
    var response = client.GetParameterAsync(request).GetAwaiter().GetResult();
    postgresConnectionString = response.Parameter.Value;
}


builder.Services.AddDbContext<postgresContext>(opt =>
        opt.UseNpgsql(postgresConnectionString));

builder.Services.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>());

// Role-Based Authentication
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

//builder.Services.AddIdentityCore<IdentityUser>()
//    .AddRoleManager<IdentityRole>()
//    .AddUserManager<IdentityUser>();

builder.Services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>();
//builder.Services.AddScoped<IUserRoleStore<IdentityUser>, CustomUserRoleStore>();

builder.Services.AddControllersWithViews();

// Require better passwords than the default options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 15;
    options.Password.RequiredUniqueChars = 6;
});

var app = builder.Build();

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

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self'; font-src 'self'; img-src 'self'; frame-src 'self'");

    await next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

