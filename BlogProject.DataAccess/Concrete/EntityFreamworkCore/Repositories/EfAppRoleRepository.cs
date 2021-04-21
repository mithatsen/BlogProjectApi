using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTProject.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfAppRoleRepository: EfGenericRepository<AppRole>,IAppRoleDal
    {
    }
}
