using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFileWriting.Services
{
    public interface ITestFileWriter
    {
        void WriteToTestLocation(
            TestConfiguration testConfig,
            ClassInformation classInfo,
            string fileContent);
    }
}