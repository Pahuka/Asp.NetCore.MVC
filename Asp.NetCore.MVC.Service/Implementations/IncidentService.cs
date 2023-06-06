using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.NetCore.MVC.Service.Implementations;

public class IncidentService : IIncidentService
{
	private readonly IIncidentRepository _incidentRepository;

	public IncidentService(IIncidentRepository incidentRepository)
	{
		_incidentRepository = incidentRepository;
	}

	public async Task<IResponce<bool>> Create(IncidentCreateViewModel incidentViewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			//var incident = new DbTableIncident
			//{
			//	City = incidentViewModel.Incident.City,
			//	Country = incidentViewModel.Incident.Country,
			//	Region = incidentViewModel.Incident.Region,
			//	IncidentFrom = incidentViewModel.Incident.IncidentFrom,
			//	PhoneNumber = incidentViewModel.Incident.PhoneNumber,
			//	Content = incidentViewModel.Incident.Content,
			//	ReasonTitleId = incidentViewModel.Incident.ReasonTitleId
			//};

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
				responce.Description = $"Иницдент с номером {id} не найден";
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
				responce.Description = $"Иницдент с номером {id} не найден";
				responce.StatusCode = StatusCode.OK;
				return responce;
			}

			var incidentViewModel = new IncidentCreateViewModel
			{
				Incident = incident,
				//FromSelect = incident.IncidentFromId.ToString(),
				//IncidentFrom = new List<SelectListItem> { new() { Value = $"{incident.IncidentFromId}" } },
				//IncidentNumber = new SelectList(new int[1] { incident.IncidentNumber }),
				//PhoneNumber = new SelectList(new string[1] { incident.PhoneNumber }),
				//ReasonSelect = incident.ReasonTitleId.ToString()
			};

			responce.StatusCode = StatusCode.OK;
			responce.Data = incidentViewModel;
			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IncidentCreateViewModel>
			{
				Description = $"[GetIncident] : {e.Message}"
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
				responce.Description = "Найдено 0 инцидентов";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			incident.Content = incidentViewModel.Incident.Content;
			incident.City = incidentViewModel.Incident.City;
			incident.Country = incidentViewModel.Incident.Country;
			incident.Region = incidentViewModel.Incident.Region;
			incident.IncidentFromId = int.Parse(incidentViewModel.FromSelect);
			incident.ReasonTitleId = int.Parse(incidentViewModel.ReasonSelect);
			incident.PhoneNumber = incidentViewModel.PhoneNumber.DataTextField;
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
			var incidents = _incidentRepository.GetAll().Result;

			if (incidents.Count() == 0)
			{
				responce.Description = "Найдено 0 инцидентов";
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