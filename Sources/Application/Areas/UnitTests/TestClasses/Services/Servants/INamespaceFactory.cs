using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Servants
{
    public interface INamespaceFactory
    {
        NamespaceDeclarationSyntax CreateNamespace(ClassInformation classInfo, ITestFramework testFramework);
    }
}