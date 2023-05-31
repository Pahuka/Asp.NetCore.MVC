namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentViewModel : ViewModelBase
{
    public int IncidentNumber { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public string Requisites { get; set; }
    public string Author { get; set; }
}