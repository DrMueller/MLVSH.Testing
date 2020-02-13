using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Services
{
    public interface ITestFrameworkFactory
    {
        ITestFramework CreateForProject();
    }
}