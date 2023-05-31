using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return View(responce.Data);
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> GetIncident(int id)
	{
		var responce = await _incidentService.GetIncident(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return View(responce.Data);
		return RedirectToAction("Error");
	}

	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> RemoveIncident(int id)
	{
		var responce = await _incidentService.Delete(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			RedirectToAction("GetIncidents");
		return RedirectToAction("Error");
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Save(int id)
	{
		var responce = await _incidentService.GetIncident(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("GetIncidents");
		return RedirectToAction("Error");
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Save(IncidentViewModel incidentViewModel)
	{
		if (ModelState.IsValid)
		{
			await _incidentService.Edit(incidentViewModel.IncidentNumber, incidentViewModel);
		}

		return RedirectToAction("GetIncidents");
	}

	public IActionResult Error()
	{
		return View();
	}
}