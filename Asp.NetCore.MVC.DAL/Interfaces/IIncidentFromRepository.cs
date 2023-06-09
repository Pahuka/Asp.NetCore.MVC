﻿using Asp.NetCore.MVC.Domain.Models.Tables;

namespace Asp.NetCore.MVC.DAL.Interfaces;

public interface IIncidentFromRepository : IRepository<DbTableIncidentFrom>
{
	Task<DbTableIncidentFrom> Get(int id);
}