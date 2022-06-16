﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Data.DateTimes
{
    public class SystemsDateTime : IDateTimeBroker
    {
        public Task<DateTime> GetCurrentDateTime()
        {
            return Task.FromResult(DateTime.Now);
        }
    }
}
