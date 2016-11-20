using NBlog.Domain.Entities;
using NBlog.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBlog.EntityFrameworkCore.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(NBlogDBContext dbcontext) : base(dbcontext)
        {

        }
    }
}
