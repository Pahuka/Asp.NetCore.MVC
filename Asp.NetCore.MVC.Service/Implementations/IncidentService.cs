using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.DAL.Repositories;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Service.Interfaces;

namespace Asp.NetCore.MVC.Service.Implementations;

public class IncidentService : IIncidentService
{
	private readonly IIncidentRepository _incidentRepository;

	public IncidentService(IIncidentRepository incidentRepository)
	{
		_incidentRepository = incidentRepository;
	}

	public async Task<IBaseResponce<IEnumerable<DbTableIncident>>> GetIncidents()
	{
		var responce = new BaseResponce<IEnumerable<DbTableIncident>>();
		try
		{
			var incidents = _incidentRepository.GetAll().Result;

			if (incidents.Count == 0)
			{
				responce.Description = "Найдено 0 инцидентов";
				responce.StatusCode = StatusCode.OK;
				return responce;
			}

			responce.Data = incidents;
			responce.StatusCode = StatusCode.OK;

			return responce;
		}
		catch (Exception e)
		{
			return new BaseResponce<IEnumerable<DbTableIncident>>
			{
				Description = $"[GetIncidents] : {e.Message}"
			};
		}
	}
}