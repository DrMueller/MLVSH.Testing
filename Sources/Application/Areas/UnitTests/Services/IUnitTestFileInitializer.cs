using System.Threading.Tasks;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Services
{
    public interface IUnitTestFileInitializer
    {
        Task InitializeAsync(string filePath);
    }
}