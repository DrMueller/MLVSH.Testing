using System.Collections.Generic;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Common.ClassInformations.Models
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