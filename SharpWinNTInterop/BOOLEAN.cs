using System;

namespace SharpWinNTInterop;

public readonly partial struct BOOLEAN : IComparable, IComparable<BOOLEAN>, IEquatable<BOOLEAN>, IFormattable
{
    private enum DefinedValue : byte
    {
        FALSE,
        TRUE
    }

    public readonly byte Value;

    public BOOLEAN(byte value) => Value = value;

    public static BOOLEAN FALSE => new((byte)DefinedValue.FALSE);

    public static BOOLEAN TRUE => new((byte)DefinedValue.TRUE);

    public static bool operator ==(BOOLEAN left, BOOLEAN right) => left.Value == right.Value;

    public static bool operator !=(BOOLEAN left, BOOLEAN right) => left.Value != right.Value;

    public static bool operator <(BOOLEAN left, BOOLEAN right) => left.Value < right.Value;

    public static bool operator <=(BOOLEAN left, BOOLEAN right) => left.Value <= right.Value;

    public static bool operator >(BOOLEAN left, BOOLEAN right) => left.Value > right.Value;

    public static bool operator >=(BOOLEAN left, BOOLEAN right) => left.Value >= right.Value;

    public static implicit operator bool(BOOLEAN value) => value.Value != (byte)DefinedValue.FALSE;

    public static implicit operator BOOLEAN(bool value) => new(value ? (byte)DefinedValue.TRUE : (byte)DefinedValue.FALSE);

    public static bool operator false(BOOLEAN value) => value.Value == (byte)DefinedValue.FALSE;

    public static bool operator true(BOOLEAN value) => value.Value != (byte)DefinedValue.FALSE;

    public static implicit operator BOOLEAN(sbyte value) => new((byte)value);

    public static explicit operator sbyte(BOOLEAN value) => (sbyte)value.Value;

    public static implicit operator BOOLEAN(short value) => new((byte)value);

    public static explicit operator short(BOOLEAN value) => value.Value;

    public static implicit operator BOOLEAN(int value) => new((byte)value);

    public static implicit operator int(BOOLEAN value) => value.Value;

    public static explicit operator BOOLEAN(long value) => new((byte)value);

    public static implicit operator long(BOOLEAN value) => value.Value;

    public static explicit operator BOOLEAN(nint value) => new((byte)value);

    public static implicit operator nint(BOOLEAN value) => value.Value;

    public static implicit operator BOOLEAN(byte value) => new(value);

    public static explicit operator byte(BOOLEAN value) => value.Value;

    public static implicit operator BOOLEAN(ushort value) => new((byte)value);

    public static explicit operator ushort(BOOLEAN value) => value.Value;

    public static explicit operator BOOLEAN(uint value) => new((byte)value);

    public static explicit operator uint(BOOLEAN value) => value.Value;

    public static explicit operator BOOLEAN(ulong value) => new((byte)value);

    public static explicit operator ulong(BOOLEAN value) => value.Value;

    public static explicit operator BOOLEAN(nuint value) => new((byte)value);

    public static explicit operator nuint(BOOLEAN value) => value.Value;

    public int CompareTo(object? obj)
    {
        if (obj is BOOLEAN other) return CompareTo(other);
        else return obj is null ? 1 : throw new ArgumentException(WErrorMessage.ObjNotBOOLEAN);
    }

    public int CompareTo(BOOLEAN other) => Value.CompareTo(other.Value);

    public override bool Equals(object? obj) => obj is BOOLEAN other && Equals(other);

    public bool Equals(BOOLEAN other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => $"BOOLEAN:{(Value != (byte)DefinedValue.FALSE).ToString().ToUpper()}";

    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotSupportedException(WErrorMessage.TargetAPINotSupported);

    public static bool BoolEquals(BOOLEAN left, BOOLEAN right) => (bool)left == (bool)right;
}
