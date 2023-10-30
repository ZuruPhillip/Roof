using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ZURU.Roof.EntityFrameworkCore;
using ZURU.Roof.Paths;

namespace ZURU.Roof.Implements.RobotPaths
{
    public class RobotPathRepository : EfCoreRepository<RoofDbContext, RobotPath, long>, IRobotPathRepository
    {
        public RobotPathRepository(IDbContextProvider<RoofDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
