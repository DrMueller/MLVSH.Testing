using System.Threading.Tasks;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Services.Servants
{
    public interface IClassInformationFactory
    {
        ClassInformation Create(string filePath);
    }
}