using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassContentCreation.Services;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Services;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Services;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFileWriting.Services;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.Orchestration.Services.Implementation
{
    public class UnitTestClassWriter : IUnitTestClassWriter
    {
        private readonly IClassContentFactory _classContentFactory;
        private readonly IClassInformationFactory _classInfoFactory;
        private readonly ITestConfigurationFactory _testConfigFactory;
        private readonly ITestFileWriter _testFileWriter;

        public UnitTestClassWriter(
            IClassInformationFactory classInfoFactory,
            ITestConfigurationFactory testConfigFactory,
            IClassContentFactory classContentFactory,
            ITestFileWriter testFileWriter)
        {
            _classInfoFactory = classInfoFactory;
            _testConfigFactory = testConfigFactory;
            _classContentFactory = classContentFactory;
            _testFileWriter = testFileWriter;
        }

        public void CreateTestClass(string fileToTestPath, string selectedProjectName)
        {
            var testConfig = _testConfigFactory.Create(selectedProjectName);
            var classInfo = _classInfoFactory.Create(fileToTestPath);
            var classContent = _classContentFactory.CreateContent(classInfo, testConfig);
            _testFileWriter.WriteToTestLocation(testConfig, classInfo, classContent);
        }
    }
}