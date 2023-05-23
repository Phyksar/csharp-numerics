using NUnit.Framework;

namespace Phyksar.Numerics.Tests;

[TestFixture]
public class Matrix3x3Test
{
	[Test]
	public void TestDeterminant()
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

		Assert.AreEqual(0.0f, matrix.Determinant);
	}

	[Test]
	public void TestInvert()
	{
		var matrix = new Matrix3x3 {
			M11 = -1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M21 = 4.0f,
			M22 = 5.0f,
			M23 = 4.0f,
			M31 = 3.0f,
			M32 = 2.0f,
			M33 = 1.0f
		};

		var result = Matrix3x3.Invert(matrix, out var inverseMatrix);

		var expected = new Matrix3x3 {
			M11 = 1.5f,
			M12 = -2.0f,
			M13 = 3.5f,
			M21 = -4.0f,
			M22 = 5.0f,
			M23 = -8.0f,
			M31 = 3.5f,
			M32 = -4.0f,
			M33 = 6.5f
		};
		Assert.IsTrue(result);
		Assert.AreEqual(expected, inverseMatrix);
	}

	[Test]
	public void TestInvertProduct()
	{
		var matrix = new Matrix3x3 {
			M11 = -1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M21 = 4.0f,
			M22 = 5.0f,
			M23 = 4.0f,
			M31 = 3.0f,
			M32 = 2.0f,
			M33 = 1.0f
		};

		var result = Matrix3x3.Invert(matrix, out var inverseMatrix);

		Assert.IsTrue(result);
		Assert.AreEqual(Matrix3x3.Identity, inverseMatrix * matrix);
	}

	[Test]
	public void TestInvertZeroDeterminant()
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

		var result = Matrix3x3.Invert(matrix, out var inverseMatrix);

		Assert.IsFalse(result);
	}
}
