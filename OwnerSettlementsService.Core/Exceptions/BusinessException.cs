using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }

        public BusinessException(string message) : base(message)
        {

        }
    }
}
