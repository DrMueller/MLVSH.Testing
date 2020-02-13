using System;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.Models
{
    public class UsingEntry : IComparable<UsingEntry>
    {
        public bool IsSystemUsing => Value.ToLowerInvariant().StartsWith("system.");
        public string Value { get; }

        public UsingEntry(string value)
        {
            Value = value;
        }

        public int CompareTo(UsingEntry other)
        {
            if (IsSystemUsing && !other.IsSystemUsing)
            {
                return -1;
            }

            if (!IsSystemUsing && other.IsSystemUsing)
            {
                return 1;
            }

            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }
}