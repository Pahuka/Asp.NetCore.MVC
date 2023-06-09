using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;

public class ReasonTitleViewModel : ViewModelBase
{
	public int Id { get; set; }

	[Display(Name = "Причина обращения")]
	public string Reason { get; set; }
}