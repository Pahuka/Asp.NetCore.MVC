using Asp.NetCore.MVC.DAL;
using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.DAL.Repositories;
using Asp.NetCore.MVC.Service.Implementations;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connetion = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connetion));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new PathString("/Account/Login");
		options.AccessDeniedPath = new PathString("/Account/Login");
	});
builder.Services.AddTransient<IIncidentRepository, IncidentRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IReasonTitleRepository, ReasonTitleRepository>();
builder.Services.AddTransient<IIncidentFromRepository, IncidentFromRepository>();
builder.Services.AddTransient<IIncidentService, IncidentService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IReasonService, ReasonTitleService>();
builder.Services.AddTransient<IIncidentFromService, IncidentFromService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");

app.Run();