using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementSystem.Application.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, message)
        {

        }
        public SuccessResult() : base(true) // base'in(Result) constructor'ına true paramtresi gönderir.
        {

        }
    }
}
