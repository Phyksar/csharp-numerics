using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using SystemVector4 = System.Numerics.Vector4;

namespace Phyksar.Numerics;

/// <summary>
/// Represents a 4x4 matrix symmetric about its diagonal.
/// </summary>
public struct Matrix4x4Symmetric : IEquatable<Matrix4x4Symmetric>
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
	/// The fourth element of the first row.
	/// </summary>
	public float M14;

	/// <summary>
	/// The second element of the second row.
	/// </summary>
	public float M22;

	/// <summary>
	/// The third element of the second row.
	/// </summary>
	public float M23;

	/// <summary>
	/// The fourth element of the second row.
	/// </summary>
	public float M24;

	/// <summary>
	/// The third element of the third row.
	/// </summary>
	public float M33;

	/// <summary>
	/// The fourth element of the third row.
	/// </summary>
	public float M34;

	/// <summary>
	/// The fourth element of the fourth row.
	/// </summary>
	public float M44;

	/// <summary>
	/// Creates a 4x4 matrix from the specified components.
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
	/// <param name="m14">
	/// The value to assign to the fourth element in the first row.
	/// </param>
	/// <param name="m22">
	/// The value to assign to the second element in the second row.
	/// </param>
	/// <param name="m23">
	/// The value to assign to the third element in the second row.
	/// </param>
	/// <param name="m24">
	/// The value to assign to the fourth element in the second row.
	/// </param>
	/// <param name="m33">
	/// The value to assign to the third element in the third row.
	/// </param>
	/// <param name="m34">
	/// The value to assign to the fourth element in the third row.
	/// </param>
	/// <param name="m44">
	/// The value to assign to the fourth element in the fourth row.
	/// </param>
	public Matrix4x4Symmetric(
		float m11, float m12, float m13, float m14,
		float m22, float m23, float m24,
		float m33, float m34,
		float m44)
	{
		M11 = m11; M12 = m12; M13 = m13; M14 = m14;
		M22 = m22; M23 = m23; M24 = m24;
		M33 = m33; M34 = m34;
		M44 = m44;
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
			} else if ((row == 0 && column == 1) || (row == 1 && column == 0)) {
				return M12;
			} else if ((row == 0 && column == 2) || (row == 2 && column == 0)) {
				return M13;
			} else if ((row == 0 && column == 3) || (row == 3 && column == 0)) {
				return M14;
			} else if (row == 1 && column == 1) {
				return M22;
			} else if ((row == 1 && column == 2) || (row == 2 && column == 1)) {
				return M23;
			} else if ((row == 1 && column == 3) || (row == 3 && column == 1)) {
				return M24;
			} else if (row == 2 && column == 2) {
				return M33;
			} else if ((row == 2 && column == 3) || (row == 3 && column == 2)) {
				return M34;
			} else if (row == 3 && column == 3) {
				return M44;
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
			} else if ((row == 0 && column == 1) || (row == 1 && column == 0)) {
				M12 = value;
			} else if ((row == 0 && column == 2) || (row == 2 && column == 0)) {
				M13 = value;
			} else if ((row == 0 && column == 3) || (row == 3 && column == 0)) {
				M14 = value;
			} else if (row == 1 && column == 1) {
				M22 = value;
			} else if ((row == 1 && column == 2) || (row == 2 && column == 1)) {
				M23 = value;
			} else if ((row == 1 && column == 3) || (row == 3 && column == 1)) {
				M24 = value;
			} else if (row == 2 && column == 2) {
				M33 = value;
			} else if ((row == 2 && column == 3) || (row == 3 && column == 2)) {
				M34 = value;
			} else if (row == 3 && column == 3) {
				M44 = value;
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
	/// Calculates the determinant of the current 4x4 matrix.
	/// </summary>
	/// <returns>
	/// The determinant.
	/// </returns>
	public readonly float Determinant => M14 * (M14 * M23 * M23 - M13 * M24 * M23 - M14 * M22 * M33 + M12 * M24 * M33 + M13 * M22 * M34 - M12 * M23 * M34)
		- M24 * (M14 * M23 * M13 - M13 * M24 * M13 - M14 * M12 * M33 + M11 * M24 * M33 + M13 * M12 * M34 - M11 * M23 * M34)
		+ M34 * (M14 * M22 * M13 - M12 * M24 * M13 - M14 * M12 * M23 + M11 * M24 * M23 + M12 * M12 * M34 - M11 * M22 * M34)
		- M44 * (M13 * M22 * M13 - M12 * M23 * M13 - M13 * M12 * M23 + M11 * M23 * M23 + M12 * M12 * M33 - M11 * M22 * M33);

	/// <summary>
	/// Gets the empty zero matrix.
	/// </summary>
	/// <returns>
	/// The zero matrix.
	/// </returns>
	public static Matrix4x4Symmetric Zero => new Matrix4x4Symmetric {
		M11 = 0.0f,
		M12 = 0.0f,
		M13 = 0.0f,
		M14 = 0.0f,
		M22 = 0.0f,
		M23 = 0.0f,
		M24 = 0.0f,
		M33 = 0.0f,
		M34 = 0.0f,
		M44 = 0.0f
	};

	/// <summary>
	/// Gets the multiplicative identity matrix.
	/// </summary>
	/// <returns>
	/// The identity matrix.
	/// </returns>
	public static Matrix4x4Symmetric Identity => new Matrix4x4Symmetric {
		M11 = 1.0f,
		M12 = 0.0f,
		M13 = 0.0f,
		M14 = 0.0f,
		M22 = 1.0f,
		M23 = 0.0f,
		M24 = 0.0f,
		M33 = 1.0f,
		M34 = 0.0f,
		M44 = 1.0f
	};

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
	/// The value to scale by on the Z axis.
	/// </param>
	/// <param name="wScale">
	/// The value to scale by on the W axis.
	/// </param>
	/// <returns>
	/// The scaling matrix.
	/// </returns>
	public static Matrix4x4Symmetric CreateScale(float xScale, float yScale, float zScale, float wScale)
	{
		return new Matrix4x4Symmetric {
			M11 = xScale,
			M12 = 0.0f,
			M13 = 0.0f,
			M14 = 0.0f,
			M22 = yScale,
			M23 = 0.0f,
			M24 = 0.0f,
			M33 = zScale,
			M34 = 0.0f,
			M44 = wScale
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
	public static Matrix4x4Symmetric CreateScale(SystemVector4 scales)
	{
		return new Matrix4x4Symmetric {
			M11 = scales.X,
			M12 = 0.0f,
			M13 = 0.0f,
			M14 = 0.0f,
			M22 = scales.Y,
			M23 = 0.0f,
			M24 = 0.0f,
			M33 = scales.Z,
			M34 = 0.0f,
			M44 = scales.W
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
	public static Matrix4x4Symmetric CreateScale(float scale)
	{
		return new Matrix4x4Symmetric {
			M11 = scale,
			M12 = 0.0f,
			M13 = 0.0f,
			M14 = 0.0f,
			M22 = scale,
			M23 = 0.0f,
			M24 = 0.0f,
			M33 = scale,
			M34 = 0.0f,
			M44 = scale
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
	public static Matrix4x4Symmetric Adjugate(Matrix4x4Symmetric matrix)
	{
		var A3434 = matrix.M33 * matrix.M44 - matrix.M34 * matrix.M34;
		var A2434 = matrix.M23 * matrix.M44 - matrix.M24 * matrix.M34;
		var A2334 = matrix.M23 * matrix.M34 - matrix.M24 * matrix.M33;
		var A1434 = matrix.M13 * matrix.M44 - matrix.M14 * matrix.M34;
		var A1334 = matrix.M13 * matrix.M34 - matrix.M14 * matrix.M33;
		var A1234 = matrix.M13 * matrix.M24 - matrix.M14 * matrix.M23;
		var A2424 = matrix.M22 * matrix.M44 - matrix.M24 * matrix.M24;
		var A2324 = matrix.M22 * matrix.M34 - matrix.M23 * matrix.M24;
		var A2323 = matrix.M22 * matrix.M33 - matrix.M23 * matrix.M23;
		var A1424 = matrix.M12 * matrix.M44 - matrix.M14 * matrix.M24;
		var A1324 = matrix.M12 * matrix.M34 - matrix.M14 * matrix.M23;
		var A1423 = matrix.M12 * matrix.M34 - matrix.M13 * matrix.M24;
		var A1323 = matrix.M12 * matrix.M33 - matrix.M13 * matrix.M23;
		var A1224 = matrix.M12 * matrix.M24 - matrix.M14 * matrix.M22;
		var A1223 = matrix.M12 * matrix.M23 - matrix.M13 * matrix.M22;
		return new Matrix4x4Symmetric {
			M11 = matrix.M22 * A3434 - matrix.M23 * A2434 + matrix.M24 * A2334,
			M12 = -matrix.M12 * A3434 + matrix.M13 * A2434 - matrix.M14 * A2334,
			M13 = matrix.M12 * A2434 - matrix.M13 * A2424 + matrix.M14 * A2324,
			M14 = -matrix.M12 * A2334 + matrix.M13 * A2324 - matrix.M14 * A2323,
			M22 = matrix.M11 * A3434 - matrix.M13 * A1434 + matrix.M14 * A1334,
			M23 = -matrix.M11 * A2434 + matrix.M13 * A1424 - matrix.M14 * A1324,
			M24 = matrix.M11 * A2334 - matrix.M13 * A1423 + matrix.M14 * A1323,
			M33 = matrix.M11 * A2424 - matrix.M12 * A1424 + matrix.M14 * A1224,
			M34 = -matrix.M11 * A2324 + matrix.M12 * A1423 - matrix.M14 * A1223,
			M44 = matrix.M11 * A2323 - matrix.M12 * A1323 + matrix.M13 * A1223
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
	public static bool Invert(Matrix4x4Symmetric matrix, out Matrix4x4Symmetric result)
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
		if (obj is not Matrix4x4Symmetric matrix) {
			return false;
		}
		return Equals(matrix);
	}

	/// <summary>
	/// Returns a value that indicates whether this instance and another 4x4 matrix are equal.
	/// </summary>
	/// <param name="other">
	/// The other matrix.
	/// </param>
	/// <returns>
	/// True if the two matrices are equal; false otherwise.
	/// </returns>
	public readonly bool Equals(Matrix4x4Symmetric other)
	{
		return M11 == other.M11 && M12 == other.M12 && M13 == other.M13 && M14 == other.M14
			&& M22 == other.M22 && M23 == other.M23 && M24 == other.M24
			&& M33 == other.M33 && M34 == other.M34
			&& M44 == other.M44;
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
		hash.Add(M14);
		hash.Add(M22);
		hash.Add(M23);
		hash.Add(M24);
		hash.Add(M33);
		hash.Add(M34);
		hash.Add(M44);
		return hash.ToHashCode();
	}

	/// <summary>
	/// Returns a string that represents this matrix.
	/// </summary>
	/// <returns>
	/// The string representation of this matrix.
	/// </returns>
	public readonly override string ToString()
	{
		return $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} "
			+ $"{{M21:{M12} M22:{M22} M23:{M23} M24:{M24}}} "
			+ $"{{M31:{M13} M32:{M23} M33:{M33} M34:{M34}}} "
			+ $"{{M41:{M14} M42:{M24} M43:{M34} M44:{M44}}} }}";
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
	public static Matrix4x4Symmetric operator +(Matrix4x4Symmetric value1, Matrix4x4Symmetric value2)
	{
		return new Matrix4x4Symmetric {
			M11 = value1.M11 + value2.M11,
			M12 = value1.M12 + value2.M12,
			M13 = value1.M13 + value2.M13,
			M14 = value1.M14 + value2.M14,
			M22 = value1.M22 + value2.M22,
			M23 = value1.M23 + value2.M23,
			M24 = value1.M24 + value2.M24,
			M33 = value1.M33 + value2.M33,
			M34 = value1.M34 + value2.M34,
			M44 = value1.M44 + value2.M44
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
	public static Matrix4x4Symmetric operator -(Matrix4x4Symmetric value)
	{
		return new Matrix4x4Symmetric {
			M11 = -value.M11,
			M12 = -value.M12,
			M13 = -value.M13,
			M14 = -value.M14,
			M22 = -value.M22,
			M23 = -value.M23,
			M24 = -value.M24,
			M33 = -value.M33,
			M34 = -value.M34,
			M44 = -value.M44
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
	public static Matrix4x4Symmetric operator -(Matrix4x4Symmetric value1, Matrix4x4Symmetric value2)
	{
		return new Matrix4x4Symmetric {
			M11 = value1.M11 - value2.M11,
			M12 = value1.M12 - value2.M12,
			M13 = value1.M13 - value2.M13,
			M14 = value1.M14 - value2.M14,
			M22 = value1.M22 - value2.M22,
			M23 = value1.M23 - value2.M23,
			M24 = value1.M24 - value2.M24,
			M33 = value1.M33 - value2.M33,
			M34 = value1.M34 - value2.M34,
			M44 = value1.M44 - value2.M44
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
	public static Matrix4x4Symmetric operator *(Matrix4x4Symmetric value1, Matrix4x4Symmetric value2)
	{
		return new Matrix4x4Symmetric {
			M11 = value1.M11 * value2.M11 + value1.M12 * value2.M12 + value1.M13 * value2.M13 + value1.M14 * value2.M14,
			M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M23 + value1.M14 * value2.M24,
			M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M34,
			M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44,
			M22 = value1.M12 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M23 + value1.M24 * value2.M24,
			M23 = value1.M12 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M34,
			M24 = value1.M12 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44,
			M33 = value1.M13 * value2.M13 + value1.M23 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M34,
			M34 = value1.M13 * value2.M14 + value1.M23 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44,
			M44 = value1.M14 * value2.M14 + value1.M24 * value2.M24 + value1.M34 * value2.M34 + value1.M44 * value2.M44
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
	public static SystemVector4 operator *(in Matrix4x4Symmetric matrix, in SystemVector4 vector)
	{
		return new SystemVector4 {
			X = matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z + matrix.M14 * vector.W,
			Y = matrix.M12 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z + matrix.M24 * vector.W,
			Z = matrix.M13 * vector.X + matrix.M23 * vector.Y + matrix.M33 * vector.Z + matrix.M34 * vector.W,
			W = matrix.M14 * vector.X + matrix.M24 * vector.Y + matrix.M34 * vector.Z + matrix.M44 * vector.W
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
	public static Matrix4x4Symmetric operator *(Matrix4x4Symmetric matrix, float scalar)
	{
		return new Matrix4x4Symmetric {
			M11 = matrix.M11 * scalar,
			M12 = matrix.M12 * scalar,
			M13 = matrix.M13 * scalar,
			M14 = matrix.M14 * scalar,
			M22 = matrix.M22 * scalar,
			M23 = matrix.M23 * scalar,
			M24 = matrix.M24 * scalar,
			M33 = matrix.M33 * scalar,
			M34 = matrix.M34 * scalar,
			M44 = matrix.M44 * scalar
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
	public static bool operator ==(Matrix4x4Symmetric value1, Matrix4x4Symmetric value2)
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
	public static bool operator !=(Matrix4x4Symmetric value1, Matrix4x4Symmetric value2)
	{
		return !value1.Equals(value2);
	}

	/// <summary>
	/// Implicitly casts the 4x4 symmetric matrix to the common 4x4 matrix type.
	/// </summary>
	/// <param name="matrix">
	/// The matrix to cast.
	/// </param>
	/// <returns>
	/// The <paramref name="matrix" /> casted to Matrix4x4 type.
	/// </returns>
	public static implicit operator Matrix4x4(Matrix4x4Symmetric matrix) => new Matrix4x4 {
		M11 = matrix.M11,
		M12 = matrix.M12,
		M13 = matrix.M13,
		M14 = matrix.M14,
		M21 = matrix.M12,
		M22 = matrix.M22,
		M23 = matrix.M23,
		M24 = matrix.M24,
		M31 = matrix.M13,
		M32 = matrix.M23,
		M33 = matrix.M33,
		M34 = matrix.M34,
		M41 = matrix.M14,
		M42 = matrix.M24,
		M43 = matrix.M34,
		M44 = matrix.M44
	};

	/// <summary>
	/// Explicitly casts the 4x4 matrix to the 4x4 symmetric matrix type.
	/// </summary>
	/// <param name="matrix">
	/// The matrix to cast.
	/// </param>
	/// <returns>
	/// The <paramref name="matrix" /> casted to Matrix4x4Symmetric type.
	/// </returns>
	public static explicit operator Matrix4x4Symmetric(Matrix4x4 matrix) => new Matrix4x4Symmetric {
		M11 = matrix.M11,
		M12 = matrix.M12,
		M13 = matrix.M13,
		M14 = matrix.M14,
		M22 = matrix.M22,
		M23 = matrix.M23,
		M24 = matrix.M24,
		M33 = matrix.M33,
		M34 = matrix.M34,
		M44 = matrix.M44
	};

	public static Matrix4x4Symmetric FromMatrix(Matrix3x3Symmetric matrix)
	{
		return new Matrix4x4Symmetric {
			M11 = matrix.M11,
			M12 = matrix.M12,
			M13 = matrix.M13,
			M14 = 0.0f,
			M22 = matrix.M22,
			M23 = matrix.M23,
			M24 = 0.0f,
			M33 = matrix.M33,
			M34 = 0.0f,
			M44 = 1.0f,
		};
	}
}
