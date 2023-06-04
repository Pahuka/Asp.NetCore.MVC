using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.User;
using Asp.NetCore.MVC.Service.Interfaces;

namespace Asp.NetCore.MVC.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResponce<IQueryable<DbTableUser>>> GetAll()
    {
        var responce = new Responce<IQueryable<DbTableUser>>();
        try
        {
            var users = _userRepository.GetAll().Result;

            if (users.Count() == 0)
            {
                responce.Description = "Найдено 0 пользователей";
                responce.StatusCode = StatusCode.OK;
                return responce;
            }

            responce.Data = users;
            responce.StatusCode = StatusCode.OK;

            return responce;
        }
        catch (Exception e)
        {
            return new Responce<IQueryable<DbTableUser>>
            {
                Description = $"[GetAll] : {e.Message}"
            };
        }
    }

    public async Task<IResponce<bool>> Create(UserViewModel userViewModel)
    {
        var responce = new Responce<bool>();
        try
        {
            var incident = new DbTableUser
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Login = userViewModel.Login,
                Password = userViewModel.Password,
                IsAdministrator = userViewModel.IsAdministrator
            };

            responce.Data = await _userRepository.Create(incident);

            return responce;
        }
        catch (Exception e)
        {
            return new Responce<bool>
            {
                Description = $"[Create] : {e.Message}",
                Data = false
            };
        }
    }

    public Task<IResponce<UserViewModel>> GetByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public async Task<IResponce<DbTableUser>> Edit(string login, UserViewModel viewModel)
    {
        var responce = new Responce<DbTableUser>();
        try
        {
            var user = await _userRepository.Get(login);

            if (user == null)
            {
                responce.Description = "Найдено 0 пользователей";
                responce.StatusCode = StatusCode.NotFound;
                return responce;
            }

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Login = viewModel.Login;
            user.Password = viewModel.Password;
            user.IsAdministrator = viewModel.IsAdministrator;

            await _userRepository.Update(user);
            responce.Data = user;
            responce.StatusCode = StatusCode.OK;

            return responce;
        }
        catch (Exception e)
        {
            return new Responce<DbTableUser>
            {
                Description = $"[Edit] : {e.Message}"
            };
        }
    }

    public async Task<IResponce<bool>> Delete(string login)
    {
        var responce = new Responce<bool>();
        try
        {
            var user = await _userRepository.Get(login);
            if (user == null)
            {
                responce.Description = $"Пользователь с логином {login} не найден";
                responce.StatusCode = StatusCode.OK;
                return responce;
            }

            responce.Data = await _userRepository.DeleteAsync(user);
            return responce;
        }
        catch (Exception e)
        {
            return new Responce<bool>
            {
                Description = $"[Delete] : {e.Message}",
                Data = false
            };
        }
    }
}