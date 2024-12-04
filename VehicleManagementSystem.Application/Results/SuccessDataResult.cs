using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementSystem.Application.Results
{
   public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message):base(data,true,message) // data ve mesaj
        {

        }
        public SuccessDataResult(T data):base(data,true) // sadece data
        {

        }
        public SuccessDataResult(string message):base(default,true,message)// sadece mesaj verir
        {

        }
        public SuccessDataResult():base(default,true) //hiçbir şey vermez
        {

        }
    }
}
