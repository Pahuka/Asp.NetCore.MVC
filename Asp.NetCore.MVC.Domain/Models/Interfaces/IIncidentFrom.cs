using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.Domain.Models.Interfaces;

public interface IIncidentFrom
{
	public int Id { get; set; }
	public string From { get; set; }
}