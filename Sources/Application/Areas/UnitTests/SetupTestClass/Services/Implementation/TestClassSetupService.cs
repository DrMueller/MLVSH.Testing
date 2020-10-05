﻿using System;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Services;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Implementation
{
    public class TestClassSetupService : ITestClassSetupService
    {
        private readonly IClassInformationFactory _classInfoFactory;
        private readonly ITestSetupWriter _testSetupWriter;

        public TestClassSetupService(
            IClassInformationFactory classInfoFactory,
            ITestSetupWriter testSetupWriter)
        {
            _classInfoFactory = classInfoFactory;
            _testSetupWriter = testSetupWriter;
        }

        public void SetupTestClass(string filePath)
        {
            var classInfo = _classInfoFactory.Create(filePath);

            var str = _testSetupWriter.WriteSetup(classInfo);

            // Load constructor metadata from filepaht
            // Create Structure
            // Per service, one mock field
            // create sut
            // pass mock objects to constructor
            // Save construct to Zwischenablage

            throw new NotImplementedException();
        }
    }
}