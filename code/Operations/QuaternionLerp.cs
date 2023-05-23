using System.Numerics;

namespace Phyksar.Numerics.Operations;

public struct QuaternionLerp
{
	public Quaternion CurrentValue;
	public Quaternion LastValue;
	public float LastUpdateTime;

	public QuaternionLerp(Quaternion value, float time = 0.0f)
	{
		CurrentValue = value;
		LastValue = value;
		LastUpdateTime = time;
	}

	public void Update(Quaternion value, float time)
	{
		LastValue = CurrentValue;
		CurrentValue = value;
		LastUpdateTime = time;
	}

	public Quaternion Evaluate(float time, float deltaTime)
	{
		return Quaternion.Slerp(LastValue, CurrentValue, (time - LastUpdateTime) / deltaTime);
	}
}
