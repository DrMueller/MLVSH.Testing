using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestConfigurations.Models
{
    public class TestConfiguration
    {
        public string BaseNamespace { get; }
        public ITestFramework TestFramework { get; }
        public string TestProjectBasePath { get; }

        public TestConfiguration(
            ITestFramework testFramework,
            string baseNamespace,
            string testProjectBasePath)
        {
            TestFramework = testFramework;
            BaseNamespace = baseNamespace;
            TestProjectBasePath = testProjectBasePath;
        }
    }
}