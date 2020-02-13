using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Services.Servants;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Services.Implementation
{
    public class UnitTestFileInitializer : IUnitTestFileInitializer
    {
        private readonly IClassInformationFactory _classInfoFactory;
        private readonly IFileSystem _fileSystem;

        public UnitTestFileInitializer(
            IFileSystem fileSystem,
            IClassInformationFactory classInfoFactory)
        {
            _fileSystem = fileSystem;
            _classInfoFactory = classInfoFactory;
        }

        public async Task InitializeAsync(string filePath)
        {
            var classInfo = _classInfoFactory.Create(filePath);

            var ns = CreateNamespace(classInfo.NamespaceDecl);
            ns = AppendUsings(ns, classInfo);

            var cls = InitializeClass(classInfo.ClassName);
            cls = AppendPrivateFields(cls, classInfo);
            cls = AppendSetUpMethod(cls, classInfo);
            cls = AppendExemplaryMethod(cls);
            ns = ns.AddMembers(cls);

            var fileContent = ns
                .NormalizeWhitespace()
                .ToFullString();

            var testFilePath = filePath.Replace(classInfo.ClassName, classInfo.ClassName + "UnitTests");
            _fileSystem.File.WriteAllText(testFilePath, fileContent);
        }

        private static NamespaceDeclarationSyntax AppendUsings(NamespaceDeclarationSyntax ns, ClassInformation classInfo)
        {
            classInfo.AppendUsing("Moq");
            classInfo.AppendUsing("NUnit.Framework");

            foreach (var usingName in classInfo.SortedUsingEntries)
            {
                ns = ns.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(usingName.Value)));
            }

            return ns;
        }

        private static ClassDeclarationSyntax AppendPrivateFields(ClassDeclarationSyntax cls, ClassInformation classInfo)
        {
            foreach (var param in classInfo.Constructor.Parameters)
            {
                cls = cls.AddMembers(CreatePrivateField($"Mock<{param.ParameterType}>", "_" + param.ParameterName));
            }

            var sutField = CreatePrivateField(classInfo.ClassName, "_sut").WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
            cls = cls.AddMembers(sutField);

            return cls;
        }

        private static ClassDeclarationSyntax AppendExemplaryMethod(ClassDeclarationSyntax cd)
        {
            var statements = new List<StatementSyntax>
            {
                SyntaxFactory.ParseStatement(string.Empty).WithTrailingTrivia(SyntaxFactory.Comment("// Arrange")),
                SyntaxFactory.ParseStatement(string.Empty).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed),
                SyntaxFactory.ParseStatement(string.Empty).WithTrailingTrivia(SyntaxFactory.Comment("// Act")),
                SyntaxFactory.ParseStatement(string.Empty).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed),
                SyntaxFactory.ParseStatement(string.Empty).WithTrailingTrivia(SyntaxFactory.Comment("// Assert")),
                SyntaxFactory.ParseStatement(string.Empty).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed)
            };

            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "UnitOfWork_InitialCondition_ExpectedOutcome")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements))
                .AddAttributeLists(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Test")))));

            return cd.AddMembers(method);
        }

        private static ClassDeclarationSyntax AppendSetUpMethod(ClassDeclarationSyntax cd, ClassInformation classInfo)
        {
            var statements = new List<StatementSyntax>();
            var sb = new StringBuilder();

            foreach (var ctorParam in classInfo.Constructor.Parameters)
            {
                statements.Add(
                    SyntaxFactory.ParseStatement($"_{ctorParam.ParameterName}= new Mock<{ctorParam.ParameterType}>();")
                        .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed));
            }

            sb.AppendLine($"_sut = new {classInfo.ClassName}(");
            foreach (var ctorParam in classInfo.Constructor.Parameters)
            {
                sb.AppendLine($"_{ctorParam.ParameterName}.Object");
            }

            sb.AppendLine(");");

            var str = sb.ToString();
            statements.Add(SyntaxFactory.ParseStatement(str));

            var methodDeclaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Align")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements));

            var attributes = SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("SetUp"))
                )
            );

            methodDeclaration = methodDeclaration.AddAttributeLists(attributes);
            methodDeclaration = methodDeclaration
                .WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed)
                .WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed);

            cd = cd.AddMembers(methodDeclaration);

            return cd;
        }

        private static FieldDeclarationSyntax CreatePrivateField(string className, string variableName)
        {
            var variableDeclaration = SyntaxFactory
                .VariableDeclaration(SyntaxFactory.ParseTypeName(className))
                .AddVariables(SyntaxFactory.VariableDeclarator(variableName));

            var field = SyntaxFactory.FieldDeclaration(variableDeclaration)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));

            return field;
        }

        private static ClassDeclarationSyntax InitializeClass(string className)
        {
            var classDeclaration = SyntaxFactory.ClassDeclaration(className + "UnitTests");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            classDeclaration = classDeclaration.AddAttributeLists(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestFixture")))));

            return classDeclaration;
        }

        private static NamespaceDeclarationSyntax CreateNamespace(string classNamespace)
        {
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(classNamespace)).NormalizeWhitespace();
            return ns;
        }
    }
}