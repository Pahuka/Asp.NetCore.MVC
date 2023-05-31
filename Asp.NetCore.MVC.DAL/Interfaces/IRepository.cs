using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.DAL.Interfaces;

public interface IRepository<T>
{
	Task<bool> Create(T entity);
	Task<DbTableIncident?> Get(int Id);
	Task<List<DbTableIncident>> GetAll();
	bool DeleteAsync(T entity);
}