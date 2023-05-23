using NUnit.Framework;

namespace Phyksar.Numerics.Tests;

[TestFixture]
public class Matrix2x2SymmetricTest
{
	[Test]
	public void TestDeterminant()
	{
		var matrix = new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M22 = 3.0f
		};

		Assert.AreEqual(-1.0f, matrix.Determinant);
	}

	[Test]
	public void TestInvert()
	{
		var matrix = new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = -2.0f,
			M22 = 3.0f
		};

		var result = Matrix2x2Symmetric.Invert(matrix, out var inverseMatrix);

		var expected = new Matrix2x2Symmetric {
			M11 = -3.0f,
			M12 = -2.0f,
			M22 = -1.0f
		};
		Assert.IsTrue(result);
		Assert.AreEqual(expected, inverseMatrix);
	}

	[Test]
	public void TestInvertProduct()
	{
		var matrix = new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = -2.0f,
			M22 = 3.0f
		};

		var result = Matrix2x2Symmetric.Invert(matrix, out var inverseMatrix);

		Assert.IsTrue(result);
		Assert.AreEqual(Matrix2x2Symmetric.Identity, inverseMatrix * matrix);
	}

	[Test]
	public void TestInvertZeroDeterminant()
	{
		var matrix = new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M22 = 4.0f
		};

		var result = Matrix2x2Symmetric.Invert(matrix, out var inverseMatrix);

		Assert.IsFalse(result);
	}

	[Test]
	public void TestCastToMatrix2x2()
	{
		var matrix = new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M22 = 3.0f
		};

		Matrix2x2 result = matrix;

		var expected = new Matrix2x2 {
			M11 = 1.0f,
			M12 = 2.0f,
			M21 = 2.0f,
			M22 = 3.0f
		};
		Assert.AreEqual(expected, result);
	}

	[Test]
	public void TestCastFromMatrix2x2()
	{
		var matrix = new Matrix2x2 {
			M11 = 1.0f,
			M12 = 2.0f,
			M21 = 3.0f,
			M22 = 4.0f
		};

		Matrix2x2Symmetric result = (Matrix2x2Symmetric)matrix;

		var expected = new Matrix2x2Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M22 = 4.0f
		};
		Assert.AreEqual(expected, result);
	}
}
