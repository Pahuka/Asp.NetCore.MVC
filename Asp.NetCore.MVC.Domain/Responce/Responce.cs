using Asp.NetCore.MVC.Domain.Enum;

namespace Asp.NetCore.MVC.Domain.Responce;

public class Responce<T> : IResponce<T>
{
	public string Description { get; set; }
	public StatusCode StatusCode { get; set; }
	public T Data { get; set; }
}