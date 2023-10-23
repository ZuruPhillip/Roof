using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ZURU.Roof.EntityFrameworkCore;
using ZURU.Roof.Roofs;

namespace ZURU.Roof.Implements.Roofs
{
    public class RoofRecordRepository : EfCoreRepository<RoofDbContext, RoofRecord, Guid>, IRoofRecordRepository
    {
        public RoofRecordRepository(IDbContextProvider<RoofDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
