using System.Security.Claims;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Account;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IAccountService
{
    Task<Responce<ClaimsIdentity>> Register(RegisterViewModel model);

    Task<Responce<ClaimsIdentity>> Login(LoginViewModel model);

    Task<Responce<bool>> ChangePassword(ChangePasswordViewModel model);
}