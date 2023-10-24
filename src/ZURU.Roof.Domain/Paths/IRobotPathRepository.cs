using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using ZURU.Roof.Roofs;

namespace ZURU.Roof.Paths
{
    public interface IRobotPathRepository : IRepository<RobotPath, Guid>
    {
    }
}
