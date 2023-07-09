namespace Phyksar.Numerics.Operations;

/// <summary>
/// Represents an interpolation between two floats on a timeline.
/// </summary>
public struct FloatLerp
{
	/// <summary>
	/// The first and the most recent value of interpolation.
	/// </summary>
	public float ValueA;

	/// <summary>
	/// The second and the oldest value of interpolation.
	/// </summary>
	public float ValueB;

	/// <summary>
	/// The first and the most recent update time of interpolation.
	/// </summary>
	public float TimeA;

	/// <summary>
	/// The second and the oldest update time of interpolation.
	/// </summary>
	public float TimeB;

	/// <summary>
	/// Create a float interpolation from value and time.
	/// </summary>
	/// <param name="value">
	/// The first and second value of interpolation.
	/// </param>
	/// <param name="time">
	/// The initial time of interpolation.
	/// </param>
	public FloatLerp(float value, float time = 0.0f)
	{
		ValueA = value;
		ValueB = value;
		TimeA = time;
		TimeB = time;
	}

	/// <summary>
	/// Update interpolation with the new recent value.
	/// </summary>
	/// <param name="value">
	/// The most recent value of interpolation.
	/// </param>
	/// <param name="time">
	/// The current time of interpolation.
	/// </param>
	public void Update(float value, float time)
	{
		ValueB = ValueA;
		ValueA = value;
		TimeB = TimeA;
		TimeA = time;
	}

	/// <summary>
	/// Evaluates the interpolated value between two last updates.
	/// </summary>
	/// <param name="time">
	/// The current time to compare with.
	/// </param>
	/// <returns>
	/// The interpolated value.
	/// </returns>
	public float Evaluate(float time)
	{
		if (TimeA == TimeB) {
			return ValueA;
		}
		var fraction = (time - TimeA) / (TimeA - TimeB);
		return ValueA * fraction + ValueB * (1.0f - fraction);
	}
}
