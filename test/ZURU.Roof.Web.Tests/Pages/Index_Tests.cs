using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ZURU.Roof.Pages;

public class Index_Tests : RoofWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
