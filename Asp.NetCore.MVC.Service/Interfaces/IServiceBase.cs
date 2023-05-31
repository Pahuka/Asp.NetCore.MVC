using Asp.NetCore.MVC.Domain.Responce;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IServiceBase<T, V>
{
    Task<IResponce<IEnumerable<T>>> GetAll();
    Task<IResponce<bool>> Create(V viewModel);
}