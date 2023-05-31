using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableIncident : DbTableBase, IIncident
{
	public int IncidentNumber { get; set; }
	public string Content { get; set; }
	public string Title { get; set; }
	public string Requisites { get; set; }
	public string Author { get; set; }
	public Guid UserId { get; set; }
}