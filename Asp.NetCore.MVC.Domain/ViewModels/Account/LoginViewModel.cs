using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.MVC.Domain.ViewModels.Account;

public class LoginViewModel
{
	[Required(ErrorMessage = "Введите логин")]
	[MaxLength(20, ErrorMessage = "Логин должен иметь длину меньше 20 символов")]
	[MinLength(3, ErrorMessage = "Логин должен иметь длину больше 3 символов")]
	public string Login { get; set; }

	[Required(ErrorMessage = "Введите пароль")]
	[DataType(DataType.Password)]
	[Display(Name = "Пароль")]
	public string Password { get; set; }
}