using System;
using System.Diagnostics.CodeAnalysis;
using SystemVector3 = System.Numerics.Vector3;

namespace Phyksar.Numerics;

public struct Matrix3x3Symmetric : IEquatable<Matrix3x3Symmetric>
{
	public float M11;
	public float M12;
	public float M13;
	public float M22;
	public float M23;
	public float M33;

	public Matrix3x3Symmetric(float m11, float m12, float m13, float m22, float m23, float m33)
	{
		M11 = m11;
		M12 = m12;
		M13 = m13;
		M22 = m22;
		M23 = m23;
		M33 = m33;
	}

	public float this[int row, int column] {
		get {
			if (row == 0 && column == 0) {
				return M11;
			} else if (row == 0 && column == 1) {
				return M12;
			} else if (row == 0 && column == 2) {
				return M13;
			} else if (row == 1 && column == 0) {
				return M12;
			} else if (row == 1 && column == 1) {
				return M22;
			} else if (row == 1 && column == 2) {
				return M23;
			} else if (row == 2 && column == 0) {
				return M13;
			} else if (row == 2 && column == 1) {
				return M23;
			} else if (row == 2 && column == 2) {
				return M33;
			} else {
				throw new IndexOutOfRangeException();
			}
		}
		set {
			if (row == 0 && column == 0) {
				M11 = value;
			} else if (row == 0 && column == 1) {
				M12 = value;
			} else if (row == 0 && column == 2) {
				M13 = value;
			} else if (row == 1 && column == 0) {
				M12 = value;
			} else if (row == 1 && column == 1) {
				M22 = value;
			} else if (row == 1 && column == 2) {
				M23 = value;
			} else if (row == 2 && column == 0) {
				M13 = value;
			} else if (row == 2 && column == 1) {
				M23 = value;
			} else if (row == 2 && column == 2) {
				M33 = value;
			} else {
				throw new IndexOutOfRangeException();
			}
		}
	}

	public readonly bool IsIdentity {
		get => this.Equals(Identity);
	}

	public readonly float Determinant {
		get => M11 * (M22 * M33 - M23 * M23) - M12 * (M12 * M33 - M23 * M13) + M13 * (M12 * M23 - M22 * M13);
	}

	public static Matrix3x3Symmetric Zero {
		get => new Matrix3x3Symmetric {
			M11 = 0.0f,
			M12 = 0.0f,
			M13 = 0.0f,
			M22 = 0.0f,
			M23 = 0.0f,
			M33 = 0.0f
		};
	}

	public static Matrix3x3Symmetric Identity {
		get => new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = 0.0f,
			M13 = 0.0f,
			M22 = 1.0f,
			M23 = 0.0f,
			M33 = 1.0f
		};
	}

	public static Matrix3x3Symmetric CreateScale(float xScale, float yScale, float zScale)
	{
		return new Matrix3x3Symmetric {
			M11 = xScale,
			M12 = 0.0f,
			M13 = 0.0f,
			M22 = yScale,
			M23 = 0.0f,
			M33 = zScale
		};
	}

	public static Matrix3x3Symmetric CreateScale(SystemVector3 scales)
	{
		return new Matrix3x3Symmetric {
			M11 = scales.X,
			M12 = 0.0f,
			M13 = 0.0f,
			M22 = scales.Y,
			M23 = 0.0f,
			M33 = scales.Z
		};
	}

	public static Matrix3x3Symmetric CreateScale(float scale)
	{
		return new Matrix3x3Symmetric {
			M11 = scale,
			M12 = 0.0f,
			M13 = 0.0f,
			M22 = scale,
			M23 = 0.0f,
			M33 = scale
		};
	}

	public static Matrix3x3Symmetric Adjugate(Matrix3x3Symmetric matrix)
	{
		return new Matrix3x3Symmetric {
			M11 = matrix.M22 * matrix.M33 - matrix.M23 * matrix.M23,
			M12 = matrix.M23 * matrix.M13 - matrix.M12 * matrix.M33,
			M13 = matrix.M12 * matrix.M23 - matrix.M22 * matrix.M13,
			M22 = matrix.M11 * matrix.M33 - matrix.M13 * matrix.M13,
			M23 = matrix.M12 * matrix.M13 - matrix.M11 * matrix.M23,
			M33 = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M12
		};
	}

	public static bool Invert(Matrix3x3Symmetric matrix, out Matrix3x3Symmetric result)
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
		if (obj is not Matrix3x3Symmetric matrix) {
			return false;
		}
		return Equals(matrix);
	}

	public readonly bool Equals(Matrix3x3Symmetric other)
	{
		return M11 == other.M11
			&& M12 == other.M12
			&& M13 == other.M13
			&& M22 == other.M22
			&& M23 == other.M23
			&& M33 == other.M33;
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(M11, M12, M13, M22, M23, M33);
	}

	public readonly override string ToString()
	{
		return "{ {M11:" + M11.ToString() + " M12:" + M12.ToString() + " M13:" + M13.ToString()
			+ "} {M21:" + M12.ToString() + " M22:" + M22.ToString() + " M23:" + M23.ToString()
			+ "} {M31:" + M13.ToString() + " M32:" + M23.ToString() + " M33:" + M33.ToString()
			+ "} }";
	}

	public static Matrix3x3Symmetric operator +(Matrix3x3Symmetric value1, Matrix3x3Symmetric value2)
	{
		return new Matrix3x3Symmetric {
			M11 = value1.M11 + value2.M11,
			M12 = value1.M12 + value2.M12,
			M13 = value1.M13 + value2.M13,
			M22 = value1.M22 + value2.M22,
			M23 = value1.M23 + value2.M23,
			M33 = value1.M33 + value2.M33
		};
	}

	public static Matrix3x3Symmetric operator -(Matrix3x3Symmetric value)
	{
		return new Matrix3x3Symmetric {
			M11 = -value.M11,
			M12 = -value.M12,
			M13 = -value.M13,
			M22 = -value.M22,
			M23 = -value.M23,
			M33 = -value.M33
		};
	}

	public static Matrix3x3Symmetric operator -(Matrix3x3Symmetric value1, Matrix3x3Symmetric value2)
	{
		return new Matrix3x3Symmetric {
			M11 = value1.M11 - value2.M11,
			M12 = value1.M12 - value2.M12,
			M13 = value1.M13 - value2.M13,
			M22 = value1.M22 - value2.M22,
			M23 = value1.M23 - value2.M23,
			M33 = value1.M33 - value2.M33
		};
	}

	public static Matrix3x3Symmetric operator *(Matrix3x3Symmetric value1, Matrix3x3Symmetric value2)
	{
		return new Matrix3x3Symmetric {
			M11 = value1.M11 * value2.M11 + value1.M12 * value2.M12 + value1.M13 * value2.M13,
			M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M23,
			M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33,
			M22 = value1.M12 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M23,
			M23 = value1.M12 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33,
			M33 = value1.M13 * value2.M13 + value1.M23 * value2.M23 + value1.M33 * value2.M33
		};
	}

	public static Vector3 operator *(in Matrix3x3Symmetric value1, in Vector3 value2)
	{
		return new Vector3 {
			x = value1.M11 * value2.x + value1.M12 * value2.y + value1.M13 * value2.z,
			y = value1.M12 * value2.x + value1.M22 * value2.y + value1.M23 * value2.z,
			z = value1.M13 * value2.x + value1.M23 * value2.y + value1.M33 * value2.z
		};
	}

	public static Vector3 operator *(in Vector3 value1, in Matrix3x3Symmetric value2)
	{
		return new Vector3 {
			x = value2.M11 * value1.x + value2.M12 * value1.y + value2.M13 * value1.z,
			y = value2.M12 * value1.x + value2.M22 * value1.y + value2.M23 * value1.z,
			z = value2.M13 * value1.x + value2.M23 * value1.y + value2.M33 * value1.z
		};
	}

	public static Matrix3x3Symmetric operator *(Matrix3x3Symmetric value1, float value2)
	{
		return new Matrix3x3Symmetric {
			M11 = value1.M11 * value2,
			M12 = value1.M12 * value2,
			M13 = value1.M13 * value2,
			M22 = value1.M22 * value2,
			M23 = value1.M23 * value2,
			M33 = value1.M33 * value2
		};
	}

	public static bool operator ==(Matrix3x3Symmetric value1, Matrix3x3Symmetric value2)
	{
		return value1.Equals(value2);
	}

	public static bool operator !=(Matrix3x3Symmetric value1, Matrix3x3Symmetric value2)
	{
		return !value1.Equals(value2);
	}

	public static implicit operator Matrix3x3(Matrix3x3Symmetric matrix) => new Matrix3x3 {
		M11 = matrix.M11,
		M12 = matrix.M12,
		M13 = matrix.M13,
		M21 = matrix.M12,
		M22 = matrix.M22,
		M23 = matrix.M23,
		M31 = matrix.M13,
		M32 = matrix.M23,
		M33 = matrix.M33
	};

	public static explicit operator Matrix3x3Symmetric(Matrix3x3 matrix) => new Matrix3x3Symmetric {
		M11 = matrix.M11,
		M12 = matrix.M12,
		M13 = matrix.M13,
		M22 = matrix.M22,
		M23 = matrix.M23,
		M33 = matrix.M33
	};
}
