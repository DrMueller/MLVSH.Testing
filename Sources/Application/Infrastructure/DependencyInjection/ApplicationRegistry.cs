using System.IO.Abstractions;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models;
using StructureMap.Configuration.DSL;

namespace Mmu.Mlvsh.Testing.Application.Infrastructure.DependencyInjection
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<ApplicationRegistry>();
                    scanner.AddAllTypesOf<ITestFramework>();
                    scanner.WithDefaultConventions();
                });

            For<IFileSystem>().Use<FileSystem>().Singleton();
        }
    }
}