using Asp.NetCore.MVC.Domain.ViewModels.User;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore.MVC.Controllers;

public class UserController : Controller
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllUsers()
	{
		var responce = await _userService.GetAll();
		if (responce.StatusCode == Domain.Enum.StatusCode.OK) return View(responce.Data.ToList());


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> EditUser(string login)
	{
		var responce = await _userService.GetByLogin(login);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK) return View(responce.Data);

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpPost]
	public async Task<IActionResult> EditUser(UserViewModel userViewModel)
	{
		var responce = await _userService.Edit(userViewModel);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK) return RedirectToAction("GetAllUsers");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	public async Task<IActionResult> RemoveUser(string login)
	{
		var responce = await _userService.Delete(login);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("GetAllUsers");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public IActionResult CreateUser()
	{
		return View(new UserViewModel());
	}

	[HttpPost]
	public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
	{
		var responce = await _userService.Create(userViewModel);
		if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			return RedirectToAction("GetAllUsers");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}
}