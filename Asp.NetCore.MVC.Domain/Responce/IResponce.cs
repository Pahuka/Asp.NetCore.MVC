using Asp.NetCore.MVC.Domain.Enum;

namespace Asp.NetCore.MVC.Domain.Responce;

public interface IResponce<T>
{
	T Data { get; set; }
	StatusCode StatusCode { get; set; }
	public string Description { get; set; }
}