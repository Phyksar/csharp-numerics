using NUnit.Framework;
using System.Numerics;

namespace Phyksar.Numerics.Tests;

[TestFixture]
public class Matrix4x4SymmetricTest
{
	[Test]
	public void TestDeterminant()
	{
		var matrix = new Matrix4x4Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M24 = 7.0f,
			M33 = 8.0f,
			M34 = 9.0f,
			M44 = 10.0f
		};

		Assert.AreEqual(-2.0f, matrix.Determinant);
	}

	[Test]
	public void TestInvert()
	{
		var matrix = new Matrix4x4Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M24 = 7.0f,
			M33 = 8.0f,
			M34 = 9.0f,
			M44 = 10.0f
		};

		var result = Matrix4x4Symmetric.Invert(matrix, out var inverseMatrix);

		var expected = new Matrix4x4Symmetric {
			M11 = 0.5f,
			M12 = -0.5f,
			M13 = -1.5f,
			M14 = 1.5f,
			M22 = 1.5f,
			M23 = -1.5f,
			M24 = 0.5f,
			M33 = 3.5f,
			M34 = -1.5f,
			M44 = 0.5f
		};
		Assert.IsTrue(result);
		Assert.AreEqual(expected, inverseMatrix);
	}

	[Test]
	public void TestInvertProduct()
	{
		var matrix = new Matrix4x4Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M24 = 7.0f,
			M33 = 8.0f,
			M34 = 9.0f,
			M44 = 10.0f
		};

		var result = Matrix4x4Symmetric.Invert(matrix, out var inverseMatrix);

		Assert.IsTrue(result);
		Assert.AreEqual(Matrix4x4Symmetric.Identity, inverseMatrix * matrix);
	}

	[Test]
	public void TestInvertZeroDeterminant()
	{
		var matrix = new Matrix4x4Symmetric {
			M11 = 2.0f,
			M12 = -1.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M24 = 7.0f,
			M33 = 8.0f,
			M34 = 9.0f,
			M44 = 10.0f
		};

		var result = Matrix4x4Symmetric.Invert(matrix, out var inverseMatrix);

		Assert.IsFalse(result);
	}

	[Test]
	public void TestCastToMatrix4x4()
	{
		var matrix = new Matrix4x4Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M24 = 7.0f,
			M33 = 8.0f,
			M34 = 9.0f,
			M44 = 10.0f
		};

		Matrix4x4 result = matrix;

		var expected = new Matrix4x4 {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M21 = 2.0f,
			M22 = 5.0f,
			M23 = 6.0f,
			M24 = 7.0f,
			M31 = 3.0f,
			M32 = 6.0f,
			M33 = 8.0f,
			M34 = 9.0f,
			M41 = 4.0f,
			M42 = 7.0f,
			M43 = 9.0f,
			M44 = 10.0f
		};
		Assert.AreEqual(expected, result);
	}

	[Test]
	public void TestCastFromMatrix4x4()
	{
		var matrix = new Matrix4x4 {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M21 = 5.0f,
			M22 = 6.0f,
			M23 = 7.0f,
			M24 = 8.0f,
			M31 = 9.0f,
			M32 = 10.0f,
			M33 = 11.0f,
			M34 = 12.0f,
			M41 = 13.0f,
			M42 = 14.0f,
			M43 = 15.0f,
			M44 = 16.0f
		};

		Matrix4x4Symmetric result = (Matrix4x4Symmetric)matrix;

		var expected = new Matrix4x4Symmetric {
			M11 = 1.0f,
			M12 = 2.0f,
			M13 = 3.0f,
			M14 = 4.0f,
			M22 = 6.0f,
			M23 = 7.0f,
			M24 = 8.0f,
			M33 = 11.0f,
			M34 = 12.0f,
			M44 = 16.0f
		};
		Assert.AreEqual(expected, result);
	}
}
