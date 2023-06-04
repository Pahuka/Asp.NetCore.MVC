namespace Asp.NetCore.MVC.DAL.Interfaces;

public interface IRepository<T>
{
	Task<bool> Create(T entity);
	Task<IQueryable<T>> GetAll();
	Task<bool> DeleteAsync(T entity);

	Task<T> Update(T entity);
}