using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;

namespace Asp.NetCore.MVC.Service.Interfaces;

public interface IIncidentService
{
	Task<IBaseResponce<IEnumerable<DbTableIncident>>> GetIncidents();
}