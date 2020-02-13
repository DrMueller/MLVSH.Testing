using System.Collections.Generic;
namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models
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