﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableIncidentFrom : DbTableBase, IIncidentFrom
{
	public DbTableIncidentFrom()
	{
		Incidents = new List<DbTableIncident>();
	}

	public List<DbTableIncident> Incidents { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string From { get; set; }
}