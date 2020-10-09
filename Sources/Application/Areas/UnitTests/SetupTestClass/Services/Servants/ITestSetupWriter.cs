using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants
{
    public interface ITestSetupWriter
    {
        string WriteSetup(ClassInformation classInfo);
    }
}