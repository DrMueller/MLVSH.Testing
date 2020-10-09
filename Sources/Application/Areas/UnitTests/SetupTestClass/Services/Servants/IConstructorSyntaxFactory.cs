using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants
{
    public interface IConstructorSyntaxFactory
    {
        ConstructorDeclarationSyntax CreateConstructor(ClassInformation classInfo);
    }
}