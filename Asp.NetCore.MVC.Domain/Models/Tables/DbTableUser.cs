using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableUser : DbTableBase, IUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public bool IsAdministrator { get; set; }
}