using System.Threading.Tasks;

namespace ZURU.Roof.Roofs
{
    public interface IRoofRecordAppService
    {
        Task AddRoofRecordAsync(RoofInputDto input);
    }
}