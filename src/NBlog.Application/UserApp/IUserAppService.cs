using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBlog.Domain.Entities;

namespace NBlog.Application.UserApp
{
    public interface IUserAppService
    {
        User CheckUser(string userName, string password);
    }
}
