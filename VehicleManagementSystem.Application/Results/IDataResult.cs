using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementSystem.Application.Results
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}
