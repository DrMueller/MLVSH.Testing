using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models
{
    public interface ITestFramework
    {
        TestAttribute ClassAttribute { get; }
        TestFrameworkType FrameworkType { get; }
        TestAttribute TestMethodAttribute { get; }
        UsingEntry UsingEntry { get; }
        TestClassSetup ClassSetup { get; }
    }
}