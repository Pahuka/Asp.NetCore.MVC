namespace Asp.NetCore.MVC.Domain.Models.Interfaces;

public interface IIncidentHistory
{
	public int IncidentNumber { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
}