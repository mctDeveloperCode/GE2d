using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Xunit;

namespace RunAbout.Tests;

///<summary>Assert methods that apply float precision.</summary>
///<remarks>
///  Use these methods in tests to compare float values. This will ensure that
///  all asserts on all floats use the same precision for comparisons.
///</remarks>
internal static class MathAssert
{
    public static void Equal(float expected, float actual) =>
        Assert.Equal(expected, actual, precision);

    public static void Equal(Vector2 expected, Vector2 actual) =>
        Assert.Equal(expected, actual, vector2EqualityComparer);

    private static readonly Vector2EqualityComparer vector2EqualityComparer = new ();

    private const int precision = 6;

    private sealed class Vector2EqualityComparer : IEqualityComparer<Vector2>
    {
        public bool Equals(Vector2 lhs, Vector2 rhs) =>
            MathF.Round(lhs.X, precision) == MathF.Round(rhs.X, precision) &&
            MathF.Round(lhs.Y, precision) == MathF.Round(rhs.Y, precision);

        public int GetHashCode(Vector2 _) =>
            throw new NotImplementedException();
    }
}
