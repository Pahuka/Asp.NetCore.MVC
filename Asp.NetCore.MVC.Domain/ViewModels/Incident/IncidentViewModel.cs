using System.ComponentModel.DataAnnotations;
using Asp.NetCore.MVC.Domain.Enum;

namespace Asp.NetCore.MVC.Domain.ViewModels.Incident;

public class IncidentViewModel : ViewModelBase
{
    [Display(Name = "Номер обращения")] public int IncidentNumber { get; set; }

    [Display(Name = "Страна")] public string Country { get; set; }

    [Display(Name = "Регион")] public string Region { get; set; }

    [Display(Name = "Город")] public string City { get; set; }

    [Display(Name = "Заголовок")] public string Title { get; set; }

    [Display(Name = "Номер телефона абонента")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Причина обращения")] public string Content { get; set; }

    [Display(Name = "Источник обращения")] public IncidentFrom IncidentFrom { get; set; }
    [Display(Name = "Дата создания")] public DateTime EditingDate { get; set; }
}