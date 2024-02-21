using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Application.Services.Interfaces
{
    public  interface IEmail
    {
        Task<bool> Send();
    }
}
