using NUnit.Framework;

namespace Phyksar.Numerics.Tests;

[TestFixture]
public class Matrix3x3SymmetricTest
{
	[Test]
	public void TestDeterminant()
	{
		var matrix = new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M22 = 4.0f,
			M23 = 5.0f,
			M33 = 6.0f
		};

		Assert.AreEqual(-1.0f, matrix.Determinant);
	}

	[Test]
	public void TestInvert()
	{
		var matrix = new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M22 = 4.0f,
			M23 = 5.0f,
			M33 = 6.0f
		};

		var result = Matrix3x3Symmetric.Invert(matrix, out var inverseMatrix);

		var expected = new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = -3.0f,
			M13 = 2.0f,
			M22 = 3.0f,
			M23 = -1.0f,
			M33 = 0.0f
		};
		Assert.IsTrue(result);
		Assert.AreEqual(expected, inverseMatrix);
	}

	[Test]
	public void TestInvertProduct()
	{
		var matrix = new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M22 = 4.0f,
			M23 = 5.0f,
			M33 = 6.0f
		};

		var result = Matrix3x3Symmetric.Invert(matrix, out var inverseMatrix);

		Assert.IsTrue(result);
		Assert.AreEqual(Matrix3x3Symmetric.Identity, inverseMatrix * matrix);
	}

	[Test]
	public void TestInvertZeroDeterminant()
	{
		var matrix = new Matrix3x3Symmetric {
			M11 = -1.0f,
			M12 = 1.0f,
			M13 = 2.0f,
			M22 = 3.0f,
			M23 = 4.0f,
			M33 = 5.0f
		};

		var result = Matrix3x3Symmetric.Invert(matrix, out var inverseMatrix);

		Assert.IsFalse(result);
	}

	[Test]
	public void TestCastToMatrix3x3()
	{
		var matrix = new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M22 = 4.0f,
			M23 = 5.0f,
			M33 = 6.0f
		};

		Matrix3x3 result = matrix;

		var expected = new Matrix3x3 {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M21 = 2.0f,
			M22 = 4.0f,
			M23 = 5.0f,
			M31 = 3.0f,
			M32 = 5.0f,
			M33 = 6.0f
		};
		Assert.AreEqual(expected, result);
	}

	[Test]
	public void TestCastFromMatrix3x3()
	{
		var matrix = new Matrix3x3 {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M21 = 4.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M31 = 7.0f,
			M32 = 8.0f,
			M33 = 9.0f
		};

		Matrix3x3Symmetric result = (Matrix3x3Symmetric)matrix;

		var expected = new Matrix3x3Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M33 = 9.0f
		};
		Assert.AreEqual(expected, result);
	}
}
