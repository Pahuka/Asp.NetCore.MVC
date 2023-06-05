using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore.MVC.DAL;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<DbTableIncident> DbTableIncidents { get; set; }
	public DbSet<DbTableIncidentHistory> DbTableIncidentHistories { get; set; }
	public DbSet<DbTableUser> DbTableUsers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DbTableUser>(builder => builder.HasData(new DbTableUser
		{
			FirstName = "Admin",
			LastName = "Admin",
			Login = "admin",
			Password = "admin",
			IsAdministrator = true,
			EditingDate = DateTime.Now
		}));
	}
}