using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Services.Implementation
{
    public class TestFrameworkFactory : ITestFrameworkFactory
    {
        private readonly IFileSystem _fileSystem;
        private readonly List<ITestFramework> _testFrameworks;

        public TestFrameworkFactory(IFileSystem fileSystem, List<ITestFramework> testFrameworks)
        {
            _fileSystem = fileSystem;
            _testFrameworks = testFrameworks;
        }

        public ITestFramework CreateForProject()
        {
            var lines = _fileSystem.File.ReadAllLines(@"C:\Users\mlm\Desktop\Stuff\Config.txt");
            var frameworkType = (TestFrameworkType)Enum.Parse(typeof(TestFrameworkType), lines[0]);

            var testFramework = _testFrameworks.Single(f => f.FrameworkType == frameworkType);
            return testFramework;
        }
    }
}