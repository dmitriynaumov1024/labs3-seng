using System;

public static class Utils
{
    public static T Clamp<T> (this T value, T lower, T upper) where T: IComparable<T>
    {
        if (value.CompareTo(upper)>0) return upper;
        if (value.CompareTo(lower)<0) return lower;
        return value;
    }
}
