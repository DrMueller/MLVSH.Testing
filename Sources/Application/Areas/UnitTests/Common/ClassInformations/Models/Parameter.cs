
namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.SubAreas.ClassInformations.Models
{
    public class Parameter
    {
        public string ParameterName { get; }
        public string ParameterType { get; }

        public Parameter(string parameterType, string parameterName)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
        }
    }
}