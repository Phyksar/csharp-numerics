using System;

namespace Phyksar.Numerics.Algorithms;

/// <summary>
/// Defines the Fast Fourier transform algorithm.
/// </summary>
public struct FastFourierTransform
{
	/// <summary>
	/// Computes the transform for a set of complex numbers and modifies the specified numeric collections.
	/// </summary>
	/// <param name="reals">
	/// The real part of complex numbers.
	/// </param>
	/// <param name="imaginaries">
	/// The imaginary part of complex numbers.
	/// </param>
	public static void Compute(float[] reals, float[] imaginaries)
	{
		var sReals = new Span<float>(reals);
		var sImaginaries = new Span<float>(imaginaries);
		Compute(ref sReals, ref sImaginaries);
	}

	/// <summary>
	/// Computes the transform for a set of complex numbers and modifies the specified numeric collections.
	/// </summary>
	/// <param name="reals">
	/// The real part of complex numbers.
	/// </param>
	/// <param name="imaginaries">
	/// The imaginary part of complex numbers.
	/// </param>
	public static void Compute(ref Span<float> reals, ref Span<float> imaginaries)
	{
		var nm1 = reals.Length - 1;
		var nd2 = reals.Length / 2;
		var m = (int)MathF.Log2((float)reals.Length);
		var nd = nd2;
		float tr, ti;
		for (var i = 1; i < reals.Length - 1; i++) {
			if (i < nd) {
				tr = reals[nd];
				ti = imaginaries[nd];
				reals[nd] = reals[i];
				imaginaries[nd] = imaginaries[i];
				reals[i] = tr;
				imaginaries[i] = ti;
			}
			var k = nd2;
			while (k <= nd) {
				nd = nd - k;
				k /= 2;
			}
			nd = nd + k;
		}
		for (var i = 1; i <= m; i++) {
			var le = 1 << i;
			var le2 = le / 2;
			var ur = 1.0f;
			var ui = 0.0f;
			var sr = MathF.Cos(MathF.PI / le2);
			var si = MathF.Sin(MathF.PI / le2);
			for (var j = 1; j <= le2; ++j) {
				var jm1 = j - 1;
				for (var k = jm1; k <= nm1; k += le) {
					var ip = k + le2;
					tr = reals[ip] * ur - imaginaries[ip] * ui;
					ti = reals[ip] * ui + imaginaries[ip] * ur;
					reals[ip] = reals[k] - tr;
					imaginaries[ip] = imaginaries[k] - ti;
					reals[k] = reals[k] + tr;
					imaginaries[k] = imaginaries[k] + ti;
				}
				tr = ur;
				ur = tr * sr - ui * si;
				ui = tr * si + ui * sr;
			}
		}
	}

	/// <summary>
	/// Computes the inverse of transform for a set of complex numbers and modifies the specified numeric collections.
	/// </summary>
	/// <param name="reals">
	/// The real part of complex numbers.
	/// </param>
	/// <param name="imaginaries">
	/// The imaginary part of complex numbers.
	/// </param>
	public static void ComputeInverse(float[] reals, float[] imaginaries)
	{
		var sReals = new Span<float>(reals);
		var sImaginaries = new Span<float>(imaginaries);
		ComputeInverse(ref sReals, ref sImaginaries);
	}

	/// <summary>
	/// Computes the inverse of transform for a set of complex numbers and modifies the specified numeric collections.
	/// </summary>
	/// <param name="reals">
	/// The real part of complex numbers.
	/// </param>
	/// <param name="imaginaries">
	/// The imaginary part of complex numbers.
	/// </param>
	public static void ComputeInverse(ref Span<float> reals, ref Span<float> imaginaries)
	{
		for (var i = 0; i < reals.Length; i++) {
			imaginaries[i] = -imaginaries[i];
		}
		Compute(ref reals, ref imaginaries);
		var fraction = 1.0f / reals.Length;
		for (var i = 0; i < reals.Length; i++) {
			reals[i] = reals[i] * fraction;
			imaginaries[i] = -imaginaries[i] * fraction;
		}
	}
}
