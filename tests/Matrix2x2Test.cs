using NUnit.Framework;

namespace Phyksar.Numerics.Tests;

[TestFixture]
public class Matrix2x2Test
{
	[Test]
	public void TestDeterminant()
	{
		var matrix = new Matrix2x2 {
			M11 = 1.0f,
			M12 = 2.0f,
			M21 = 3.0f,
			M22 = 4.0f
		};

		Assert.AreEqual(-2.0f, matrix.Determinant);
	}

	[Test]
	public void TestInvert()
	{
		var matrix = new Matrix2x2 {
			M11 = 1.0f,
			M12 = -2.0f,
			M21 = -3.0f,
			M22 = 4.0f
		};

		var result = Matrix2x2.Invert(matrix, out var inverseMatrix);

		var expected = new Matrix2x2 {
			M11 = -2.0f,
			M12 = -1.0f,
			M21 = -1.5f,
			M22 = -0.5f
		};
		Assert.IsTrue(result);
		Assert.AreEqual(expected, inverseMatrix);
	}

	[Test]
	public void TestInvertProduct()
	{
		var matrix = new Matrix2x2 {
			M11 = 1.0f,
			M12 = -2.0f,
			M21 = -3.0f,
			M22 = 4.0f
		};

		var result = Matrix2x2.Invert(matrix, out var inverseMatrix);

		Assert.IsTrue(result);
		Assert.AreEqual(Matrix2x2.Identity, inverseMatrix * matrix);
	}

	[Test]
	public void TestInvertZeroDeterminant()
	{
		var matrix = new Matrix2x2 {
			M11 = 1.0f,
			M12 = 2.0f,
			M21 = 3.0f,
			M22 = 6.0f
		};

		var result = Matrix2x2.Invert(matrix, out var inverseMatrix);

		Assert.IsFalse(result);
	}
}
