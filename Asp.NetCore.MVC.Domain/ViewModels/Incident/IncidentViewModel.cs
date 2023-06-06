using System.ComponentModel.DataAnnotations;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.ViewModels.IncidentFrom;
using Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentViewModel : ViewModelBase
{
	[Display(Name = "Номер обращения")] public int IncidentNumber { get; set; }

	[Display(Name = "Страна")] public string Country { get; set; }

	[Display(Name = "Регион")] public string Region { get; set; }

	[Display(Name = "Город")] public string City { get; set; }

	[Display(Name = "Заголовок")] public ReasonTitleViewModel ReasonTitle { get; set; }

	[Display(Name = "Номер телефона абонента")]
	public string PhoneNumber { get; set; }

	[Display(Name = "Текст обращения")] public string Content { get; set; }

	[Display(Name = "Источник обращения")] public IncidentFromViewModel IncidentFrom { get; set; }
	[Display(Name = "Дата создания")] public DateTime EditingDate { get; set; }
}