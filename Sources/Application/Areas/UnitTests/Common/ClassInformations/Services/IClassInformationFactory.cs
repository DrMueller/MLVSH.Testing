using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Services
{
    public interface IClassInformationFactory
    {
        ClassInformation Create(string filePath);
    }
}