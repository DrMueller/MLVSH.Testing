namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services.Servants.Implementation
{
    public class ValueAligner : IValueAligner
    {
        public string CreateMockFieldName(string paramName)
        {
            var fieldName = $"_{paramName}Mock";
            return fieldName;
        }
    }
}