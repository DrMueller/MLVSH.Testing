using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models.Frameworks
{
    public class Nunit : ITestFramework
    {
        public TestAttribute ClassAttribute { get; } = TestAttribute.CreateFrom("Test");
        public TestFrameworkType FrameworkType { get; } = TestFrameworkType.NUnit;
        public TestAttribute TestMethodAttribute { get; } = TestAttribute.CreateFrom("Test");
        public UsingEntry UsingEntry { get; } = UsingEntry.CreateFrom("NUnit.Framework");

        public TestClassSetup ClassSetup { get; } = new TestClassSetup(TestClassSetupType.Method, TestAttribute.CreateFrom("SetUp"));
    }
}