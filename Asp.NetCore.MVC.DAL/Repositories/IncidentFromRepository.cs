using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore.MVC.DAL.Repositories;

public class IncidentFromRepository : IIncidentFromRepository
{
	private readonly AppDbContext _appDbContext;

	public IncidentFromRepository(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(DbTableIncidentFrom entity)
	{
		await _appDbContext.DbTableIncidentFroms.AddAsync(entity);
		return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
	}

	public async Task<IQueryable<DbTableIncidentFrom>> GetAll()
	{
		return _appDbContext.DbTableIncidentFroms.AsQueryable();
	}

	public async Task<bool> DeleteAsync(DbTableIncidentFrom entity)
	{
		_appDbContext.DbTableIncidentFroms.Remove(entity);
		return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
	}

	public async Task<DbTableIncidentFrom> Update(DbTableIncidentFrom entity)
	{
		_appDbContext.DbTableIncidentFroms.Update(entity);
		await _appDbContext.SaveChangesAsync();

		return entity;
	}

	public async Task<DbTableIncidentFrom> Get(int id)
	{
		return await _appDbContext.DbTableIncidentFroms.FirstOrDefaultAsync(x => x.Id == id);
	}
}