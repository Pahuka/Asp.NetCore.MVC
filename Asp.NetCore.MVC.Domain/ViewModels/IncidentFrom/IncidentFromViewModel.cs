using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.MVC.Domain.ViewModels.IncidentFrom;

public class IncidentFromViewModel : ViewModelBase
{
	public int Id { get; set; }
	[Display(Name = "Источник обращения")] public string From { get; set; }
}