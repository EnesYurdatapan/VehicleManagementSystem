using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementSystem.Application.Results
{
    public class Result : IResult
    {
       

        public Result(bool success, string message):this(success) // hem bunu hem de alttaki tek parametreliyi çalıştırır.
        {
            Message = message;

            
        }
        public Result(bool success)// Mesaj göndermek istemediğimiz zaman
        {
            
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
