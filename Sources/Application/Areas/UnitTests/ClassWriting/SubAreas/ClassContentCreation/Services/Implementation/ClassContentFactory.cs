using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassContentCreation.Services.Servants;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassContentCreation.Services.Implementation
{
    public class ClassContentFactory : IClassContentFactory
    {
        private readonly IClassBuilder _classBuilder;

        public ClassContentFactory(IClassBuilder classBuilder)
        {
            _classBuilder = classBuilder;
        }

        public string CreateContent(ClassInformation classInfo, TestConfiguration testConfig)
        {
            var cls = _classBuilder.Initialize(classInfo, testConfig.TestFramework)
                .AppendFields()
                .AppendSetupMethod()
                .AppendExamplaryMethod()
                .Build();

            var nameSpace = CreateNamespace(classInfo, testConfig);
            nameSpace = nameSpace.AddMembers(cls);

            var syntaxFactory = SyntaxFactory.CompilationUnit();
            syntaxFactory = AppendUsings(syntaxFactory, classInfo, testConfig.TestFramework);
            syntaxFactory = syntaxFactory.AddMembers(nameSpace);

            var classContent = syntaxFactory
                .NormalizeWhitespace()
                .ToFullString();

            return classContent;
        }

        private static NamespaceDeclarationSyntax CreateNamespace(ClassInformation classInfo, TestConfiguration testConfig)
        {
            var relativeNamespacePart = classInfo
                .NamespaceDecl
                .Replace(testConfig.ApplicationProjectBaseNamespace, string.Empty);

            var nameSpace = testConfig.TestProjectBaseNamespace + relativeNamespacePart;
            var ns = SyntaxFactory
                .NamespaceDeclaration(SyntaxFactory.ParseName(nameSpace))
                .NormalizeWhitespace();

            return ns;
        }

        private static CompilationUnitSyntax AppendUsings(
            CompilationUnitSyntax syntaxFactory,
            ClassInformation classInfo,
            ITestFramework testFramework)
        {
            classInfo.AppendUsing(UsingEntry.CreateFrom("Moq"));
            classInfo.AppendUsing(testFramework.UsingEntry);
            classInfo.AppendUsing(UsingEntry.CreateFrom(classInfo.NamespaceDecl));

            foreach (var usingName in classInfo.SortedUsingEntries)
            {
                syntaxFactory = syntaxFactory
                    .AddUsings(
                        SyntaxFactory.UsingDirective(
                            SyntaxFactory.ParseName(usingName.Value)));
            }

            return syntaxFactory;
        }
    }
}