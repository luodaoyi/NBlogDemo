using NBlog.Domain.Entities;
using NBlog.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBlog.EntityFrameworkCore.Repositories
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(NBlogDBContext dbcontext) : base(dbcontext)
        {

        }
    }
}
