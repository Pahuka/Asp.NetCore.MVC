using Asp.NetCore.MVC.Domain.Models.Interfaces;

namespace Asp.NetCore.MVC.Domain.Models.Tables;

public class DbTableIncidentHistory : DbTableBase, IIncidentHistory
{
    public int IncidentNumber { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
}