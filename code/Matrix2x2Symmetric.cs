using System;
using System.Diagnostics.CodeAnalysis;
using SystemVector2 = System.Numerics.Vector2;

namespace Phyksar.Numerics;

public struct Matrix2x2Symmetric : IEquatable<Matrix2x2Symmetric>
{
	public float M11;
	public float M12;
	public float M22;

	public Matrix2x2Symmetric(float m11, float m12, float m22)
	{
		M11 = m11;
		M12 = m12;
		M22 = m22;
	}

	public float this[int row, int column] {
		get {
			if (row == 0 && column == 0) {
				return M11;
			} else if (row == 0 && column == 1) {
				return M12;
			} else if (row == 1 && column == 0) {
				return M12;
			} else if (row == 1 && column == 1) {
				return M22;
			} else {
				throw new IndexOutOfRangeException();
			}
		}
		set {
			if (row == 0 && column == 0) {
				M11 = value;
			} else if (row == 0 && column == 1) {
				M12 = value;
			} else if (row == 1 && column == 0) {
				M12 = value;
			} else if (row == 1 && column == 1) {
				M22 = value;
			} else {
				throw new IndexOutOfRangeException();
			}
		}
	}

	public readonly bool IsIdentity {
		get => this.Equals(Identity);
	}

	public readonly float Determinant {
		get => M11 * M22 - M12 * M12;
	}

	public static Matrix2x2Symmetric Zero {
		get => new Matrix2x2Symmetric {
			M11 = 0.0f,
			M12 = 0.0f,
			M22 = 0.0f
		};
	}

	public static Matrix2x2Symmetric Identity {
		get => new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = 0.0f,
			M22 = 1.0f
		};
	}

	public static Matrix2x2Symmetric CreateScale(float xScale, float yScale)
	{
		return new Matrix2x2Symmetric {
			M11 = xScale,
			M12 = 0.0f,
			M22 = yScale
		};
	}

	public static Matrix2x2Symmetric CreateScale(SystemVector2 scales)
	{
		return new Matrix2x2Symmetric {
			M11 = scales.X,
			M12 = 0.0f,
			M22 = scales.Y
		};
	}

	public static Matrix2x2Symmetric CreateScale(float scale)
	{
		return new Matrix2x2Symmetric {
			M11 = scale,
			M12 = 0.0f,
			M22 = scale
		};
	}

	public static Matrix2x2Symmetric Adjugate(Matrix2x2Symmetric matrix)
	{
		return new Matrix2x2Symmetric {
			M11 = matrix.M22,
			M12 = -matrix.M12,
			M22 = matrix.M11
		};
	}

	public static bool Invert(Matrix2x2Symmetric matrix, out Matrix2x2Symmetric result)
	{
		var determinant = matrix.Determinant;
		if (determinant == 0.0f) {
			result = Identity;
			return false;
		}
		result = Adjugate(matrix) * (1.0f / determinant);
		return true;
	}

	public readonly override bool Equals([NotNullWhen(true)] object obj)
	{
		if (obj is not Matrix2x2Symmetric matrix) {
			return false;
		}
		return Equals(matrix);
	}

	public readonly bool Equals(Matrix2x2Symmetric other)
	{
		return M11 == other.M11 && M12 == other.M12 && M22 == other.M22;
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(M11, M12, M22);
	}

	public readonly override string ToString()
	{
		return "{ {M11:" + M11.ToString() + " M12:" + M12.ToString()
			+ "} {M21:" + M12.ToString() + " M22:" + M22.ToString()
			+ "} }";
	}

	public static Matrix2x2Symmetric operator +(Matrix2x2Symmetric value1, Matrix2x2Symmetric value2)
	{
		return new Matrix2x2Symmetric {
			M11 = value1.M11 + value2.M11,
			M12 = value1.M12 + value2.M12,
			M22 = value1.M22 + value2.M22
		};
	}

	public static Matrix2x2Symmetric operator -(Matrix2x2Symmetric value)
	{
		return new Matrix2x2Symmetric {
			M11 = -value.M11,
			M12 = -value.M12,
			M22 = -value.M22
		};
	}

	public static Matrix2x2Symmetric operator -(Matrix2x2Symmetric value1, Matrix2x2Symmetric value2)
	{
		return new Matrix2x2Symmetric {
			M11 = value1.M11 - value2.M11,
			M12 = value1.M12 - value2.M12,
			M22 = value1.M22 - value2.M22
		};
	}

	public static Matrix2x2Symmetric operator *(Matrix2x2Symmetric value1, Matrix2x2Symmetric value2)
	{
		return new Matrix2x2Symmetric {
			M11 = value1.M11 * value2.M11 + value1.M12 * value2.M12,
			M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22,
			M22 = value1.M12 * value2.M12 + value1.M22 * value2.M22
		};
	}

	public static Vector3 operator *(in Matrix2x2Symmetric value1, in Vector3 value2)
	{
		return new Vector3 {
			x = value1.M11 * value2.x + value1.M12 * value2.y,
			y = value1.M12 * value2.x + value1.M22 * value2.y,
		};
	}

	public static Vector3 operator *(in Vector3 value1, in Matrix2x2Symmetric value2)
	{
		return new Vector3 {
			x = value2.M11 * value1.x + value2.M12 * value1.y,
			y = value2.M12 * value1.x + value2.M22 * value1.y
		};
	}

	public static Matrix2x2Symmetric operator *(Matrix2x2Symmetric value1, float value2)
	{
		return new Matrix2x2Symmetric {
			M11 = value1.M11 * value2,
			M12 = value1.M12 * value2,
			M22 = value1.M22 * value2
		};
	}

	public static bool operator ==(Matrix2x2Symmetric value1, Matrix2x2Symmetric value2)
	{
		return value1.Equals(value2);
	}

	public static bool operator !=(Matrix2x2Symmetric value1, Matrix2x2Symmetric value2)
	{
		return !value1.Equals(value2);
	}

	public static implicit operator Matrix2x2(Matrix2x2Symmetric matrix) => new Matrix2x2 {
		M11 = matrix.M11,
		M12 = matrix.M12,
		M21 = matrix.M12,
		M22 = matrix.M22
	};

	public static explicit operator Matrix2x2Symmetric(Matrix2x2 matrix) => new Matrix2x2Symmetric {
		M11 = matrix.M11,
		M12 = matrix.M12,
		M22 = matrix.M22
	};
}
