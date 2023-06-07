using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.IncidentFrom;
using Asp.NetCore.MVC.Service.Interfaces;

namespace Asp.NetCore.MVC.Service.Implementations;

public class IncidentFromService : IIncidentFromService
{
	private readonly IIncidentFromRepository _incidentFromRepository;

	public IncidentFromService(IIncidentFromRepository incidentFromRepository)
	{
		_incidentFromRepository = incidentFromRepository;
	}

	public async Task<IResponce<IQueryable<DbTableIncidentFrom>>> GetAll()
	{
		var responce = new Responce<IQueryable<DbTableIncidentFrom>>();
		try
		{
			var reasons = await _incidentFromRepository.GetAll();

			if (reasons.Count() == 0)
			{
				responce.Description = "Найдено 0 источников обращения";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			responce.Data = reasons;
			responce.StatusCode = StatusCode.OK;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IQueryable<DbTableIncidentFrom>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(IncidentFromViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var incident = new DbTableIncidentFrom
			{
				From = viewModel.From
			};

			responce.Data = await _incidentFromRepository.Create(incident);

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

	public async Task<IResponce<IncidentFromViewModel>> GetById(int id)
	{
		var responce = new Responce<IncidentFromViewModel>();
		try
		{
			var incident = await _incidentFromRepository.Get(id);
			if (incident == null)
			{
				responce.Description = $"Источник обращения с ИД:{id} не найден";
				responce.StatusCode = StatusCode.OK;
				return responce;
			}

			var incidentViewModel = new IncidentFromViewModel
			{
				From = incident.From
			};

			responce.StatusCode = StatusCode.OK;
			responce.Data = incidentViewModel;
			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IncidentFromViewModel>
			{
				Description = $"[GetById] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<IncidentFromViewModel>> Edit(int id, IncidentFromViewModel incidentFromViewModel)
	{
		var responce = new Responce<IncidentFromViewModel>();
		try
		{
			var incident = await _incidentFromRepository.Get(id);

			if (incident == null)
			{
				responce.Description = "Найдено 0 источников обращения";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			incident.From = incidentFromViewModel.From;
			incident.EditingDate = DateTime.Now;

			await _incidentFromRepository.Update(incident);
			responce.Data = incidentFromViewModel;
			responce.StatusCode = StatusCode.OK;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IncidentFromViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Delete(int id)
	{
		var responce = new Responce<bool>();
		try
		{
			var incident = await _incidentFromRepository.Get(id);
			if (incident == null)
			{
				responce.Description = $"Источник обращения с ИД:{id} не найден";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			responce.StatusCode = StatusCode.OK;
			responce.Data = await _incidentFromRepository.DeleteAsync(incident);
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
}