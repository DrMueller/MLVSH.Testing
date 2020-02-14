using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Models;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.YamlDtos;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models;
using YamlDotNet.Serialization;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Services.Implementation
{
    public class TestConfigurationFactory : ITestConfigurationFactory
    {
        private readonly IFileSystem _fileSystem;
        private readonly List<ITestFramework> _testFrameworks;

        public TestConfigurationFactory(IFileSystem fileSystem, List<ITestFramework> testFrameworks)
        {
            _fileSystem = fileSystem;
            _testFrameworks = testFrameworks;
        }

        public TestConfiguration Create(string projectName)
        {
            var filePath = $@"C:\MLSVH.Testing\{projectName}.yaml";
            var fileText = _fileSystem.File.ReadAllText(filePath);

            var yamlDeserializer = new Deserializer();
            var testConfigDto = yamlDeserializer.Deserialize<TestConfigurationDto>(fileText);

            var testFramework = ParseTestFramework(testConfigDto.TestFrameworkName);

            var config = new TestConfiguration(
                testFramework,
                testConfigDto.BaseNamespace,
                testConfigDto.TestProjectBasePath);

            return config;
        }

        private ITestFramework ParseTestFramework(string frameworkName)
        {
            var nameLower = frameworkName.ToLowerInvariant();
            var testFramework = _testFrameworks.Single(f => f.FrameworkName.ToLowerInvariant() == nameLower);

            return testFramework;
        }
    }
}