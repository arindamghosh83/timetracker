using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Timetracker.Core.Domain;
using Timetracker.Core.Domain.Interface;

namespace Timetracker.Core.Infrastructure.Data
{
	public class ActivityRepository:DataRepository<Activity,int>, IActivityRepository
	{
		public ActivityRepository(DbContext db) : base(db)
		{
		}
	}
}
