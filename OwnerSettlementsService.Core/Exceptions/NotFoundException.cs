using OwnerSettlementsService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Core.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string entityName, object keyValue) : base($"The {entityName} with the id '{keyValue}' doesn't exist.")
        {
            this.StatusCode = 404;
            this.Title = entityName + " Not Found.";
        }
    }
}
