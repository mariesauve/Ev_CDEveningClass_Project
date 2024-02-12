using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MerchProjectDataAccess.Data;
using OnlineStoreApp;
using MerchProjectDataAccess.Repository.IRepository;
using MerchProjectDataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



//using (var scope = app.Services.CreateScope())

//{

//    var userManager =

//        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//    string email = "adminHR@admin.com";

//    string password = "Test1234!";

//    if (await userManager.FindByEmailAsync(email) == null)

//    {

//        var user = new IdentityUser();

//        user.UserName = email;

//        user.Email = email;

//        user.EmailConfirmed = true;

//        await userManager.CreateAsync(user, password);

//        await userManager.AddToRoleAsync(user, "Admin");

//    }

//}



app.Run();
