using System.IO.Abstractions;
using Microsoft.CodeAnalysis;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Servants;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Services;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Services.Implementation
{
    public class UnitTestFileInitializer : IUnitTestFileInitializer
    {
        private readonly IClassBuilder _classBuilder;
        private readonly IClassInformationFactory _classInfoFactory;
        private readonly IFileSystem _fileSystem;
        private readonly INamespaceFactory _namespaceFactory;
        private readonly ITestFrameworkFactory _testFrameworkFactory;

        public UnitTestFileInitializer(
            IFileSystem fileSystem,
            IClassInformationFactory classInfoFactory,
            ITestFrameworkFactory testFrameworkFactory,
            INamespaceFactory namespaceFactory,
            IClassBuilder classBuilder)
        {
            _fileSystem = fileSystem;
            _classInfoFactory = classInfoFactory;
            _testFrameworkFactory = testFrameworkFactory;
            _namespaceFactory = namespaceFactory;
            _classBuilder = classBuilder;
        }

        public void Initialize(string filePath)
        {
            var classInfo = _classInfoFactory.Create(filePath);
            var fileContent = BuildFileContent(classInfo);

            var testFilePath = filePath.Replace(classInfo.ClassName, classInfo.ClassName + "UnitTests");
            _fileSystem.File.WriteAllText(testFilePath, fileContent);
        }

        private string BuildFileContent(ClassInformation classInfo)
        {
            var testFramework = _testFrameworkFactory.CreateForProject();

            var nameSpace = _namespaceFactory.CreateNamespace(classInfo, testFramework);

            var cls = _classBuilder.Initialize(classInfo, testFramework)
                .AppendFields()
                .AppendSetupMethod()
                .AppendExamplaryMethod()
                .Build();

            nameSpace = nameSpace.AddMembers(cls);

            var fileContent = nameSpace
                .NormalizeWhitespace()
                .ToFullString();

            return fileContent;
        }
    }
}