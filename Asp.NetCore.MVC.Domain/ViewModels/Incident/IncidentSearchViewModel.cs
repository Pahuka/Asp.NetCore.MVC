using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentSearchViewModel : ViewModelBase
{
	public IncidentSearchViewModel()
	{
		IncidentFrom = new SelectList(System.Enum.GetNames<IncidentFrom>().Append("Все"));
	}
    public List<DbTableIncident> Incidents { get; set; }
    public SelectList IncidentNumber { get; set; }
    public SelectList IncidentFrom { get; set; }
    public SelectList PhoneNumber { get; set; }
}