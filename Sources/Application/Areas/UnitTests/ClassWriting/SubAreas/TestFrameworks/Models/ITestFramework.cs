using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models
{
    public interface ITestFramework
    {
        TestAttribute ClassAttribute { get; }
        TestClassSetup ClassSetup { get; }

        string FrameworkName { get; }
        TestAttribute TestMethodAttribute { get; }
        UsingEntry UsingEntry { get; }
    }
}