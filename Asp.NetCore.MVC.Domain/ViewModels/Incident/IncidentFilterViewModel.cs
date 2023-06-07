using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentFilterViewModel : ViewModelBase
{
	public IEnumerable<DbTableIncident> Incidents { get; set; }
	public List<SelectListItem> IncidentFromList { get; set; }
	public List<SelectListItem> ReasonTitleList { get; set; }
	public string PhoneNumber { get; set; }
	public int IncidentNumber { get; set; }
	public string IncidentFrom { get; set; }
	public string ReasonTitle { get; set; }
	public string Country { get; set; }
	public string Region { get; set; }
	public string City { get; set; }
}