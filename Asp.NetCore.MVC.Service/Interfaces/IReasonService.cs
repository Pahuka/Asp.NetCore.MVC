using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IReasonService : IServiceBase<DbTableReasonTitle, ReasonTitleViewModel>
{
	Task<IResponce<ReasonTitleViewModel>> GetById(int id);
	Task<IResponce<ReasonTitleViewModel>> Edit(int id, ReasonTitleViewModel viewModel);
	Task<IResponce<bool>> Delete(int id);
}