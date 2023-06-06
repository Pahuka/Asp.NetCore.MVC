using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableIncident : DbTableBase, IIncident
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int IncidentNumber { get; set; }

	public string Country { get; set; }
	public string Region { get; set; }
	public string City { get; set; }
	public string ReasonTitle { get; set; }
	public string PhoneNumber { get; set; }
	public string Content { get; set; }
	public string IncidentFrom { get; set; }
}