using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services
{
    public interface IUnitTestFileInitializer
    {
        void Initialize(string filePath);
    }
}