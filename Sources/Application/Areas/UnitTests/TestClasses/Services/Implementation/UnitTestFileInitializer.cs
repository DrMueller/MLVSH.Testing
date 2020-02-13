using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Servants;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Services;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Implementation
{
    public class UnitTestFileInitializer : IUnitTestFileInitializer
    {
        private readonly IClassInformationFactory _classInfoFactory;
        private readonly IFileSystem _fileSystem;
        private readonly ITestFrameworkFactory _testFrameworkFactory;

        public UnitTestFileInitializer(
            IFileSystem fileSystem,
            IClassInformationFactory classInfoFactory,
            ITestFrameworkFactory testFrameworkFactory)
        {
            _fileSystem = fileSystem;
            _classInfoFactory = classInfoFactory;
            _testFrameworkFactory = testFrameworkFactory;
        }

        public void Initialize(string filePath)
        {
            var testFramework = _testFrameworkFactory.CreateForProject();
            var classInfo = _classInfoFactory.Create(filePath);

            var ns = CreateNamespace(classInfo.NamespaceDecl);
            ns = AppendUsings(ns, classInfo, testFramework);

            var cls = InitializeClass(classInfo.ClassName, testFramework.ClassAttribute);
            cls = AppendPrivateFields(cls, classInfo, testFramework.ClassSetup.SetupType == TestClassSetupType.Constructor);
            cls = AppendSetUpMethod(cls, classInfo, testFramework);
            cls = AppendExemplaryMethod(cls, testFramework.TestMethodAttribute);
            ns = ns.AddMembers(cls);

            var fileContent = ns
                .NormalizeWhitespace()
                .ToFullString();

            var testFilePath = filePath.Replace(classInfo.ClassName, classInfo.ClassName + "UnitTests");
            _fileSystem.File.WriteAllText(testFilePath, fileContent);
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

        private static ClassDeclarationSyntax AppendPrivateFields(
            ClassDeclarationSyntax cls,
            ClassInformation classInfo,
            bool applyReadOnlyModifier)
        {
            foreach (var param in classInfo.Constructor.Parameters)
            {
                cls = cls.AddMembers(
                    CreatePrivateField($"Mock<{param.ParameterType}>", "_" + param.ParameterName, applyReadOnlyModifier));
            }

            var sutField = CreatePrivateField(
                    classInfo.ClassName,
                    "_sut",
                    applyReadOnlyModifier)
                .WithTrailingTrivia(SyntaxFactory.EndOfLine(""));

            cls = cls.AddMembers(sutField);

            return cls;
        }

        private static ClassDeclarationSyntax AppendExemplaryMethod(ClassDeclarationSyntax cd, TestAttribute testMethodAttribute)
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
                .WithBody(SyntaxFactory.Block(statements));

            if (testMethodAttribute.ShouldBeApplied)
            {
                var attributes = SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(testMethodAttribute.Value))
                    )
                );

                method = method.AddAttributeLists(attributes);
            }

            return cd.AddMembers(method);
        }

        private static ClassDeclarationSyntax AppendSetUpMethod(
            ClassDeclarationSyntax cd,
            ClassInformation classInfo,
            ITestFramework testFramework)
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

            for (var i = 0; i < classInfo.Constructor.Parameters.Count; i++)
            {
                var ctorParam = classInfo.Constructor.Parameters.ElementAt(i);
                sb.Append($"_{ctorParam.ParameterName}.Object");
                if (i < classInfo.Constructor.Parameters.Count - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine(");");
                }
            }

            var str = sb.ToString();
            statements.Add(SyntaxFactory.ParseStatement(str));

            BaseMethodDeclarationSyntax setupMethod;
            if (testFramework.ClassSetup.SetupType == TestClassSetupType.Constructor)
            {
                setupMethod = SyntaxFactory.ConstructorDeclaration(cd.Identifier.ToString());
            }
            else
            {
                var attributes = SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("SetUp"))
                    )
                );

                setupMethod = SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Align")
                    .AddAttributeLists(attributes);
            }

            setupMethod = setupMethod
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements));

            cd = cd.AddMembers(setupMethod);
            return cd;
        }

        private static FieldDeclarationSyntax CreatePrivateField(string className, string variableName, bool applyReadOnlyModiefier)
        {
            var variableDeclaration = SyntaxFactory
                .VariableDeclaration(SyntaxFactory.ParseTypeName(className))
                .AddVariables(SyntaxFactory.VariableDeclarator(variableName));

            var field = SyntaxFactory.FieldDeclaration(variableDeclaration)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));

            if (applyReadOnlyModiefier)
            {
                field = field.AddModifiers(SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
            }

            return field;
        }

        private static ClassDeclarationSyntax InitializeClass(string className, TestAttribute classAttribute)
        {
            var classDeclaration = SyntaxFactory.ClassDeclaration(className + "UnitTests");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            if (classAttribute.ShouldBeApplied)
            {
                classDeclaration = classDeclaration.AddAttributeLists(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(classAttribute.Value)))));
            }

            return classDeclaration;
        }

        private static NamespaceDeclarationSyntax CreateNamespace(string classNamespace)
        {
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(classNamespace)).NormalizeWhitespace();
            return ns;
        }
    }
}