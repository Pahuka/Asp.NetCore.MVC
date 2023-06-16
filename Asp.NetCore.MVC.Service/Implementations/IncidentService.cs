using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Service.Interfaces;

namespace Asp.NetCore.MVC.Service.Implementations;

public class IncidentService : IIncidentService
{
	private readonly IIncidentFromRepository _incidentFromRepository;
	private readonly IIncidentRepository _incidentRepository;
	private readonly IReasonTitleRepository _reasonTitleRepository;

	public IncidentService(IIncidentRepository incidentRepository, IIncidentFromRepository incidentFromRepository,
		IReasonTitleRepository reasonTitleRepository)
	{
		_incidentRepository = incidentRepository;
		_incidentFromRepository = incidentFromRepository;
		_reasonTitleRepository = reasonTitleRepository;
	}

	public async Task<IResponce<bool>> Create(IncidentCreateViewModel incidentViewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			incidentViewModel.Incident.IncFrom =
				await _incidentFromRepository.Get(int.Parse(incidentViewModel.FromSelect));
			incidentViewModel.Incident.IncFrom.Incidents.Add(incidentViewModel.Incident);
			incidentViewModel.Incident.IncReason =
				await _reasonTitleRepository.Get(int.Parse(incidentViewModel.ReasonSelect));
			incidentViewModel.Incident.IncReason.Incidents.Add(incidentViewModel.Incident);
			await _incidentFromRepository.Update(incidentViewModel.Incident.IncFrom);
			await _reasonTitleRepository.Update(incidentViewModel.Incident.IncReason);
			responce.Data = await _incidentRepository.Create(incidentViewModel.Incident);

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

	public async Task<IResponce<bool>> Delete(int id)
	{
		var responce = new Responce<bool>();
		try
		{
			var incident = await _incidentRepository.Get(id);
			if (incident == null)
			{
				responce.Description = $"Обращение с номером {id} не найдено";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			responce.StatusCode = StatusCode.OK;
			responce.Data = await _incidentRepository.DeleteAsync(incident);
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

	public async Task<IResponce<IncidentCreateViewModel>> GetById(int id)
	{
		var responce = new Responce<IncidentCreateViewModel>();
		try
		{
			var incident = await _incidentRepository.Get(id);
			if (incident == null)
			{
				responce.Description = $"Обращение с номером {id} не найдено";
				responce.StatusCode = StatusCode.OK;
				return responce;
			}

			var incidentViewModel = new IncidentCreateViewModel
			{
				Incident = incident
			};

			responce.StatusCode = StatusCode.OK;
			responce.Data = incidentViewModel;
			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IncidentCreateViewModel>
			{
				Description = $"[GetById] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<IncidentCreateViewModel>> Edit(int id, IncidentCreateViewModel incidentViewModel)
	{
		var responce = new Responce<IncidentCreateViewModel>();
		try
		{
			var incident = await _incidentRepository.Get(id);

			if (incident == null)
			{
				responce.Description = "Найдено 0 обращений";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			incident.Content = incidentViewModel.Incident.Content;
			incident.City = incidentViewModel.Incident.City;
			incident.Country = incidentViewModel.Incident.Country;
			incident.Region = incidentViewModel.Incident.Region;
			incident.IncFrom = await _incidentFromRepository.Get(int.Parse(incidentViewModel.FromSelect));
			incident.IncReason = await _reasonTitleRepository.Get(int.Parse(incidentViewModel.ReasonSelect));
			incident.PhoneNumber = incidentViewModel.Incident.PhoneNumber;
			incident.EditingDate = DateTime.Now;

			await _incidentRepository.Update(incident);
			responce.Data = incidentViewModel;
			responce.StatusCode = StatusCode.OK;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IncidentCreateViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<IQueryable<DbTableIncident>>> GetAll()
	{
		var responce = new Responce<IQueryable<DbTableIncident>>();
		try
		{
			var incidents = await _incidentRepository.GetAll();

			if (incidents.Count() == 0)
			{
				responce.Description = "Найдено 0 обращений";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			responce.Data = incidents;
			responce.StatusCode = StatusCode.OK;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IQueryable<DbTableIncident>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}
}