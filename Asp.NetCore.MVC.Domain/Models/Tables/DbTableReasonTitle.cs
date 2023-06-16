using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableReasonTitle : DbTableBase, IReasonTitle
{
	public DbTableReasonTitle()
	{
		Incidents = new List<DbTableIncident>();
	}

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public List<DbTableIncident> Incidents { get; set; }

	public string Reason { get; set; }
}