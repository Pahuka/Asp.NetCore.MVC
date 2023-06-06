using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IReasonService : IServiceBase<DbTableReasonTitle, ReasonTitleViewModel>
{
	Task<IResponce<ReasonTitleViewModel>> GetById(int id);
}