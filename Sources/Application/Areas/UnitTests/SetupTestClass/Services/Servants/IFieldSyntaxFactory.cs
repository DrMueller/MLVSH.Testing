using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants
{
    public interface IFieldSyntaxFactory
    {
        IReadOnlyCollection<MemberDeclarationSyntax> CreateFields(ClassInformation classInfo);
    }
}