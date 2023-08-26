using System;

namespace Phyksar.Numerics.Buffers;

/// <summary>
/// Represents a fixed-size buffer connected end-to-end.
/// </summary>
public struct RingBuffer<T> where T : unmanaged
{
	private T[] Buffer = Array.Empty<T>();
	private int Shift = 0;

	/// <summary>
	/// The length of the buffer.
	/// </summary>
	public int Length => Buffer.Length;

	/// <summary>
	/// Creates a ring buffer of specifined length.
	/// </summary>
	/// <param name="length">
	/// The length of the buffer.
	/// </param>
	public RingBuffer(int length)
	{
		Buffer = (length > 0) ? new T[length] : Array.Empty<T>();
		Shift = 0;
	}

	/// <summary>
	/// Writes the value to the buffer and shifts the pointer by one element.
	/// </summary>
	/// <param name="value">
	/// The value to write to the buffer.
	/// </param>
	public void Write(T value)
	{
		Buffer[Shift] = value;
		Shift = (Shift + 1) % Buffer.Length;
	}

	/// <summary>
	/// Reads the value from the buffer without modifying its pointer.
	/// </summary>
	/// <param name="index">
	/// The index of the element.
	/// </param>
	/// <returns>
	/// The value at <paramref name="index" />.
	/// </returns>
	public T Peek(int index)
	{
		return Buffer[(Shift + index).UnsignedMod(Buffer.Length)];
	}

	/// <summary>
	/// Fills the buffer with a single value.
	/// </summary>
	/// <param name="value">
	/// The value of every element.
	/// </param>
	public void Fill(T value)
	{
		Array.Fill(Buffer, value);
	}
}
