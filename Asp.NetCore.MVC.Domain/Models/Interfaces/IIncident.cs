using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.Domain.Models.Interfaces;

public interface IIncident
{
	public int IncidentNumber { get; set; }
	public string Country { get; set; }
	public string Region { get; set; }
	public string City { get; set; }
	public string PhoneNumber { get; set; }
	public string Content { get; set; }
	public DbTableIncidentFrom? IncFrom { get; set; }
	public DbTableReasonTitle? IncReason { get; set; }
	public int DbTableIncidentFromId { get; set; }
	public int DbTableReasonTitleId { get; set; }
}