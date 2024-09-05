using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkV4.Application.Common.Helpers;

namespace ParkV4.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        long UserId { get; }
        string FullName { get; }
        bool IsAuthenticated { get; }
    }
}