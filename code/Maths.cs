namespace Phyksar.Numerics;

public static class Maths
{
	/// <summary>
	/// Computes the modulus of the number respecting negative numbers.
	/// </summary>
	/// <param name="value">
	/// The numeric value.
	/// </param>
	/// <param name="denominator">
	/// The denominator of modulus operation.
	/// </param>
	/// <returns>
	/// Returns the modulus of value.
	/// </returns>
	public static int UnsignedMod(this int value, int denominator)
	{
		return (value % denominator + denominator) % denominator;
	}

	/// <summary>
	/// Computes the modulus of the number respecting negative numbers.
	/// </summary>
	/// <param name="value">
	/// The numeric value.
	/// </param>
	/// <param name="denominator">
	/// The denominator of modulus operation.
	/// </param>
	/// <returns>
	/// Returns the modulus of value.
	/// </returns>
	public static long UnsignedMod(this long value, long denominator)
	{
		return (value % denominator + denominator) % denominator;
	}

	/// <summary>
	/// Checks if the number is power of 2.
	/// </summary>
	/// <param name="value">
	/// The numeric value.
	/// </param>
	/// <returns>
	/// Returns true if the number is power of 2.
	/// </returns>
	public static bool IsPowerOfTwo(this int value)
	{
		return value > 0 && (value & (value - 1)) == 0;
	}

	/// <summary>
	/// Checks if the number is power of 2.
	/// </summary>
	/// <param name="value">
	/// The numeric value.
	/// </param>
	/// <returns>
	/// Returns true if the number is power of 2.
	/// </returns>
	public static bool IsPowerOfTwo(this long value)
	{
		return value > 0 && (value & (value - 1)) == 0;
	}

	/// <summary>
	/// Computes the interpolation between <paramref name="b" /> and <paramref name="c" /> using cubic polynomial
	/// equation.
	/// </summary>
	/// <param name="a">
	/// The first value coming before the staring range of interpolation.
	/// </param>
	/// <param name="b">
	/// The second value and the staring range of interpolation.
	/// </param>
	/// <param name="c">
	/// The third value and the ending range of interpolation.
	/// </param>
	/// <param name="d">
	/// The fourth value following after the ending range of interpolation.
	/// </param>
	/// <param name="fraction">
	/// The fraction of interpolation between <paramref name="b" /> and <paramref name="c" /> values.
	/// </param>
	/// <returns>
	/// Returns the interpolation between <paramref name="b" /> and <paramref name="c" />.
	/// </returns>
	public static float CubicLerp(float a, float b, float c, float d, float fraction)
	{
		var fraction2 = fraction * fraction;
		return (d - c + b - a) * fraction2 * fraction
			+ (2.0f * a - 2.0f * b + c - d) * fraction2
			+ (c - a) * fraction
			+ b;
	}

	/// <summary>
	/// Computes the interpolation between <paramref name="b" /> and <paramref name="c" /> using hermite polynomial
	/// equation.
	/// </summary>
	/// <param name="a">
	/// The first value coming before the staring range of interpolation.
	/// </param>
	/// <param name="b">
	/// The second value and the staring range of interpolation.
	/// </param>
	/// <param name="c">
	/// The third value and the ending range of interpolation.
	/// </param>
	/// <param name="d">
	/// The fourth value following after the ending range of interpolation.
	/// </param>
	/// <param name="fraction">
	/// The fraction of interpolation between <paramref name="b" /> and <paramref name="c" /> values.
	/// </param>
	/// <returns>
	/// Returns the interpolation between <paramref name="b" /> and <paramref name="c" />.
	/// </returns>
	public static float HermiteLerp(float a, float b, float c, float d, float fraction)
	{
		float c0 = 0.5f * (c - a);
		float c1 = a - (2.5f * b) + (2.0f * c) - (0.5f * d);
		float c2 = (0.5f * (d - a)) + (1.5f * (b - c));
		return (((((c2 * fraction) + c1) * fraction) + 0.5f * (c - a)) * fraction) + b;
	}
}
