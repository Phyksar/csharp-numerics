using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using SystemVector3 = System.Numerics.Vector3;

namespace Phyksar.Numerics;

public struct Matrix3x3 : IEquatable<Matrix3x3>
{
	public float M11;
	public float M12;
	public float M13;
	public float M21;
	public float M22;
	public float M23;
	public float M31;
	public float M32;
	public float M33;

	public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
	{
		M11 = m11;
		M12 = m12;
		M13 = m13;
		M21 = m21;
		M22 = m22;
		M23 = m23;
		M31 = m31;
		M32 = m32;
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
				return M21;
			} else if (row == 1 && column == 1) {
				return M22;
			} else if (row == 1 && column == 2) {
				return M23;
			} else if (row == 2 && column == 0) {
				return M31;
			} else if (row == 2 && column == 1) {
				return M32;
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
				M21 = value;
			} else if (row == 1 && column == 1) {
				M22 = value;
			} else if (row == 1 && column == 2) {
				M23 = value;
			} else if (row == 2 && column == 0) {
				M31 = value;
			} else if (row == 2 && column == 1) {
				M32 = value;
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
		get => M11 * (M22 * M33 - M23 * M32) - M12 * (M21 * M33 - M23 * M31) + M13 * (M21 * M32 - M22 * M31);
	}

	public SystemVector3 VectorX {
		readonly get => new SystemVector3(M11, M21, M31);
		set {
			M11 = value.X;
			M21 = value.Y;
			M31 = value.Z;
		}
	}

	public SystemVector3 VectorY {
		readonly get => new SystemVector3(M12, M22, M32);
		set {
			M12 = value.X;
			M22 = value.Y;
			M32 = value.Z;
		}
	}

	public SystemVector3 VectorZ {
		readonly get => new SystemVector3(M13, M23, M33);
		set {
			M13 = value.X;
			M23 = value.Y;
			M33 = value.Z;
		}
	}

	public static Matrix3x3 Zero {
		get => new Matrix3x3 {
			M11 = 0.0f,
			M12 = 0.0f,
			M13 = 0.0f,
			M21 = 0.0f,
			M22 = 0.0f,
			M23 = 0.0f,
			M31 = 0.0f,
			M32 = 0.0f,
			M33 = 0.0f
		};
	}

	public static Matrix3x3 Identity {
		get => new Matrix3x3 {
			M11 = 1.0f,
			M12 = 0.0f,
			M13 = 0.0f,
			M21 = 0.0f,
			M22 = 1.0f,
			M23 = 0.0f,
			M31 = 0.0f,
			M32 = 0.0f,
			M33 = 1.0f
		};
	}

	public static Matrix3x3 CreateFromQuaternion(Quaternion quaternion)
	{
		return new Matrix3x3 {
			M11 = 1.0f - 2.0f * quaternion.Y * quaternion.Y - 2.0f * quaternion.Z * quaternion.Z,
			M12 = 2.0f * quaternion.X * quaternion.Y - 2.0f * quaternion.Z * quaternion.W,
			M13 = 2.0f * quaternion.X * quaternion.Z + 2.0f * quaternion.Y * quaternion.W,
			M21 = 2.0f * quaternion.X * quaternion.Y + 2.0f * quaternion.Z * quaternion.W,
			M22 = 1.0f - 2.0f * quaternion.X * quaternion.X - 2.0f * quaternion.Z * quaternion.Z,
			M23 = 2.0f * quaternion.Y * quaternion.Z - 2.0f * quaternion.X * quaternion.W,
			M31 = 2.0f * quaternion.X * quaternion.Z - 2.0f * quaternion.Y * quaternion.W,
			M32 = 2.0f * quaternion.Y * quaternion.Z + 2.0f * quaternion.X * quaternion.W,
			M33 = 1.0f - 2.0f * quaternion.X * quaternion.X - 2.0f * quaternion.Y * quaternion.Y
		};
	}

	public static Matrix3x3 CreateFromVectors(in Vector3 vectorX, in Vector3 vectorY, in Vector3 vectorZ)
	{
		return new Matrix3x3 {
			M11 = vectorX.x,
			M12 = vectorY.x,
			M13 = vectorZ.x,
			M21 = vectorX.y,
			M22 = vectorY.y,
			M23 = vectorZ.y,
			M31 = vectorX.z,
			M32 = vectorY.z,
			M33 = vectorZ.z
		};
	}

	public static Matrix3x3 CreateSkewSymmetric(in Vector3 skew)
	{
		return new Matrix3x3 {
			M11 = 0.0f,
			M12 = -skew.z,
			M13 = skew.y,
			M21 = skew.z,
			M22 = 0.0f,
			M23 = -skew.x,
			M31 = -skew.y,
			M32 = skew.x,
			M33 = 0.0f
		};
	}

	public static Matrix3x3 CreateScale(float xScale, float yScale, float zScale)
	{
		return new Matrix3x3 {
			M11 = xScale,
			M12 = 0.0f,
			M13 = 0.0f,
			M21 = 0.0f,
			M22 = yScale,
			M23 = 0.0f,
			M31 = 0.0f,
			M32 = 0.0f,
			M33 = zScale
		};
	}

	public static Matrix3x3 CreateScale(SystemVector3 scales)
	{
		return new Matrix3x3 {
			M11 = scales.X,
			M12 = 0.0f,
			M13 = 0.0f,
			M21 = 0.0f,
			M22 = scales.Y,
			M23 = 0.0f,
			M31 = 0.0f,
			M32 = 0.0f,
			M33 = scales.Z
		};
	}

	public static Matrix3x3 CreateScale(float scale)
	{
		return new Matrix3x3 {
			M11 = scale,
			M12 = 0.0f,
			M13 = 0.0f,
			M21 = 0.0f,
			M22 = scale,
			M23 = 0.0f,
			M31 = 0.0f,
			M32 = 0.0f,
			M33 = scale
		};
	}

	public static Matrix3x3 Transpose(Matrix3x3 matrix)
	{
		return new Matrix3x3 {
			M11 = matrix.M11,
			M12 = matrix.M21,
			M13 = matrix.M31,
			M21 = matrix.M12,
			M22 = matrix.M22,
			M23 = matrix.M32,
			M31 = matrix.M13,
			M32 = matrix.M23,
			M33 = matrix.M33
		};
	}

	public static Matrix3x3 Adjugate(Matrix3x3 matrix)
	{
		var t = Matrix3x3.Transpose(matrix);
		return new Matrix3x3 {
			M11 = t.M22 * t.M33 - t.M23 * t.M32,
			M12 = t.M23 * t.M31 - t.M21 * t.M33,
			M13 = t.M21 * t.M32 - t.M22 * t.M31,
			M21 = t.M13 * t.M32 - t.M12 * t.M33,
			M22 = t.M11 * t.M33 - t.M13 * t.M31,
			M23 = t.M12 * t.M31 - t.M11 * t.M32,
			M31 = t.M12 * t.M23 - t.M13 * t.M22,
			M32 = t.M13 * t.M21 - t.M11 * t.M23,
			M33 = t.M11 * t.M22 - t.M12 * t.M21
		};
	}

	public static bool Invert(Matrix3x3 matrix, out Matrix3x3 result)
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
		if (obj is not Matrix3x3 matrix) {
			return false;
		}
		return Equals(matrix);
	}

	public readonly bool Equals(Matrix3x3 other)
	{
		return M11 == other.M11
			&& M12 == other.M12
			&& M13 == other.M13
			&& M21 == other.M21
			&& M22 == other.M22
			&& M23 == other.M23
			&& M31 == other.M31
			&& M32 == other.M32
			&& M33 == other.M33;
	}

	public readonly override int GetHashCode()
	{
		var hash = new HashCode();
		hash.Add(M11);
		hash.Add(M12);
		hash.Add(M13);
		hash.Add(M21);
		hash.Add(M22);
		hash.Add(M23);
		hash.Add(M31);
		hash.Add(M32);
		hash.Add(M33);
		return hash.ToHashCode();
	}

	public readonly Quaternion ToQuaternion()
	{
		var trace = M11 + M22 + M33;
		if (trace > 0.0f) {
			var s = 2.0f * MathF.Sqrt(1.0f + trace);
			return new Quaternion((M32 - M23) / s, (M13 - M31) / s, (M21 - M12) / s, 0.25f * s);
		} else if (M11 > M22 && M11 > M33) {
			var s = 2.0f * MathF.Sqrt(1.0f + M11 - M22 - M33);
			return new Quaternion(0.25f * s, (M12 + M21) / s, (M13 + M31) / s, (M32 - M23) / s);
		} else if (M22 > M33) {
			var s = 2.0f * MathF.Sqrt(1.0f + M22 - M11 - M33);
			return new Quaternion((M12 + M21) / s, 0.25f * s, (M23 + M32) / s, (M13 - M31) / s);
		} else {
			var s = 2.0f * MathF.Sqrt(1.0f + M33 - M11 - M22);
			return new Quaternion((M13 + M31) / s, (M23 + M32) / s, 0.25f * s, (M21 - M12) / s);
		}
	}

	public readonly override string ToString()
	{
		return "{ {M11:" + M11.ToString() + " M12:" + M12.ToString() + " M13:" + M13.ToString()
			+ "} {M21:" + M21.ToString() + " M22:" + M22.ToString() + " M23:" + M23.ToString()
			+ "} {M31:" + M31.ToString() + " M32:" + M32.ToString() + " M33:" + M33.ToString()
			+ "} }";
	}

	public static Matrix3x3 operator +(Matrix3x3 value1, Matrix3x3 value2)
	{
		return new Matrix3x3 {
			M11 = value1.M11 + value2.M11,
			M12 = value1.M12 + value2.M12,
			M13 = value1.M13 + value2.M13,
			M21 = value1.M21 + value2.M21,
			M22 = value1.M22 + value2.M22,
			M23 = value1.M23 + value2.M23,
			M31 = value1.M31 + value2.M31,
			M32 = value1.M32 + value2.M32,
			M33 = value1.M33 + value2.M33
		};
	}

	public static Matrix3x3 operator -(Matrix3x3 value)
	{
		return new Matrix3x3 {
			M11 = -value.M11,
			M12 = -value.M12,
			M13 = -value.M13,
			M21 = -value.M21,
			M22 = -value.M22,
			M23 = -value.M23,
			M31 = -value.M31,
			M32 = -value.M32,
			M33 = -value.M33
		};
	}

	public static Matrix3x3 operator -(Matrix3x3 value1, Matrix3x3 value2)
	{
		return new Matrix3x3 {
			M11 = value1.M11 - value2.M11,
			M12 = value1.M12 - value2.M12,
			M13 = value1.M13 - value2.M13,
			M21 = value1.M21 - value2.M21,
			M22 = value1.M22 - value2.M22,
			M23 = value1.M23 - value2.M23,
			M31 = value1.M31 - value2.M31,
			M32 = value1.M32 - value2.M32,
			M33 = value1.M33 - value2.M33
		};
	}

	public static Matrix3x3 operator *(Matrix3x3 value1, Matrix3x3 value2)
	{
		return new Matrix3x3 {
			M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31,
			M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32,
			M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33,
			M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31,
			M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32,
			M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33,
			M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31,
			M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32,
			M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33
		};
	}

	public static Vector3 operator *(in Matrix3x3 value1, in Vector3 value2)
	{
		return new Vector3 {
			x = value1.M11 * value2.x + value1.M12 * value2.y + value1.M13 * value2.z,
			y = value1.M21 * value2.x + value1.M22 * value2.y + value1.M23 * value2.z,
			z = value1.M31 * value2.x + value1.M32 * value2.y + value1.M33 * value2.z
		};
	}

	public static Vector3 operator *(in Vector3 value1, in Matrix3x3 value2)
	{
		return new Vector3 {
			x = value2.M11 * value1.x + value2.M21 * value1.y + value2.M31 * value1.z,
			y = value2.M12 * value1.x + value2.M22 * value1.y + value2.M32 * value1.z,
			z = value2.M13 * value1.x + value2.M23 * value1.y + value2.M33 * value1.z
		};
	}

	public static Matrix3x3 operator *(Matrix3x3 value1, float value2)
	{
		return new Matrix3x3 {
			M11 = value1.M11 * value2,
			M12 = value1.M12 * value2,
			M13 = value1.M13 * value2,
			M21 = value1.M21 * value2,
			M22 = value1.M22 * value2,
			M23 = value1.M23 * value2,
			M31 = value1.M31 * value2,
			M32 = value1.M32 * value2,
			M33 = value1.M33 * value2
		};
	}

	public static bool operator ==(Matrix3x3 value1, Matrix3x3 value2)
	{
		return value1.Equals(value2);
	}

	public static bool operator !=(Matrix3x3 value1, Matrix3x3 value2)
	{
		return !value1.Equals(value2);
	}
}
