using System;
using System.Diagnostics.CodeAnalysis;
using SystemVector2 = System.Numerics.Vector2;

namespace Phyksar.Numerics;

/// <summary>
/// Represents a 2x2 matrix.
/// </summary>
public struct Matrix2x2 : IEquatable<Matrix2x2>
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
	/// The first element of the second row.
	/// </summary>
	public float M21;

	/// <summary>
	/// The second element of the second row.
	/// </summary>
	public float M22;

	/// <summary>
	/// Creates a 2x2 matrix from the specified components.
	/// </summary>
	/// <param name="m11">
	/// The value to assign to the first element in the first row.
	/// </param>
	/// <param name="m12">
	/// The value to assign to the second element in the first row.
	/// </param>
	/// <param name="m21">
	/// The value to assign to the first element in the second row.
	/// </param>
	/// <param name="m22">
	/// The value to assign to the second element in the second row.
	/// </param>
	public Matrix2x2(float m11, float m12, float m21, float m22)
	{
		M11 = m11;
		M12 = m12;
		M21 = m21;
		M22 = m22;
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
			} else if (row == 1 && column == 0) {
				return M21;
			} else if (row == 1 && column == 1) {
				return M22;
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
			} else if (row == 1 && column == 0) {
				M21 = value;
			} else if (row == 1 && column == 1) {
				M22 = value;
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
	/// Calculates the determinant of the current 2x2 matrix.
	/// </summary>
	/// <returns>
	/// The determinant.
	/// </returns>
	public readonly float Determinant => M11 * M22 - M12 * M21;

	/// <summary>
	/// Gets or sets the X-axis vector of matrix.
	/// </summary>
	/// <returns>
	/// The X-axis vector.
	/// </returns>
	public SystemVector2 AxisX {
		readonly get => new SystemVector2(M11, M21);
		set {
			M11 = value.X;
			M21 = value.Y;
		}
	}

	/// <summary>
	/// Gets or sets the Y-axis vector of matrix.
	/// </summary>
	/// <returns>
	/// The Y-axis vector.
	/// </returns>
	public SystemVector2 AxisY {
		readonly get => new SystemVector2(M12, M22);
		set {
			M12 = value.X;
			M22 = value.Y;
		}
	}

	/// <summary>
	/// Gets the empty zero matrix.
	/// </summary>
	/// <returns>
	/// The zero matrix.
	/// </returns>
	public static Matrix2x2 Zero => new Matrix2x2 {
		M11 = 0.0f,
		M12 = 0.0f,
		M21 = 0.0f,
		M22 = 0.0f
	};

	/// <summary>
	/// Gets the multiplicative identity matrix.
	/// </summary>
	/// <returns>
	/// The identity matrix.
	/// </returns>
	public static Matrix2x2 Identity => new Matrix2x2 {
		M11 = 1.0f,
		M12 = 0.0f,
		M21 = 0.0f,
		M22 = 1.0f
	};

	/// <summary>
	/// Creates a scaling matrix from the specified X and Y components.
	/// </summary>
	/// <param name="xScale">
	/// The value to scale by on the X axis.
	/// </param>
	/// <param name="yScale">
	/// The value to scale by on the Y axis.
	/// </param>
	/// <returns>
	/// The scaling matrix.
	/// </returns>
	public static Matrix2x2 CreateScale(float xScale, float yScale)
	{
		return new Matrix2x2 {
			M11 = xScale,
			M12 = 0.0f,
			M21 = 0.0f,
			M22 = yScale
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
	public static Matrix2x2 CreateScale(SystemVector2 scales)
	{
		return new Matrix2x2 {
			M11 = scales.X,
			M12 = 0.0f,
			M21 = 0.0f,
			M22 = scales.Y
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
	public static Matrix2x2 CreateScale(float scale)
	{
		return new Matrix2x2 {
			M11 = scale,
			M12 = 0.0f,
			M21 = 0.0f,
			M22 = scale
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
	public static Matrix2x2 Transpose(Matrix2x2 matrix)
	{
		return new Matrix2x2 {
			M11 = matrix.M11,
			M12 = matrix.M21,
			M21 = matrix.M12,
			M22 = matrix.M22
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
	public static Matrix2x2 Adjugate(Matrix2x2 matrix)
	{
		return new Matrix2x2 {
			M11 = matrix.M22,
			M12 = -matrix.M12,
			M21 = -matrix.M21,
			M22 = matrix.M11
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
	public static bool Invert(Matrix2x2 matrix, out Matrix2x2 result)
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
		if (obj is not Matrix2x2 matrix) {
			return false;
		}
		return Equals(matrix);
	}

	/// <summary>
	/// Returns a value that indicates whether this instance and another 2x2 matrix are equal.
	/// </summary>
	/// <param name="other">
	/// The other matrix.
	/// </param>
	/// <returns>
	/// True if the two matrices are equal; false otherwise.
	/// </returns>
	public readonly bool Equals(Matrix2x2 other)
	{
		return M11 == other.M11 && M12 == other.M12 && M21 == other.M21 && M22 == other.M22;
	}

	/// <summary>
	/// Returns the hash code for this instance.
	/// </summary>
	/// <returns>
	/// The hash code.
	/// </returns>
	public readonly override int GetHashCode()
	{
		return HashCode.Combine(M11, M12, M21, M22);
	}

	/// <summary>
	/// Returns a string that represents this matrix.
	/// </summary>
	/// <returns>
	/// The string representation of this matrix.
	/// </returns>
	public readonly override string ToString()
	{
		return $"{{ {{M11:{M11} M12:{M12}}} {{M21:{M21} M22:{M22}}} }}";
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
	public static Matrix2x2 operator +(Matrix2x2 value1, Matrix2x2 value2)
	{
		return new Matrix2x2 {
			M11 = value1.M11 + value2.M11,
			M12 = value1.M12 + value2.M12,
			M21 = value1.M21 + value2.M21,
			M22 = value1.M22 + value2.M22
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
	public static Matrix2x2 operator -(Matrix2x2 value)
	{
		return new Matrix2x2 {
			M11 = -value.M11,
			M12 = -value.M12,
			M21 = -value.M21,
			M22 = -value.M22
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
	public static Matrix2x2 operator -(Matrix2x2 value1, Matrix2x2 value2)
	{
		return new Matrix2x2 {
			M11 = value1.M11 - value2.M11,
			M12 = value1.M12 - value2.M12,
			M21 = value1.M21 - value2.M21,
			M22 = value1.M22 - value2.M22
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
	public static Matrix2x2 operator *(Matrix2x2 value1, Matrix2x2 value2)
	{
		return new Matrix2x2 {
			M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21,
			M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22,
			M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21,
			M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22
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
	public static SystemVector2 operator *(in Matrix2x2 matrix, in SystemVector2 vector)
	{
		return new SystemVector2 {
			X = matrix.M11 * vector.X + matrix.M12 * vector.Y,
			Y = matrix.M21 * vector.X + matrix.M22 * vector.Y,
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
	public static SystemVector2 operator *(in SystemVector2 vector, in Matrix2x2 matrix)
	{
		return new SystemVector2 {
			X = matrix.M11 * vector.X + matrix.M21 * vector.Y,
			Y = matrix.M12 * vector.X + matrix.M22 * vector.Y
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
	public static Matrix2x2 operator *(Matrix2x2 matrix, float scalar)
	{
		return new Matrix2x2 {
			M11 = matrix.M11 * scalar,
			M12 = matrix.M12 * scalar,
			M21 = matrix.M21 * scalar,
			M22 = matrix.M22 * scalar
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
	public static bool operator ==(Matrix2x2 value1, Matrix2x2 value2)
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
	public static bool operator !=(Matrix2x2 value1, Matrix2x2 value2)
	{
		return !value1.Equals(value2);
	}
}
