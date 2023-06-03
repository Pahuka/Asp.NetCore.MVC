using System.Security.Claims;
using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Account;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore.MVC.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Responce<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.FirstName == model.Name);
            if (user != null)
                return new Responce<ClaimsIdentity>
                {
                    Description = "Пользователь с таким логином уже есть"
                };

            user = new DbTableUser
            {
                Login = model.Name,
                FirstName = "",
                LastName = "",
                IsAdministrator = false,
                Password = model.Password
            };

            await _userRepository.Create(user);

            var result = Authenticate(user);

            return new Responce<ClaimsIdentity>
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new Responce<ClaimsIdentity>
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<Responce<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Login == model.Name);
            if (user == null)
                return new Responce<ClaimsIdentity>
                {
                    Description = "Пользователь не найден"
                };

            if (user.Password != model.Password)
                return new Responce<ClaimsIdentity>
                {
                    Description = "Неверный пароль или логин"
                };
            var result = Authenticate(user);

            return new Responce<ClaimsIdentity>
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new Responce<ClaimsIdentity>
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<Responce<bool>> ChangePassword(ChangePasswordViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.FirstName == model.UserName);
            if (user == null)
                return new Responce<bool>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Пользователь не найден"
                };

            user.Password = model.NewPassword;
            await _userRepository.Update(user);

            return new Responce<bool>
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Пароль обновлен"
            };
        }
        catch (Exception ex)
        {
            return new Responce<bool>
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private ClaimsIdentity Authenticate(DbTableUser user)
    {
        var role = user.IsAdministrator ? "Administrator" : "User";
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new(ClaimsIdentity.DefaultRoleClaimType, role)
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}