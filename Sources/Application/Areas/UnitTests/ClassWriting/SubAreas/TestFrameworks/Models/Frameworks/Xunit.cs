﻿using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models.Frameworks
{
    public class Xunit : ITestFramework
    {
        public TestAttribute ClassAttribute { get; } = TestAttribute.CreateNone();
        public TestClassSetup ClassSetup { get; } = new TestClassSetup(TestClassSetupType.Constructor, TestAttribute.CreateNone());
        public string FrameworkName { get; } = "XUnit";
        public TestAttribute TestMethodAttribute { get; } = TestAttribute.CreateFrom("Fact");
        public UsingEntry UsingEntry { get; } = UsingEntry.CreateFrom("Xunit");
    }
}