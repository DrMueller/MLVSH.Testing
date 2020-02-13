using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Servants.Implementation
{
    public class NamespaceFactory : INamespaceFactory
    {
        public NamespaceDeclarationSyntax CreateNamespace(ClassInformation classInfo, ITestFramework testFramework)
        {
            var ns = SyntaxFactory
                .NamespaceDeclaration(SyntaxFactory.ParseName(classInfo.NamespaceDecl))
                .NormalizeWhitespace();

            ns = AppendUsings(ns, classInfo, testFramework);
            return ns;
        }

        private static NamespaceDeclarationSyntax AppendUsings(
            NamespaceDeclarationSyntax ns,
            ClassInformation classInfo,
            ITestFramework testFramework)
        {
            classInfo.AppendUsing(UsingEntry.CreateFrom("Moq"));
            classInfo.AppendUsing(testFramework.UsingEntry);

            foreach (var usingName in classInfo.SortedUsingEntries)
            {
                ns = ns.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(usingName.Value)));
            }

            return ns;
        }
    }
}