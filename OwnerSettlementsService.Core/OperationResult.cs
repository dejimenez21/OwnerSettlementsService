using OwnerSettlementsService.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerSettlementsService.Core
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public BusinessException Error { get; set; }

        public OperationResult(T data)
        {
            this.Data = data;
            this.Success = true;
        }

        public OperationResult(BusinessException exception)
        {
            this.Success = false;
            this.Error = exception;
        }

        public static implicit operator OperationResult<T>(T entity) =>
            new OperationResult<T>(entity);

        public static implicit operator T(OperationResult<T> result) =>
            result.Data;

        public static implicit operator OperationResult<T>(BusinessException exception) => 
            new OperationResult<T>(exception);
    }
}
