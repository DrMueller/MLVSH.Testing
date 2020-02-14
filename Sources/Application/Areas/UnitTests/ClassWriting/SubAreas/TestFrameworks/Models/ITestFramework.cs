using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models
{
    public interface ITestFramework
    {
        TestAttribute ClassAttribute { get; }
        TestAttribute TestMethodAttribute { get; }
        UsingEntry UsingEntry { get; }
        TestClassSetup ClassSetup { get; }

        string FrameworkName { get; }
    }
}