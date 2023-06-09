using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.User;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IUserService : IServiceBase<DbTableUser, UserViewModel>
{
	Task<IResponce<UserViewModel>> GetByLogin(string login);
	Task<IResponce<DbTableUser>> Edit(UserViewModel viewModel);

	Task<IResponce<bool>> Delete(string login);
}