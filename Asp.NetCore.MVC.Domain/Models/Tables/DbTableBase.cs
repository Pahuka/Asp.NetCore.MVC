namespace Asp.NetCore.MVC.Domain.Models.Tables;

public abstract class DbTableBase
{
	public Guid Id { get; private set; }
	public DateTime EditingDate { get; set; }

	protected DbTableBase()
	{
		Id = Guid.NewGuid();
		EditingDate = DateTime.UtcNow;
	}
}