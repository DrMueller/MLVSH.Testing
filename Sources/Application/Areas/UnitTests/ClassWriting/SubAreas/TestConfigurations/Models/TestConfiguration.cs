using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Models
{
    public class TestConfiguration
    {
        public string ApplicationProjectBaseNamespace { get; }
        public ITestFramework TestFramework { get; }
        public string TestProjectBaseNamespace { get; }
        public string TestProjectBasePath { get; }

        public TestConfiguration(
            ITestFramework testFramework,
            string applicationProjectBaseNamespace,
            string testProjectBaseNamespace,
            string testProjectBasePath)
        {
            TestFramework = testFramework;
            ApplicationProjectBaseNamespace = applicationProjectBaseNamespace;
            TestProjectBaseNamespace = testProjectBaseNamespace;
            TestProjectBasePath = testProjectBasePath;
        }
    }
}