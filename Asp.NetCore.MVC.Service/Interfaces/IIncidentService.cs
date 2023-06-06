using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IIncidentService : IServiceBase<DbTableIncident, IncidentCreateViewModel>
{
	Task<IResponce<IncidentCreateViewModel>> GetById(int id);
	Task<IResponce<IncidentCreateViewModel>> Edit(int id, IncidentCreateViewModel viewModel);

	Task<IResponce<bool>> Delete(int id);
}