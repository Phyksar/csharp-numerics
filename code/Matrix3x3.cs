using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using SystemVector3 = System.Numerics.Vector3;

namespace Phyksar.Numerics;

/// <summary>
/// Represents a 3x3 matrix.
/// </summary>
public struct Matrix3x3 : IEquatable<Matrix3x3>
{
	/// <summary>
	/// The first element of the first row.
	/// </summary>
	public float M11;

	/// <summary>
	/// The second element of the first row.
	/// </summary>
	public float M12;

	/// <summary>
	/// The third element of the first row.
	/// </summary>
	public float M13;

	/// <summary>
	/// The first element of the second row.
	/// </summary>
	public float M21;

	/// <summary>
	/// The second element of the second row.
	/// </summary>
	public float M22;

	/// <summary>
	/// The third element of the second row.
	/// </summary>
	public float M23;

	/// <summary>
	/// The first element of the third row.
	/// </summary>
	public float M31;

	/// <summary>
	/// The second element of the third row.
	/// </summary>
	public float M32;

	/// <summary>
	/// The third element of the third row.
	/// </summary>
	public float M33;

	/// <summary>
	/// Creates a 3x3 matrix from the specified components.
	/// </summary>
	/// <param name="m11">
	/// The value to assign to the first element in the first row.
	/// </param>
	/// <param name="m12">
	/// The value to assign to the second element in the first row.
	/// </param>
	/// <param name="m13">
	/// The value to assign to the third element in the first row.
	/// </param>
	/// <param name="m21">
	/// The value to assign to the first element in the second row.
	/// </param>
	/// <param name="m22">
	/// The value to assign to the second element in the second row.
	/// </param>
	/// <param name="m23">
	/// The value to assign to the third element in the second row.
	/// </param>
	/// <param name="m31">
	/// The value to assign to the first element in the third row.
	/// </param>
	/// <param name="m32">
	/// The value to assign to the second element in the third row.
	/// </param>
	/// <param name="m33">
	/// The value to assign to the third element in the third row.
	/// </param>
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

	/// <summary>
	/// Gets or sets the element at the specified indices.
	/// </summary>
	/// <param name="row">
	/// The index of the row containing the element to get or set.
	/// </param>
	/// <param name="column">
	/// The index of the column containing the element to get or set.
	/// </param>
	/// <returns>
	/// The element at [<paramref name="row" />][<paramref name="column" />].
	/// </returns>
	/// <exception cref="System.ArgumentOutOfRangeException" />
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
				throw new ArgumentOutOfRangeException(
					"row",
					row,
					"row was less than zero or greater than the number of rows."
				);
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
				throw new ArgumentOutOfRangeException(
					"column",
					column,
					"column was less than zero or greater than the number of columns."
				);
			}
		}
	}

	/// <summary>
	/// Indicates whether the current matrix is the identity matrix.
	/// </summary>
	/// <returns>
	/// True if the current matrix is the identity matrix; otherwise, false.
	/// </returns>
	public readonly bool IsIdentity => this.Equals(Identity);

	/// <summary>
	/// Calculates the determinant of the current 3x3 matrix.
	/// </summary>
	/// <returns>
	/// The determinant.
	/// </returns>
	public readonly float Determinant => M11 * (M22 * M33 - M23 * M32)
		- M12 * (M21 * M33 - M23 * M31)
		+ M13 * (M21 * M32 - M22 * M31);

	/// <summary>
	/// Gets or sets the X-axis vector of matrix.
	/// </summary>
	/// <returns>
	/// The X-axis vector.
	/// </returns>
	public SystemVector3 AxisX {
		readonly get => new SystemVector3(M11, M21, M31);
		set {
			M11 = value.X;
			M21 = value.Y;
			M31 = value.Z;
		}
	}

	/// <summary>
	/// Gets or sets the Y-axis vector of matrix.
	/// </summary>
	/// <returns>
	/// The Y-axis vector.
	/// </returns>
	public SystemVector3 AxisY {
		readonly get => new SystemVector3(M12, M22, M32);
		set {
			M12 = value.X;
			M22 = value.Y;
			M32 = value.Z;
		}
	}

	/// <summary>
	/// Gets or sets the Z-axis vector of matrix.
	/// </summary>
	/// <returns>
	/// The Z-axis vector.
	/// </returns>
	public SystemVector3 AxisZ {
		readonly get => new SystemVector3(M13, M23, M33);
		set {
			M13 = value.X;
			M23 = value.Y;
			M33 = value.Z;
		}
	}

	/// <summary>
	/// Gets the empty zero matrix.
	/// </summary>
	/// <returns>
	/// The zero matrix.
	/// </returns>
	public static Matrix3x3 Zero => new Matrix3x3 {
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

	/// <summary>
	/// Gets the multiplicative identity matrix.
	/// </summary>
	/// <returns>
	/// The identity matrix.
	/// </returns>
	public static Matrix3x3 Identity => new Matrix3x3 {
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

	/// <summary>
	/// Creates a rotation matrix from the specified Quaternion rotation value.
	/// </summary>
	/// <param name="quaternion">
	/// The source Quaternion.
	/// </param>
	/// <returns>
	/// The rotation matrix.
	/// </returns>
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

	/// <summary>
	/// Creates a matrix from the specified axis vectors.
	/// </summary>
	/// <param name="axisX">
	/// The X-axis vector of matrix.
	/// </param>
	/// <param name="axisY">
	/// The Y-axis vector of matrix.
	/// </param>
	/// <param name="axisZ">
	/// The Z-axis vector of matrix.
	/// </param>
	/// <returns>
	/// The matrix.
	/// </returns>
	public static Matrix3x3 CreateFromVectors(in Vector3 axisX, in Vector3 axisY, in Vector3 axisZ)
	{
		return new Matrix3x3 {
			M11 = axisX.x,
			M12 = axisY.x,
			M13 = axisZ.x,
			M21 = axisX.y,
			M22 = axisY.y,
			M23 = axisZ.y,
			M31 = axisX.z,
			M32 = axisY.z,
			M33 = axisZ.z
		};
	}

	/// <summary>
	/// Creates a skew-symmetric matrix from the specified X, Y and Z components.
	/// </summary>
	/// <param name="xSkew">
	/// The value to skew by on the X axis.
	/// </param>
	/// <param name="ySkew">
	/// The value to skew by on the Y axis.
	/// </param>
	/// <param name="zSkew">
	/// The value to skew by on the Z axis.
	/// </param>
	/// <returns>
	/// The skew-symmetric matrix.
	/// </returns>
	public static Matrix3x3 CreateSkewSymmetric(float xSkew, float ySkew, float zSkew)
	{
		return new Matrix3x3 {
			M11 = 0.0f,
			M12 = -zSkew,
			M13 = ySkew,
			M21 = zSkew,
			M22 = 0.0f,
			M23 = -xSkew,
			M31 = -ySkew,
			M32 = xSkew,
			M33 = 0.0f
		};
	}

	/// <summary>
	/// Creates a skew-symmetric matrix from the specified vector skew.
	/// </summary>
	/// <param name="skew">
	/// The vector that contains the amount to skew on each axis.
	/// </param>
	/// <returns>
	/// The skew-symmetric matrix.
	/// </returns>
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

	/// <summary>
	/// Creates a scaling matrix from the specified X, Y and Z components.
	/// </summary>
	/// <param name="xScale">
	/// The value to scale by on the X axis.
	/// </param>
	/// <param name="yScale">
	/// The value to scale by on the Y axis.
	/// </param>
	/// <param name="zScale">
	/// The value to scale by on the Y axis.
	/// </param>
	/// <returns>
	/// The scaling matrix.
	/// </returns>
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

	/// <summary>
	/// Creates a scaling matrix from the specified vector scale.
	/// </summary>
	/// <param name="scales">
	/// The vector that contains the amount to scale on each axis.
	/// </param>
	/// <returns>
	/// The scaling matrix.
	/// </returns>
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

	/// <summary>
	/// Creates a uniform scaling matrix that scales equally on each axis.
	/// </summary>
	/// <param name="scale">
	/// The uniform scaling factor.
	/// </param>
	/// <returns>
	/// The scaling matrix.
	/// </returns>
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

	/// <summary>
	/// Transposes the rows and columns of a matrix.
	/// </summary>
	/// <param name="matrix">
	/// The matrix to transpose.
	/// </param>
	/// <returns>
	/// The transposed matrix.
	/// </returns>
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

	/// <summary>
	/// Creates the adjugate of specified <paramref name="matrix" />.
	/// </summary>
	/// <param name="matrix">
	/// The matrix to adjugate
	/// </param>
	/// <returns>
	/// The adjugate of <paramref name="matrix" />
	/// </returns>
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

	/// <summary>
	/// Inverts the specified <paramref name="matrix" />. The return value indicates whether the operation succeeded.
	/// </summary>
	/// <param name="matrix">
	/// The matrix to invert.
	/// </param>
	/// <param name="result">
	/// When this method returns, contains the inverted <paramref name="matrix" /> if the operation succeeded.
	/// </param>
	/// <returns>
	/// True if <paramref name="matrix" /> was inverted successfully, false otherwise.
	/// </returns>
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

	/// <summary>
	/// Returns a value that indicates whether this instance and a specified object are equal.
	/// </summary>
	/// <param name="obj">
	/// The object to compare with the current instance.
	/// </param>
	/// <returns>
	/// True if the current instance and <paramref name="obj" /> are equal, false otherwise. If <paramref name="obj" />
	/// is null, the method returns false.
	/// </returns>
	public readonly override bool Equals([NotNullWhen(true)] object obj)
	{
		if (obj is not Matrix3x3 matrix) {
			return false;
		}
		return Equals(matrix);
	}

	/// <summary>
	/// Returns a value that indicates whether this instance and another 3x3 matrix are equal.
	/// </summary>
	/// <param name="other">
	/// The other matrix.
	/// </param>
	/// <returns>
	/// True if the two matrices are equal; false otherwise.
	/// </returns>
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

	/// <summary>
	/// Returns the hash code for this instance.
	/// </summary>
	/// <returns>
	/// The hash code.
	/// </returns>
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

	/// <summary>
	/// Returns a quaternion that represents the rotation component of this matrix.
	/// </summary>
	/// <returns>
	/// The quaternion representation of this matrix.
	/// </returns>
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

	/// <summary>
	/// Returns a string that represents this matrix.
	/// </summary>
	/// <returns>
	/// The string representation of this matrix.
	/// </returns>
	public readonly override string ToString()
	{
		return $"{{ {{M11:{M11} M12:{M12} M13:{M13}}} "
			+ $"{{M21:{M21} M22:{M22} M23:{M23}}} "
			+ $"{{M31:{M31} M32:{M32} M33:{M33}}} }}";
	}

	/// <summary>
	/// Adds each element in one matrix with its corresponding element in a second matrix.
	/// </summary>
	/// <param name="value1">
	/// The first matrix.
	/// </param>
	/// <param name="value2">
	/// The second matrix.
	/// </param>
	/// <returns>
	/// The matrix that contains the summed values.
	/// </returns>
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

	/// <summary>
	/// Negates the specified matrix by multiplying all its values by -1.
	/// </summary>
	/// <param name="value">
	/// The matrix to negate.
	/// </param>
	/// <returns>
	/// The negated matrix.
	/// </returns>
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

	/// <summary>
	/// Subtracts each element in a second matrix from its corresponding element in a first matrix.
	/// </summary>
	/// <param name="value1">
	/// The first matrix.
	/// </param>
	/// <param name="value2">
	/// The second matrix.
	/// </param>
	/// <returns>
	/// The matrix containing the values that result from subtracting each element in value2 from its corresponding
	/// element in value1.
	/// </returns>
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

	/// <summary>
	/// Returns the matrix that results from multiplying two matrices together.
	/// </summary>
	/// <param name="value1">
	/// The first matrix.
	/// </param>
	/// <param name="value2">
	/// The second matrix.
	/// </param>
	/// <returns>
	/// The product matrix.
	/// </returns>
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

	/// <summary>
	/// Returns the <paramref name="vector" /> transformed by <paramref name="matrix" />
	/// </summary>
	/// <param name="matrix">
	/// The transform matrix.
	/// </param>
	/// <param name="vector">
	/// The vector to transform.
	/// </param>
	/// <returns>
	/// The transformed vector.
	/// </returns>
	public static Vector3 operator *(in Matrix3x3 matrix, in Vector3 vector)
	{
		return new Vector3 {
			x = matrix.M11 * vector.x + matrix.M12 * vector.y + matrix.M13 * vector.z,
			y = matrix.M21 * vector.x + matrix.M22 * vector.y + matrix.M23 * vector.z,
			z = matrix.M31 * vector.x + matrix.M32 * vector.y + matrix.M33 * vector.z
		};
	}

	/// <summary>
	/// Returns the <paramref name="vector" /> transformed by transposed <paramref name="matrix" />
	/// </summary>
	/// <param name="vector">
	/// The vector to transform.
	/// </param>
	/// <param name="matrix">
	/// The transform matrix.
	/// </param>
	/// <returns>
	/// The transformed vector.
	/// </returns>
	public static Vector3 operator *(in Vector3 vector, in Matrix3x3 matrix)
	{
		return new Vector3 {
			x = matrix.M11 * vector.x + matrix.M21 * vector.y + matrix.M31 * vector.z,
			y = matrix.M12 * vector.x + matrix.M22 * vector.y + matrix.M32 * vector.z,
			z = matrix.M13 * vector.x + matrix.M23 * vector.y + matrix.M33 * vector.z
		};
	}

	/// <summary>
	/// Returns the matrix that results from scaling all the elements of a specified matrix by a
	/// <paramref name="scalar" />.
	/// </summary>
	/// <param name="matrix">
	/// The matrix to scale.
	/// </param>
	/// <param name="scalar">
	/// The scaling value to use.
	/// </param>
	/// <returns>
	/// The scaled matrix.
	/// </returns>
	public static Matrix3x3 operator *(Matrix3x3 matrix, float scalar)
	{
		return new Matrix3x3 {
			M11 = matrix.M11 * scalar,
			M12 = matrix.M12 * scalar,
			M13 = matrix.M13 * scalar,
			M21 = matrix.M21 * scalar,
			M22 = matrix.M22 * scalar,
			M23 = matrix.M23 * scalar,
			M31 = matrix.M31 * scalar,
			M32 = matrix.M32 * scalar,
			M33 = matrix.M33 * scalar
		};
	}

	/// <summary>
	/// Returns a value that indicates whether the specified matrices are equal.
	/// </summary>
	/// <param name="value1">
	/// The first matrix to compare.
	/// </param>
	/// <param name="value2">
	/// The second matrix to care.
	/// </param>
	/// <returns>
	/// True if <paramref name="value1" /> and <paramref name="value2" /> are equal, false otherwise.
	/// </returns>
	public static bool operator ==(Matrix3x3 value1, Matrix3x3 value2)
	{
		return value1.Equals(value2);
	}

	/// <summary>
	/// Returns a value that indicates whether the specified matrices are not equal.
	/// </summary>
	/// <param name="value1">
	/// The first matrix to compare.
	/// </param>
	/// <param name="value2">
	/// The second matrix to care.
	/// </param>
	/// <returns>
	/// True if <paramref name="value1" /> and <paramref name="value2" /> are not equal, false otherwise.
	/// </returns>
	public static bool operator !=(Matrix3x3 value1, Matrix3x3 value2)
	{
		return !value1.Equals(value2);
	}
}
