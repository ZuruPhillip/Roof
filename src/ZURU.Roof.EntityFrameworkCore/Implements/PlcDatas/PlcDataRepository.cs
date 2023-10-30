using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ZURU.Roof.EntityFrameworkCore;
using ZURU.Roof.PlcDatas;
using ZURU.Roof.Roofs;

namespace ZURU.Roof.Implements.Roofs
{
    public class PlcDataRepository : EfCoreRepository<RoofDbContext, PlcData, long>, IPlcDataRepository
    {
        public PlcDataRepository(IDbContextProvider<RoofDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
