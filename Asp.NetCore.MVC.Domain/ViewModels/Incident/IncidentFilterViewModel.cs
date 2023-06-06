using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentFilterViewModel : ViewModelBase
{
	public IEnumerable<DbTableIncident> Incidents { get; set; }
	public List<SelectListItem> IncidentFrom { get; set; }
	public List<SelectListItem> ReasonTitle { get; set; }
}