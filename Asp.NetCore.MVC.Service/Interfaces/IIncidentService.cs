using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IIncidentService
{
	Task<IResponce<IEnumerable<DbTableIncident>>> GetIncidents();
	Task<IResponce<bool>> Create(IncidentViewModel incidentViewModel);
	Task<IResponce<bool>> Delete(int id);
	Task<IResponce<DbTableIncident>> GetIncident(int id);
	Task<IResponce<DbTableIncident>> Edit(int id, IncidentViewModel incidentViewModel);
}