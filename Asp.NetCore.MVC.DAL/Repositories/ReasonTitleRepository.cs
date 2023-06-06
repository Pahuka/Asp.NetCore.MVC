using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore.MVC.DAL.Repositories;

public class ReasonTitleRepository : IReasonTitleRepository
{
	private readonly AppDbContext _appDbContext;

	public ReasonTitleRepository(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(DbTableReasonTitle entity)
	{
		await _appDbContext.DbTableReasonTitles.AddAsync(entity);
		return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
	}

	public async Task<IQueryable<DbTableReasonTitle>> GetAll()
	{
		return _appDbContext.DbTableReasonTitles.AsQueryable();
	}

	public async Task<bool> DeleteAsync(DbTableReasonTitle entity)
	{
		_appDbContext.DbTableReasonTitles.Remove(entity);
		return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
	}

	public async Task<DbTableReasonTitle> Update(DbTableReasonTitle entity)
	{
		_appDbContext.DbTableReasonTitles.Update(entity);
		await _appDbContext.SaveChangesAsync();

		return entity;
	}

	public async Task<DbTableReasonTitle> Get(int Id)
	{
		return await _appDbContext.DbTableReasonTitles.FirstOrDefaultAsync(x => x.Id == Id);
	}
}