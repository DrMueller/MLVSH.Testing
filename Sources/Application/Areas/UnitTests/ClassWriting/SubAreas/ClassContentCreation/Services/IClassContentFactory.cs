using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassContentCreation.Services
{
    public interface IClassContentFactory
    {
        string CreateContent(ClassInformation classInfo, TestConfiguration testConfig);
    }
}