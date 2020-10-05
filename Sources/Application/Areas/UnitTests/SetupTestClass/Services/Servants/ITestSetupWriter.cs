using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants
{
    public interface ITestSetupWriter
    {
        string WriteSetup(ClassInformation classInfo);
    }
}