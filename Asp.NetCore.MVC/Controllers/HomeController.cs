﻿using System.Diagnostics;
using Asp.NetCore.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore.MVC.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}
}