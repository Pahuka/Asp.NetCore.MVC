using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IIncidentService : IServiceBase<DbTableIncident, IncidentViewModel>
{
    Task<IResponce<IncidentViewModel>> GetById(int id);
    Task<IResponce<IncidentViewModel>> Edit(int id, IncidentViewModel viewModel);

    Task<IResponce<bool>> Delete(int id);
}