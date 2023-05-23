namespace Phyksar.Numerics.Operations;

public struct FloatLerp
{
	public float CurrentValue;
	public float LastValue;
	public float LastUpdateTime;

	public FloatLerp(float value, float time = 0.0f)
	{
		CurrentValue = value;
		LastValue = value;
		LastUpdateTime = time;
	}

	public void Update(float value, float time)
	{
		LastValue = CurrentValue;
		CurrentValue = value;
		LastUpdateTime = time;
	}

	public float Evaluate(float time, float deltaTime)
	{
		var fraction = (time - LastUpdateTime) / deltaTime;
		return CurrentValue * fraction + LastValue * (1.0f - fraction);
	}
}
