using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementSystem.Application.Results
{
   public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message) // data ve mesaj
        {

        }
        public ErrorDataResult(T data) : base(data, false) // sadece data
        {

        }
        public ErrorDataResult(string message) : base(default, false, message)// sadece mesaj verir
        {

        }
        public ErrorDataResult() : base(default, false) //hiçbir şey vermez
        {

        }
    }
}

