using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Controllers;

public class IncidentController : Controller
{
	private readonly IIncidentFromService _incidentFromService;
	private readonly IIncidentService _incidentService;
	private readonly IReasonService _reasonService;

	public IncidentController(IIncidentService incidentService, IReasonService reasonService,
		IIncidentFromService incidentFromService)
	{
		_incidentService = incidentService;
		_reasonService = reasonService;
		_incidentFromService = incidentFromService;
	}

	[HttpGet]
	public async Task<IActionResult> GetIncidents(IncidentFilterViewModel incidentFilterViewModel, string IncidentFrom,
		string ReasonTitle)
	{
		var responce = await _incidentService.GetAll();
		if (responce.Data == null)
			return RedirectToAction("Error");

		var filterResult = responce.Data;
		if (!string.IsNullOrEmpty(IncidentFrom) && !IncidentFrom.Equals("Все источники"))
			filterResult = filterResult.Where(x => x.IncidentFrom.Equals(IncidentFrom));
		if (!string.IsNullOrEmpty(ReasonTitle))
			filterResult = filterResult.Where(x => x.ReasonTitle.Equals(ReasonTitle));

		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
		{
			incidentFilterViewModel.Incidents = filterResult.ToList();
			incidentFilterViewModel.ReasonTitle = _reasonService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.Reason }).ToList();
			incidentFilterViewModel.IncidentFrom = _incidentFromService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.From }).ToList();

			return View(incidentFilterViewModel);
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
		{
			responce.Data.IncidentFrom = _incidentFromService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.From }).ToList();
			responce.Data.ReasonTitle = _reasonService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.Reason }).ToList();
			return View(responce.Data);
		}

		return RedirectToAction("Error");
	}

	[HttpPost]
	//[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> Edit(IncidentCreateViewModel incidentCreateViewModel)
	{
		//if (ModelState.IsValid) 
		await _incidentService.Edit(incidentCreateViewModel.Incident.IncidentNumber, incidentCreateViewModel);

		return RedirectToAction("GetIncidents");
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		var incidentCreateViewModel = new IncidentCreateViewModel
		{
			ReasonTitle = _reasonService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.Reason }).ToList(),
			IncidentFrom = _incidentFromService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.From }).ToList()
		};
		return View(incidentCreateViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Create(IncidentCreateViewModel incidentViewModel)
	{
		//if (ModelState.IsValid)
		{
			await _incidentService.Create(incidentViewModel);
		}

		return RedirectToAction("GetIncidents");
	}

	public IActionResult Error()
	{
		return View();
	}
}