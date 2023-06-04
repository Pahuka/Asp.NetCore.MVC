using Asp.NetCore.MVC.Domain.Enum;

namespace Asp.NetCore.MVC.Domain.Models.Interfaces;

public interface IIncident
{
	public int IncidentNumber { get; set; }
	public string Country { get; set; }
	public string Region { get; set; }
	public string City { get; set; }
	public string Title { get; set; }
	public string PhoneNumber { get; set; }
	public string Content { get; set; }
	public IncidentFrom IncidentFrom { get; set; }
}