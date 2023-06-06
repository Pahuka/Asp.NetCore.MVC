using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.DAL.Interfaces;

public interface IReasonTitleRepository : IRepository<DbTableReasonTitle>
{
	Task<DbTableReasonTitle> Get(int Id);
}