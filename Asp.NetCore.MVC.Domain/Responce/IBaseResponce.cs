namespace Asp.NetCore.MVC.Domain.Responce;

public interface IBaseResponce<T>
{
	T Data { get; set; }
}