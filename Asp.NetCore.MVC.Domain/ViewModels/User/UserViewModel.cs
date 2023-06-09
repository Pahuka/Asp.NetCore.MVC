using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.MVC.Domain.ViewModels.User;

public class UserViewModel : ViewModelBase
{
	[Display(Name = "Имя")]
	public string FirstName { get; set; }

	[Display(Name = "Фамилия")]
	public string LastName { get; set; }

	[Display(Name = "Логин")]
	public string Login { get; set; }

	[Display(Name = "Пароль")]
	public string Password { get; set; }

	[Display(Name = "Права администратора")]
	public bool IsAdministrator { get; set; }
}