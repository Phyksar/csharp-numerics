using System.Numerics;

namespace Phyksar.Numerics.Operations;

/// <summary>
/// Represents an interpolation between two quaternions on a timeline.
/// </summary>
public struct QuaternionLerp
{
	/// <summary>
	/// The first and the most recent value of interpolation.
	/// </summary>
	public Quaternion ValueA;

	/// <summary>
	/// The second and the oldest value of interpolation.
	/// </summary>
	public Quaternion ValueB;

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
	public QuaternionLerp(in Quaternion value, float time = 0.0f)
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
	public void Update(in Quaternion value, float time)
	{
		ValueB = ValueA;
		ValueA = value;
		UpdateTime = time;
	}

	/// <summary>
	/// Evaluates the interpolated value between two last updates.
	/// </summary>
	/// <param name="time">
	/// The current time to compare recent update with.
	/// </param>
	/// <param name="deltaTime">
	/// The time delta between two last updates.
	/// </param>
	/// <returns>
	/// The interpolated value.
	/// </returns>
	public Quaternion Evaluate(float time, float deltaTime)
	{
		return Quaternion.Slerp(ValueB, ValueA, (time - UpdateTime) / deltaTime);
	}
}
