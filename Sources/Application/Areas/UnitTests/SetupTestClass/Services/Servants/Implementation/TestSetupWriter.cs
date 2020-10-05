using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants.Implementation
{
    public class TestSetupWriter : ITestSetupWriter
    {
        public string WriteSetup(ClassInformation classInfo)
        {
            var cls = SyntaxFactory.ClassDeclaration("tra");
            foreach(var param in classInfo.Constructor.Parameters)
            {
                cls = cls.AddMembers(
                    CreatePrivateField($"Mock<{param.ParameterType}>", "_" + param.ParameterName));
            }

            var ctor = CreateConstructor(classInfo);
            cls = cls.AddMembers(ctor);

            var result = cls
                .NormalizeWhitespace()
                .ToFullString();

            return result;
        }
        
        private static ConstructorDeclarationSyntax CreateConstructor(ClassInformation classInfo)
        {
            var statements = new List<StatementSyntax>();
            var sb = new StringBuilder();

            foreach (var ctorParam in classInfo.Constructor.Parameters)
            {
                statements.Add(
                    SyntaxFactory.ParseStatement($"_{ctorParam.ParameterName}= new Mock<{ctorParam.ParameterType}>();"));
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

            var ctor = SyntaxFactory
                .ConstructorDeclaration(classInfo.ClassName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements));

            return ctor;
        }

        private static FieldDeclarationSyntax CreatePrivateField(string className, string variableName)
        {
            var variableDeclaration = SyntaxFactory
                .VariableDeclaration(SyntaxFactory.ParseTypeName(className))
                .AddVariables(SyntaxFactory.VariableDeclarator(variableName));

            var field = SyntaxFactory
                .FieldDeclaration(variableDeclaration)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));

            field = field.AddModifiers(SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));

            return field;
        }
    }
}
