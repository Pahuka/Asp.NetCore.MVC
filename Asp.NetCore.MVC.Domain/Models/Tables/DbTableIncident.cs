using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableIncident : DbTableBase, IIncident
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int IncidentNumber { get; set; }

	[Display(Name = "Страна")] public string Country { get; set; }

	[Display(Name = "Регион")] public string Region { get; set; }

	[Display(Name = "Город")] public string City { get; set; }

	[Display(Name = "Номер телефона абонента")]
	public string PhoneNumber { get; set; }

	[Display(Name = "Текст обращения")] public string Content { get; set; }

	[Display(Name = "Источник обращения")] public DbTableIncidentFrom? IncFrom { get; set; }

	[Display(Name = "Причина обращения")] public DbTableReasonTitle? IncReason { get; set; }

	public int DbTableIncidentFromId { get; set; }
	public int DbTableReasonTitleId { get; set; }
}