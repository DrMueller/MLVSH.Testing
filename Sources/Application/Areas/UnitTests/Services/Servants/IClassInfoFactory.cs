using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Services.Servants
{
   public interface IClassInfoFactory
    {
        Task<ClassInfo> CreateAsync(string filePath);
    }
}
