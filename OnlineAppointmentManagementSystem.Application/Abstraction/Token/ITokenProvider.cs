﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentManagementSystem.Application.Abstraction.Token
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}