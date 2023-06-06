using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentCreateViewModel : ViewModelBase
{
	public string FromSelect { get; set; }
	public string ReasonSelect { get; set; }
	public DbTableIncident Incident { get; set; }
	public SelectList IncidentNumber { get; set; }
	public List<SelectListItem> IncidentFrom { get; set; }
	public SelectList PhoneNumber { get; set; }
	public List<SelectListItem> ReasonTitle { get; set; }
}