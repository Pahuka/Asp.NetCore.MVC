using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.Incident;
using Asp.NetCore.MVC.Service.Interfaces;

namespace Asp.NetCore.MVC.Service.Implementations;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;

    public IncidentService(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<IResponce<bool>> Create(IncidentViewModel incidentViewModel)
    {
        var responce = new Responce<bool>();
        try
        {
            var incident = new DbTableIncident
            {
                City = incidentViewModel.City,
                Country = incidentViewModel.Country,
                Region = incidentViewModel.Region,
                IncidentFrom = incidentViewModel.IncidentFrom,
                PhoneNumber = incidentViewModel.PhoneNumber,
                Content = incidentViewModel.Content,
                Title = incidentViewModel.Title
                //IncidentNumber = incidentViewModel.IncidentNumber
            };

            responce.Data = await _incidentRepository.Create(incident);

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

    public async Task<IResponce<IncidentViewModel>> GetById(int id)
    {
        var responce = new Responce<IncidentViewModel>();
        try
        {
            var incident = await _incidentRepository.Get(id);
            if (incident == null)
            {
                responce.Description = $"Иницдент с номером {id} не найден";
                responce.StatusCode = StatusCode.OK;
                return responce;
            }

            var incidentViewModel = new IncidentViewModel
            {
                Country = incident.Country,
                Region = incident.Region,
                City = incident.City,
                IncidentNumber = incident.IncidentNumber,
                PhoneNumber = incident.PhoneNumber,
                IncidentFrom = incident.IncidentFrom,
                Content = incident.Content,
                Title = incident.Title
            };

            responce.StatusCode = StatusCode.OK;
            responce.Data = incidentViewModel;
            return responce;
        }
        catch (Exception e)
        {
            return new Responce<IncidentViewModel>
            {
                Description = $"[GetIncident] : {e.Message}"
            };
        }
    }

    public async Task<IResponce<DbTableIncident>> Edit(int id, IncidentViewModel incidentViewModel)
    {
        var responce = new Responce<DbTableIncident>();
        try
        {
            var incident = await _incidentRepository.Get(id);

            if (incident == null)
            {
                responce.Description = "Найдено 0 инцидентов";
                responce.StatusCode = StatusCode.NotFound;
                return responce;
            }

            incident.IncidentFrom = incidentViewModel.IncidentFrom;
            incident.Country = incidentViewModel.Country;
            incident.City = incidentViewModel.City;
            incident.Region = incidentViewModel.Region;
            incident.Content = incidentViewModel.Content;
            incident.Title = incidentViewModel.Title;
            incident.PhoneNumber = incidentViewModel.PhoneNumber;
            incident.EditingDate = DateTime.UtcNow;

            await _incidentRepository.Update(incident);
            responce.Data = incident;
            responce.StatusCode = StatusCode.OK;

            return responce;
        }
        catch (Exception e)
        {
            return new Responce<DbTableIncident>
            {
                Description = $"[Edit] : {e.Message}"
            };
        }
    }

    public async Task<IResponce<IEnumerable<DbTableIncident>>> GetAll()
    {
        var responce = new Responce<IEnumerable<DbTableIncident>>();
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
            return new Responce<IEnumerable<DbTableIncident>>
            {
                Description = $"[GetAll] : {e.Message}"
            };
        }
    }
}