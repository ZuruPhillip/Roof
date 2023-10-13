using System.Threading.Tasks;

namespace ZURU.Roof.Data;

public interface IRoofDbSchemaMigrator
{
    Task MigrateAsync();
}
