using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Servants
{
    public interface IClassInformationFactory
    {
        ClassInformation Create(string filePath);
    }
}