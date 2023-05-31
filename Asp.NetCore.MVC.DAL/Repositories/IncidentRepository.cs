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

    public async Task<IQueryable<DbTableIncident>> GetAll()
    {
        return _appDbContext.DbTableIncidents.AsQueryable();
    }

    public async Task<bool> DeleteAsync(DbTableIncident entity)
    {
        _appDbContext.DbTableIncidents.Remove(entity);
        return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
    }

    public async Task<DbTableIncident> Update(DbTableIncident entity)
    {
        _appDbContext.DbTableIncidents.Update(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }
}