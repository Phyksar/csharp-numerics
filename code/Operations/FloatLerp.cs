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
	/// The most recent update time of interpolation.
	/// </summary>
	public float UpdateTime;

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
		UpdateTime = time;
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
		UpdateTime = time;
	}

	/// <summary>
	/// Evaluates the interpolated value between two last updates.
	/// </summary>
	/// <param name="time">
	/// The current time to compare with.
	/// </param>
	/// <param name="deltaTime">
	/// The time delta between two last updates.
	/// </param>
	/// <returns>
	/// The interpolated value.
	/// </returns>
	public float Evaluate(float time, float deltaTime)
	{
		var fraction = (time - UpdateTime) / deltaTime;
		return ValueA * fraction + ValueB * (1.0f - fraction);
	}
}
