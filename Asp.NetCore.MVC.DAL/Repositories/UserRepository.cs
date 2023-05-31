using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> Create(DbTableUser entity)
    {
        await _appDbContext.DbTableUsers.AddAsync(entity);
        return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
    }

    public Task<DbTableUser> Get(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<DbTableUser>> GetAll()
    {
        return _appDbContext.DbTableUsers.AsQueryable();
    }

    public async Task<bool> DeleteAsync(DbTableUser entity)
    {
        _appDbContext.DbTableUsers.Remove(entity);
        return _appDbContext.SaveChangesAsync().IsCompletedSuccessfully;
    }

    public async Task<DbTableUser> Update(DbTableUser entity)
    {
        _appDbContext.DbTableUsers.Update(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }
}