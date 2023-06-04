using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Controllers;

public class IncidentController : Controller
{
	private readonly IIncidentService _incidentService;

	public IncidentController(IIncidentService incidentService)
	{
		_incidentService = incidentService;
	}

	[HttpGet]
	public async Task<IActionResult> GetIncidents(int IncidentNumber, string IncidentFrom, string PhoneNumber)
	{
		var responce = await _incidentService.GetAll();
		var tempResult = responce.Data;

		if (IncidentNumber != null && IncidentNumber != 0)
			tempResult = responce.Data.Where(x => x.IncidentNumber.Equals(IncidentNumber));
		if (!string.IsNullOrEmpty(PhoneNumber) && !PhoneNumber.Equals("Все"))
			tempResult = responce.Data.Where(x => x.PhoneNumber.Equals(PhoneNumber));
		
		//TODO: Исправить поиск по IncidentFrom
		if (!string.IsNullOrEmpty(IncidentFrom) && !IncidentFrom.Equals("Все"))
			tempResult = responce.Data.Where(x => x.IncidentFrom.Equals(IncidentFrom));

		var incidentSearch = new IncidentSearchViewModel
		{
			Incidents = new List<DbTableIncident>(),
			PhoneNumber = new SelectList(responce.Data.Select(x => x.PhoneNumber).ToList().Append("Все")),
			IncidentNumber = new SelectList(responce.Data.Select(x => x.IncidentNumber).ToList().Append(0))
		};

		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
		{
			incidentSearch.Incidents = tempResult.ToList();

			return View(incidentSearch);
		}

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

	//[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> RemoveIncident(int id)
	{
		var responce = await _incidentService.Delete(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("GetIncidents");
		return RedirectToAction("Error");
	}

	[HttpGet]
	//[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> Edit(int id)
	{
		if (id == 0)
			return View();

		var responce = await _incidentService.GetById(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return View(responce.Data);
		return RedirectToAction("Error");
	}

	[HttpPost]
	//[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> Edit(IncidentViewModel incidentViewModel)
	{
		if (ModelState.IsValid) await _incidentService.Edit(incidentViewModel.IncidentNumber, incidentViewModel);

		return RedirectToAction("GetIncidents");
	}

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