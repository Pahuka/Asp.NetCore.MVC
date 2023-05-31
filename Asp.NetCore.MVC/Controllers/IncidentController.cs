using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore.MVC.Controllers;

public class IncidentController : Controller
{
	private readonly IIncidentService _incidentService;

	public IncidentController(IIncidentService incidentService)
	{
		_incidentService = incidentService;
	}


	[HttpGet]
	public async Task<IActionResult> GetIncidents()
	{
		var responce = await _incidentService.GetIncidents();
		return View(responce.Data);
	}
}