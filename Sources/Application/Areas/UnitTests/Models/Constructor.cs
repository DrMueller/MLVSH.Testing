using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models
{
    public class Constructor
    {
        public IReadOnlyCollection<Parameter> Parameters { get; }

        public Constructor(IReadOnlyCollection<Parameter> parameters)
        {
            Guard.ObjectNotNull(() => parameters);

            Parameters = parameters;
        }
    }
}