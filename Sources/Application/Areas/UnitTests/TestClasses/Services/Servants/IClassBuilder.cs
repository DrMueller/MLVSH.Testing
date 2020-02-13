using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Servants
{
    public interface IClassBuilder
    {
        IClassBuilder AppendExamplaryMethod();

        IClassBuilder AppendFields();

        IClassBuilder AppendSetupMethod();

        ClassDeclarationSyntax Build();

        IClassBuilder Initialize(ClassInformation classInfo, ITestFramework testFramework);
    }
}