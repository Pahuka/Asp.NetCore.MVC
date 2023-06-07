using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.Domain.ViewModels.Dictionary;

public class DictionaryViewModel : ViewModelBase
{
	public IEnumerable<DbTableReasonTitle> ReasonTitleList { get; set; }
	public IEnumerable<DbTableIncidentFrom> IncidentFromList { get; set; }
}