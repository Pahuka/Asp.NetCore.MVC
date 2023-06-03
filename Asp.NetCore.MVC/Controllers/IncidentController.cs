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
		var responce = await _incidentService.GetAll();
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return View(responce.Data);
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> GetIncident(int id)
	{
		var responce = await _incidentService.GetById(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return View(responce.Data);
		return RedirectToAction("Error");
	}

	[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> RemoveIncident(int id)
	{
		var responce = await _incidentService.Delete(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			RedirectToAction("GetIncidents");
		return RedirectToAction("Error");
	}

	//[HttpGet]
	////[Authorize(Roles = "Admin")]
	//public async Task<IActionResult> Save(int id)
	//{
	//	if (id == 0)
	//		return View();

	//	var responce = await _incidentService.GetById(id);
	//	if (responce.StatusCode == Domain.Enum.StatusCode.OK)
	//		return View(responce.Data);
	//	return RedirectToAction("Error");
	//}

	//[HttpPost]
	////[Authorize(Roles = "Administrator")]
	//public async Task<IActionResult> Save(IncidentViewModel incidentViewModel)
	//{
	//	if (ModelState.IsValid) await _incidentService.Edit(incidentViewModel.IncidentNumber, incidentViewModel);

	//	return RedirectToAction("GetIncidents");
	//}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(IncidentViewModel incidentViewModel)
	{
		if (ModelState.IsValid) await _incidentService.Create(incidentViewModel);
		return RedirectToAction("GetIncidents");
	}

	public IActionResult Error()
	{
		return View();
	}
}