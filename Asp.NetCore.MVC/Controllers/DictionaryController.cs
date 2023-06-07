using Asp.NetCore.MVC.Domain.ViewModels.Dictionary;
using Asp.NetCore.MVC.Domain.ViewModels.IncidentFrom;
using Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore.MVC.Controllers;

public class DictionaryController : Controller
{
	private readonly IIncidentFromService _incidentFromService;
	private readonly IReasonService _reasonService;

	public DictionaryController(IReasonService reasonService, IIncidentFromService incidentFromService)
	{
		_reasonService = reasonService;
		_incidentFromService = incidentFromService;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var dictionaryViewModel = new DictionaryViewModel();
		dictionaryViewModel.IncidentFromList = _incidentFromService.GetAll().Result.Data.Skip(1).ToList();
		dictionaryViewModel.ReasonTitleList = _reasonService.GetAll().Result.Data.Skip(1).ToList();

		return View(dictionaryViewModel);
	}

	[HttpPost]
	public IActionResult Index(DictionaryViewModel dictionaryViewModel)
	{
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> EditReasonTitle(int id)
	{
		var responce = await _reasonService.GetById(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK) return View(responce.Data);

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpPost]
	public async Task<IActionResult> EditFromTitle(IncidentFromViewModel incidentFromViewModel)
	{
		await _incidentFromService.Edit(incidentFromViewModel.Id, incidentFromViewModel);
		return RedirectToAction("Index");
	}

	[HttpGet]
	public async Task<IActionResult> EditFromTitle(int id)
	{
		var responce = await _incidentFromService.GetById(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK) return View(responce.Data);

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpPost]
	public async Task<IActionResult> EditReasonTitle(ReasonTitleViewModel reasonTitleViewModel)
	{
		await _reasonService.Edit(reasonTitleViewModel.Id, reasonTitleViewModel);
		return RedirectToAction("Index");
	}

	public async Task<IActionResult> RemoveReasonTitle(int id)
	{
		var responce = await _reasonService.Delete(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("Index");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	public async Task<IActionResult> RemoveFromTitle(int id)
	{
		var responce = await _incidentFromService.Delete(id);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("Index");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public IActionResult CreateReasonTitle()
	{
		var reasonViewModel = new ReasonTitleViewModel();
		return View(reasonViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> CreateReasonTitle(ReasonTitleViewModel reasonTitleViewModel)
	{
		await _reasonService.Create(reasonTitleViewModel);
		return RedirectToAction("Index");
	}

	[HttpGet]
	public IActionResult CreateFromTitle()
	{
		var reasonViewModel = new IncidentFromViewModel();
		return View(reasonViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> CreateFromTitle(IncidentFromViewModel incidentFromViewModel)
	{
		await _incidentFromService.Create(incidentFromViewModel);
		return RedirectToAction("Index");
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}
}