using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore.MVC.DAL.Repositories;

public class IncidentRepository : IIncidentRepository
{
	private readonly AppDbContext _appDbContext;

	public IncidentRepository(AppDbContext _appDbContext)
	{
		this._appDbContext = _appDbContext;
	}

	public async Task<bool> Create(DbTableIncident entity)
	{
		await _appDbContext.DbTableIncidents.AddAsync(entity);
		return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
	}

	public async Task<DbTableIncident?> Get(int Id)
	{
		return await _appDbContext.DbTableIncidents.FirstOrDefaultAsync(id => id.IncidentNumber == Id);
	}

	public async Task<List<DbTableIncident>> GetAll()
	{
		return await _appDbContext.DbTableIncidents.ToListAsync();
	}

	public bool DeleteAsync(DbTableIncident entity)
	{
		_appDbContext.DbTableIncidents.Remove(entity);
		return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
	}
}