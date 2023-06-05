namespace Asp.NetCore.MVC.Domain.Models.Tables;

public abstract class DbTableBase
{
	protected DbTableBase()
	{
		Id = Guid.NewGuid();
		EditingDate = DateTime.Now;
	}

	public Guid Id { get; private set; }
	public DateTime EditingDate { get; set; }
}