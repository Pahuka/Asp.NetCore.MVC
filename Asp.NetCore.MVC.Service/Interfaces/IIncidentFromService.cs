using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.IncidentFrom;
using Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IIncidentFromService : IServiceBase<DbTableIncidentFrom, IncidentFromViewModel>
{
	Task<IResponce<IncidentFromViewModel>> GetById(int id);
}