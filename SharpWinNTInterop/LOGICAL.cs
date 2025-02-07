using System;

namespace SharpWinNTInterop;

public readonly partial struct LOGICAL : IComparable, IComparable<LOGICAL>, IEquatable<LOGICAL>, IFormattable
{
    private enum LOGICALValue : ulong
    {
        FALSE = 0,
        TRUE = 1
    }

    public readonly ulong Value;

    public LOGICAL(ulong value) => Value = value;

    public static LOGICAL FALSE => new((ulong)LOGICALValue.FALSE);

    public static LOGICAL TRUE => new((ulong)LOGICALValue.TRUE);

    public static bool operator ==(LOGICAL left, LOGICAL right) => left.Value == right.Value;

    public static bool operator !=(LOGICAL left, LOGICAL right) => left.Value != right.Value;

    public static bool operator <(LOGICAL left, LOGICAL right) => left.Value < right.Value;

    public static bool operator <=(LOGICAL left, LOGICAL right) => left.Value <= right.Value;

    public static bool operator >(LOGICAL left, LOGICAL right) => left.Value > right.Value;

    public static bool operator >=(LOGICAL left, LOGICAL right) => left.Value >= right.Value;

    public static implicit operator bool(LOGICAL value) => value.Value != (ulong)LOGICALValue.FALSE;

    public static implicit operator LOGICAL(bool value) => new(value ? (ulong)LOGICALValue.TRUE : (ulong)LOGICALValue.FALSE);

    public static bool operator false(LOGICAL value) => value.Value == (ulong)LOGICALValue.FALSE;

    public static bool operator true(LOGICAL value) => value.Value != (ulong)LOGICALValue.FALSE;

    public static implicit operator LOGICAL(sbyte value) => new((ulong)value);

    public static explicit operator sbyte(LOGICAL value) => (sbyte)value.Value;

    public static implicit operator LOGICAL(short value) => new((ulong)value);

    public static explicit operator short(LOGICAL value) => (short)value.Value;

    public static implicit operator LOGICAL(int value) => new((ulong)value);

    public static implicit operator int(LOGICAL value) => (int)value.Value;

    public static explicit operator LOGICAL(long value) => new((ulong)value);

    public static implicit operator long(LOGICAL value) => (long)value.Value;

    public static explicit operator LOGICAL(nint value) => new((ulong)value);

    public static implicit operator nint(LOGICAL value) => (nint)value.Value;

    public static implicit operator LOGICAL(byte value) => new(value);

    public static explicit operator byte(LOGICAL value) => (byte)value.Value;

    public static implicit operator LOGICAL(ushort value) => new(value);

    public static explicit operator ushort(LOGICAL value) => (ushort)value.Value;

    public static explicit operator LOGICAL(uint value) => new(value);

    public static explicit operator uint(LOGICAL value) => (uint)value.Value;

    public static explicit operator LOGICAL(ulong value) => new(value);

    public static explicit operator ulong(LOGICAL value) => value.Value;

    public static explicit operator LOGICAL(nuint value) => new(value);

    public static explicit operator nuint(LOGICAL value) => (nuint)value.Value;

    public int CompareTo(object? obj)
    {
        if (obj is LOGICAL other) return CompareTo(other);
        else return obj is null ? 1 : throw new ArgumentException(WErrorMessage.ObjNotLOGICAL);
    }

    public int CompareTo(LOGICAL other) => Value.CompareTo(other.Value);

    public override bool Equals(object? obj) => obj is LOGICAL other && Equals(other);

    public bool Equals(LOGICAL other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => $"LOGICAL:{(Value != (ulong)LOGICALValue.FALSE).ToString().ToUpper()}";

    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotSupportedException(WErrorMessage.TargetAPINotSupported);
}
