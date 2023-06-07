using System.ComponentModel.DataAnnotations;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentFilterViewModel : ViewModelBase
{
	public IEnumerable<DbTableIncident> Incidents { get; set; }
	public List<SelectListItem> IncidentFromList { get; set; }
	public List<SelectListItem> ReasonTitleList { get; set; }

	[Display(Name = "Номер телефона абонента")]
	public string PhoneNumber { get; set; }

	[Display(Name = "Номер обращения")] public int IncidentNumber { get; set; }

	[Display(Name = "Источник обращения")] public string IncidentFrom { get; set; }

	[Display(Name = "Причина обращения")] public string ReasonTitle { get; set; }

	[Display(Name = "Страна")] public string Country { get; set; }

	[Display(Name = "Регион")] public string Region { get; set; }

	[Display(Name = "Город")] public string City { get; set; }
}