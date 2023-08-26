namespace Phyksar.Numerics.Primitives;

using System;
using SystemVector2 = System.Numerics.Vector2;

/// <summary>
/// Represents an ellipse with center at zero coordinates.
/// </summary>
public struct Ellipse
{
	/// <summary>
	/// The radius of the ellipse in the X-axis.
	/// </summary>
	public float RadiusX;

	/// <summary>
	/// The radius of the ellipse in the Y-axis.
	/// </summary>
	public float RadiusY;

	/// <summary>
	/// Creates an ellipse with specified radiuses.
	/// </summary>
	/// <param name="radiusX">
	/// The radius of the ellipse in the X-axis.
	/// </param>
	/// <param name="radiusY">
	/// The radius of the ellipse in the Y-axis.
	/// </param>
	public Ellipse(float radiusX, float radiusY)
	{
		RadiusX = radiusX;
		RadiusY = radiusY;
	}

	/// <summary>
	/// Creates an ellipse with specified radiuses.
	/// </summary>
	/// <param name="radiuses">
	/// The vector that defines ellipse radiuses.
	/// </param>
	public Ellipse(in SystemVector2 radiuses)
	{
		RadiusX = radiuses.X;
		RadiusY = radiuses.Y;
	}

	/// <summary>
	/// Computes the squared distance from point to ellipse in the coordinate system scaled by ellipse radiuses.
	/// </summary>
	/// <returns>
	/// The squared projecton distance to point defined by <paramref name="pointX" /> and <paramref name="pointY" />.
	/// </returns>
	public float GetProjectionDistanceSquared(float pointX, float pointY)
	{
		if (RadiusX == 0.0f || RadiusY == 0.0f) {
			return float.PositiveInfinity;
		}
		return pointX * pointX / (RadiusX * RadiusX) + pointY * pointY / (RadiusY * RadiusY);
	}

	/// <summary>
	/// Computes the squared distance from point to ellipse in the coordinate system scaled by ellipse radiuses.
	/// </summary>
	/// <returns>
	/// The squared projecton distance to <paramref name="point" />.
	/// </returns>
	public float GetProjectionDistanceSquared(in SystemVector2 point)
	{
		return GetProjectionDistanceSquared(point.X, point.Y);
	}

	/// <summary>
	/// Computes distance from point to ellipse in the coordinate system scaled by ellipse radiuses.
	/// </summary>
	/// <returns>
	/// The projecton distance to point defined by <paramref name="pointX" /> and <paramref name="pointY" />.
	/// </returns>
	public float GetProjectionDistance(float pointX, float pointY)
	{
		return MathF.Sqrt(GetProjectionDistanceSquared(pointX, pointY));
	}

	/// <summary>
	/// Computes distance from point to ellipse in the coordinate system scaled by ellipse radiuses.
	/// </summary>
	/// <returns>
	/// The projecton distance to <paramref name="point" />.
	/// </returns>
	public float GetProjectionDistance(in SystemVector2 point)
	{
		return GetProjectionDistance(point.X, point.Y);
	}

	/// <summary>
	/// Checks if the point is outside of ellipse.
	/// </summary>
	/// <returns>
	/// Returns true if the point defined by <paramref name="pointX" /> and <paramref name="pointY" /> is outside.
	/// </returns>
	public bool IsPointOutside(float pointX, float pointY)
	{
		return GetProjectionDistanceSquared(pointX, pointY) > 1.0f;
	}

	/// <summary>
	/// Checks if the point is outside of ellipse.
	/// </summary>
	/// <returns>
	/// Returns true if the <paramref name="point" /> is outside.
	/// </returns>
	public bool IsPointOutside(in SystemVector2 point)
	{
		return IsPointOutside(point.X, point.Y);
	}
}
