namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.TestConfiguration.Models
{
    public class TestAttribute
    {
        public string Value { get; }

        public bool ShouldBeApplied => !string.IsNullOrEmpty(Value);

        private TestAttribute(string value)
        {
            Value = value;
        }

        public static TestAttribute CreateFrom(string value)
        {
            return new TestAttribute(value);
        }

        public static TestAttribute CreateNone()
        {
            return new TestAttribute(string.Empty);
        }
    }
}