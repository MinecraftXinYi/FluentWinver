using System;

namespace SharpWinNTInterop;

public unsafe readonly partial struct HANDLE : IComparable, IComparable<HANDLE>, IEquatable<HANDLE>, IFormattable
{
    public readonly void* Value;

    public HANDLE(void* value) => Value = value;

    public static HANDLE NULL => new(null);

    public static bool operator ==(HANDLE left, HANDLE right) => left.Value == right.Value;

    public static bool operator !=(HANDLE left, HANDLE right) => left.Value != right.Value;

    public static bool operator <(HANDLE left, HANDLE right) => left.Value < right.Value;

    public static bool operator <=(HANDLE left, HANDLE right) => left.Value <= right.Value;

    public static bool operator >(HANDLE left, HANDLE right) => left.Value > right.Value;

    public static bool operator >=(HANDLE left, HANDLE right) => left.Value >= right.Value;

    public static explicit operator HANDLE(void* value) => new(value);

    public static implicit operator void*(HANDLE value) => value.Value;

    public static explicit operator HANDLE(byte value) => new((void*)value);

    public static explicit operator byte(HANDLE value) => (byte)value.Value;

    public static explicit operator HANDLE(short value) => new((void*)value);

    public static explicit operator short(HANDLE value) => (short)value.Value;

    public static explicit operator HANDLE(int value) => new((void*)value);

    public static explicit operator int(HANDLE value) => (int)value.Value;

    public static explicit operator HANDLE(long value) => new((void*)value);

    public static explicit operator long(HANDLE value) => (long)value.Value;

    public static explicit operator HANDLE(nint value) => new((void*)value);

    public static implicit operator nint(HANDLE value) => (nint)value.Value;

    public static explicit operator HANDLE(sbyte value) => new((void*)value);

    public static explicit operator sbyte(HANDLE value) => (sbyte)value.Value;

    public static explicit operator HANDLE(ushort value) => new((void*)value);

    public static explicit operator ushort(HANDLE value) => (ushort)value.Value;

    public static explicit operator HANDLE(uint value) => new((void*)value);

    public static explicit operator uint(HANDLE value) => (uint)value.Value;

    public static explicit operator HANDLE(ulong value) => new((void*)value);

    public static explicit operator ulong(HANDLE value) => (ulong)value.Value;

    public static explicit operator HANDLE(nuint value) => new((void*)value);

    public static implicit operator nuint(HANDLE value) => (nuint)value.Value;

    public static explicit operator HANDLE(IntPtr value) => new((void*)value);

    public static implicit operator IntPtr(HANDLE value) => (IntPtr)value.Value;

    public int CompareTo(object? obj)
    {
        if (obj is HANDLE other) return CompareTo(other);
        else return obj is null ? 1 : throw new ArgumentException(WErrorMessage.ObjNotHANDLE);
    }

    public int CompareTo(HANDLE other)
    {
        if (sizeof(nuint) == sizeof(uint)) return ((uint)Value).CompareTo((uint)other.Value);
        else return ((ulong)Value).CompareTo((ulong)other.Value);
    }

    public override bool Equals(object? obj) => obj is HANDLE other && Equals(other);

    public bool Equals(HANDLE other) => ((nuint)Value).Equals((nuint)other.Value);

    public override int GetHashCode() => ((nuint)Value).GetHashCode();

    public override string ToString() => $"HANDLE:{(nuint)Value}";

    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotSupportedException(WErrorMessage.TargetAPINotSupported);

    public IntPtr ToIntPtr() => new(Value);

    public HANDLE FromIntPtr(IntPtr intPtr) => new(intPtr.ToPointer());
}
