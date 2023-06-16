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
	public DbSet<DbTableReasonTitle> DbTableReasonTitles { get; set; }
	public DbSet<DbTableIncidentFrom> DbTableIncidentFroms { get; set; }
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

		modelBuilder.Entity<DbTableReasonTitle>(builder => builder.HasData(
			new DbTableReasonTitle
			{
				Id = 1,
				Reason = "Все причины",
				EditingDate = DateTime.Now
			},
			new DbTableReasonTitle
			{
				Id = 2,
				Reason = "Тестовая причина",
				EditingDate = DateTime.Now
			}));

		modelBuilder.Entity<DbTableIncidentFrom>(builder =>
			builder.HasData(
				new DbTableIncidentFrom
				{
					Id = 3,
					From = "Колл-центр",
					EditingDate = DateTime.Now
				},
				new DbTableIncidentFrom
				{
					Id = 2,
					From = "Сервисный центр",
					EditingDate = DateTime.Now
				},
				new DbTableIncidentFrom
				{
					Id = 1,
					From = "Все источники",
					EditingDate = DateTime.Now
				}));

		modelBuilder.Entity<DbTableIncidentFrom>()
			.HasMany(x => x.Incidents)
			.WithOne(x => x.IncFrom);

		modelBuilder.Entity<DbTableReasonTitle>()
			.HasMany(x => x.Incidents)
			.WithOne(x => x.IncReason);
	}
}