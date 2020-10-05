using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services;
using Mmu.Mlvsh.Testing.Application.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlvsh.Testing.Application.Tests.TestingAreas.Areas.UnitTests.SetupTestClass.Services
{
    public class TestClassSetupServiceTests
    {
        private readonly ITestClassSetupService _sut;

        public TestClassSetupServiceTests()
        {
            _sut = ApplicationServiceLocator.GetService<ITestClassSetupService>();
        }

        [Fact]
        public void Tra()
        {
            _sut.SetupTestClass(@"C:\MyGit\Personal\MLVSH\MLVSH.Testing\Sources\Application\Areas\UnitTests\ClassWriting\Orchestration\Services\Implementation\UnitTestClassWriter.cs");
        }
    }
}