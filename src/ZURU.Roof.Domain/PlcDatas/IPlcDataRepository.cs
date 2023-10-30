using System;
using Volo.Abp.Domain.Repositories;
using ZURU.Roof.Roofs;

namespace ZURU.Roof.PlcDatas
{
    public interface IPlcDataRepository : IRepository<PlcData, long>
    {
    }
}
