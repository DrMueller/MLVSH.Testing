using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models.Frameworks
{
    public class Xunit : ITestFramework
    {
        public TestAttribute ClassAttribute { get; } = TestAttribute.CreateNone();
        public TestClassSetup ClassSetup { get; } = new TestClassSetup(TestClassSetupType.Constructor, TestAttribute.CreateNone());
        public TestFrameworkType FrameworkType { get; } = TestFrameworkType.XUnit;
        public TestAttribute TestMethodAttribute { get; } = TestAttribute.CreateFrom("Fact");
        public UsingEntry UsingEntry { get; } = UsingEntry.CreateFrom("Xunit");
    }
}