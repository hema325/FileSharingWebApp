using FileSharingApp.Areas.Admin;
using FileSharingApp.Areas.Identity.Data;
using FileSharingApp.Areas.Users;
using FileSharingApp.Data;
using FileSharingApp.DataBaseInitializer;
using FileSharingApp.Hubs;
using FileSharingApp.Services.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddAdminServices();
builder.Services.AddUsersServices();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication()
.AddFacebook(options =>
{
    options.AppId = builder.Configuration.GetSection("Authentication:Facebook").GetValue<string>("AppId");
    options.AppSecret = builder.Configuration.GetSection("Authentication:Facebook").GetValue<string>("AppSecret");
})
.AddGoogle(options =>
{
    options.ClientId= builder.Configuration["Authentication:Google:ClientId"]; 
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
builder.Services.AddSignalR();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseRequestLocalization(options =>
{
    options.AddSupportedUICultures("ar-EG");
    options.AddSupportedCultures("ar-EG");
    options.SetDefaultCulture("en-US");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Users}/{controller=Home}/{action=Index}/{id?}");

app.MapHub<Notifications>("/Notify");

await seedDataBase();
app.Run();

async Task seedDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.Initialize();
    }
}