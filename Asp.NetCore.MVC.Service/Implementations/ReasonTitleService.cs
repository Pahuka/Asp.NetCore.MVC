using Asp.NetCore.MVC.DAL.Interfaces;
using Asp.NetCore.MVC.Domain.Enum;
using Asp.NetCore.MVC.Domain.Models.Tables;
using Asp.NetCore.MVC.Domain.Responce;
using Asp.NetCore.MVC.Domain.ViewModels.ReasonTitle;
using Asp.NetCore.MVC.Service.Interfaces;

namespace Asp.NetCore.MVC.Service.Implementations;

public class ReasonTitleService : IReasonService
{
	private readonly IReasonTitleRepository _reasonTitleRepository;

	public ReasonTitleService(IReasonTitleRepository reasonTitleRepository)
	{
		_reasonTitleRepository = reasonTitleRepository;
	}

	public async Task<IResponce<IQueryable<DbTableReasonTitle>>> GetAll()
	{
		var responce = new Responce<IQueryable<DbTableReasonTitle>>();
		try
		{
			var reasons = _reasonTitleRepository.GetAll().Result;

			if (reasons.Count() == 0)
			{
				responce.Description = "Найдено 0 причин";
				responce.StatusCode = StatusCode.NotFound;
				return responce;
			}

			responce.Data = reasons;
			responce.StatusCode = StatusCode.OK;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IQueryable<DbTableReasonTitle>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(ReasonTitleViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var incident = new DbTableReasonTitle
			{
				Reason = viewModel.Reason
			};

			responce.Data = await _reasonTitleRepository.Create(incident);

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

	public async Task<IResponce<ReasonTitleViewModel>> GetById(int id)
	{
		var responce = new Responce<ReasonTitleViewModel>();
		try
		{
			var reasonTitle = await _reasonTitleRepository.Get(id);
			if (reasonTitle == null)
			{
				responce.Description = $"Причина обращения с номером {id} не найдена";
				responce.StatusCode = StatusCode.OK;
				return responce;
			}

			var incidentViewModel = new ReasonTitleViewModel
			{
				Reason = reasonTitle.Reason
			};

			responce.StatusCode = StatusCode.OK;
			responce.Data = incidentViewModel;
			return responce;
		}
		catch (Exception e)
		{
			return new Responce<ReasonTitleViewModel>
			{
				Description = $"[GetById] : {e.Message}"
			};
		}
	}
}