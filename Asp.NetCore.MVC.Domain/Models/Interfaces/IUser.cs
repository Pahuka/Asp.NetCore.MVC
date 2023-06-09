﻿namespace Asp.NetCore.MVC.Domain.Models.Interfaces;

public interface IUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public bool IsAdministrator { get; set; }
}