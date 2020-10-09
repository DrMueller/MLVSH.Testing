using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassContentCreation.Services.Servants
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