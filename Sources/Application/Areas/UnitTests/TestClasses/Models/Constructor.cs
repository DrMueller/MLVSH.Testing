using System.Collections.Generic;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestClasses.Models
{
    public class Constructor
    {
        public IReadOnlyCollection<Parameter> Parameters { get; }

        public Constructor(IReadOnlyCollection<Parameter> parameters)
        {
            Parameters = parameters;
        }
    }
}