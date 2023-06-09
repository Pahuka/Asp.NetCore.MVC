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
	public async Task<IActionResult> GetIncidents(IncidentFilterViewModel incidentFilterViewModel)
	{
		var responce = await _incidentService.GetAll();
		if (responce.Data == null)
		{
			TempData["Message"] = responce.Description;
			return RedirectToAction("Error");
		}

		var filterResult = responce.Data;

		if (incidentFilterViewModel.IncidentNumber != null)
			filterResult = filterResult.Where(x => x.IncidentNumber == incidentFilterViewModel.IncidentNumber);
		if (!string.IsNullOrEmpty(incidentFilterViewModel.Country))
			filterResult = filterResult.Where(x => x.Country.Equals(incidentFilterViewModel.Country));
		if (!string.IsNullOrEmpty(incidentFilterViewModel.Region))
			filterResult = filterResult.Where(x => x.Region.Equals(incidentFilterViewModel.Region));
		if (!string.IsNullOrEmpty(incidentFilterViewModel.City))
			filterResult = filterResult.Where(x => x.City.Equals(incidentFilterViewModel.City));
		if (!string.IsNullOrEmpty(incidentFilterViewModel.PhoneNumber))
			filterResult = filterResult.Where(x => x.PhoneNumber.Equals(incidentFilterViewModel.PhoneNumber));
		if (!string.IsNullOrEmpty(incidentFilterViewModel.IncidentFrom) &&
		    !incidentFilterViewModel.IncidentFrom.Equals("Все источники"))
			filterResult = filterResult.Where(x => x.IncidentFrom.Equals(incidentFilterViewModel.IncidentFrom));
		if (!string.IsNullOrEmpty(incidentFilterViewModel.ReasonTitle) &&
		    !incidentFilterViewModel.ReasonTitle.Equals("Все причины"))
			filterResult = filterResult.Where(x => x.ReasonTitle.Equals(incidentFilterViewModel.ReasonTitle));
		if (!incidentFilterViewModel.IsAllDateSearch)
			filterResult = filterResult.Where(x => x.EditingDate.Date.Equals(incidentFilterViewModel.CreateTime));

		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
		{
			incidentFilterViewModel.Incidents = filterResult.ToList();
			incidentFilterViewModel.ReasonTitleList = _reasonService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.Reason }).ToList();
			incidentFilterViewModel.IncidentFromList = _incidentFromService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.From }).ToList();

			return View(incidentFilterViewModel);
		}

		TempData["Message"] = responce.Description;
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

	public async Task<IActionResult> RemoveIncident(int id)
	{
		var responce = await _incidentService.Delete(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("GetIncidents");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		if (id == 0)
			return View();

		var responce = await _incidentService.GetById(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
		{
			responce.Data.IncidentFrom = _incidentFromService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.From }).Skip(1).ToList();
			responce.Data.ReasonTitle = _reasonService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.Reason }).Skip(1).ToList();
			return View(responce.Data);
		}

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpPost]
	public async Task<IActionResult> Edit(IncidentCreateViewModel incidentCreateViewModel)
	{
		await _incidentService.Edit(incidentCreateViewModel.Incident.IncidentNumber, incidentCreateViewModel);
		return RedirectToAction("GetIncidents");
	}

	[HttpGet]
	public IActionResult Create()
	{
		var incidentCreateViewModel = new IncidentCreateViewModel
		{
			ReasonTitle = _reasonService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.Reason }).Skip(1).ToList(),
			IncidentFrom = _incidentFromService.GetAll().Result.Data
				.Select(x => new SelectListItem { Text = x.From }).Skip(1).ToList()
		};
		return View(incidentCreateViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Create(IncidentCreateViewModel incidentViewModel)
	{
		await _incidentService.Create(incidentViewModel);
		return RedirectToAction("GetIncidents");
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}
}