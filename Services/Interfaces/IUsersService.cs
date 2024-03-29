﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_III_WPF.Services.Interfaces
{
    public interface IUsersService
    {
        bool Register(Users user);
        bool Login(Users user);
        IEnumerable<Users> GetAll();
        int GetUserIdByName(string userName);
    }
}
