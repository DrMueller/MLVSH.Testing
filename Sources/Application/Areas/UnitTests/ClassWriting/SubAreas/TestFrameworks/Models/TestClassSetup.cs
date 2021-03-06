﻿namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.TestFrameworks.Models
{
    public class TestClassSetup
    {
        public TestAttribute SetupMethodAttribute { get; }
        public TestClassSetupType SetupType { get; }

        public TestClassSetup(
            TestClassSetupType setupType,
            TestAttribute setupMethodAttribute)
        {
            SetupType = setupType;
            SetupMethodAttribute = setupMethodAttribute;
        }
    }
}