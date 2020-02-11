using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models
{
    public class ClassInfo
    {
        public string ClassName { get; }
        public IReadOnlyCollection<Constructor> Constructors { get; }
        public string NamespaceDecl { get; }

        public ClassInfo(string className, string namespaceDecl, IReadOnlyCollection<Constructor> constructors)
        {
            Guard.StringNotNullOrEmpty(() => className);
            Guard.StringNotNullOrEmpty(() => namespaceDecl);
            Guard.ObjectNotNull(() => constructors);

            ClassName = className;
            NamespaceDecl = namespaceDecl;
            Constructors = constructors;
        }
    }
}